using account_web.Data;
using account_web.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace account_web.Services;

public class GroupService : BaseDbService
{
    UserServices _userServices;
    public GroupService(ApplicationDbContext context) : base(context)
    {
        _userServices = new UserServices(context);
    }

    // Get all groups
    public async Task<IEnumerable<Models.Group>> GetGroups()
    {
        return await _context.Groups.ToListAsync();
    }

    // get group by id
    public async Task<Models.Group?> GetGroupById(int id)
    {
        return await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
    }

    // insert group
    public async Task InsertGroup(Models.Group group)
    {
        if (group == null) throw new ArgumentNullException(nameof(group));
        if (string.IsNullOrWhiteSpace(group.GroupName))
        {
            throw new ArgumentException("Group name cannot be empty", nameof(group.GroupName));
        }
        if (string.IsNullOrWhiteSpace(group.ProjectId))
        {
            throw new ArgumentException("Project ID cannot be empty", nameof(group.ProjectId));
        }
        group.CreatedAt = DateTime.Now;
        group.UpdatedAt = DateTime.Now;
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();
    }

    // update group
    public async Task UpdateGroup(Models.Group group)
    {
        if (group == null) throw new ArgumentNullException(nameof(group));
        var existingGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
        if (existingGroup == null)
        {
            throw new ArgumentNullException($"Group with Id {group.Id} not found!");
        }
        existingGroup.GroupName = group.GroupName;
        existingGroup.ProjectId = group.ProjectId;
        existingGroup.Description = group.Description;
        existingGroup.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
    }

    // delete group
    public async Task DeleteGroup(int id)
    {
        var group = await _context.Groups.FindAsync(id);
        if (group == null)
        {
            throw new InvalidOperationException($"Group with ID {id} does not exist.");
        }
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsGroupExists(string groupId)
    {
        if (string.IsNullOrWhiteSpace(groupId))
        {
            throw new ArgumentException("Group ID cannot be empty", nameof(groupId));
        }
        return await _context.Groups.AnyAsync(g => g.Id.ToString() == groupId);
    }

    // user group mapping
    // 新增一組
    public async Task AddUserToGroup(string groupId, string userId)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(groupId))
        {
            throw new ArgumentException("UserID or GroupID cannot be empty", nameof(userId));
        }
        var isGroupExists = await IsGroupExists(groupId);
        var isUserExists = await _userServices.IsUserExists(userId);
        var existingMapping = await _context.UserGroupMappings
            .FirstOrDefaultAsync(ug => ug.GroupId.ToString() == groupId && ug.UserId == userId);
        if (isGroupExists && isUserExists)
        {
            if (existingMapping == null)
            {
                var userGroup = new UserGroupMapping
                {
                    GroupId = groupId,
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };
                _context.UserGroupMappings.Add(userGroup);
                await _context.SaveChangesAsync();
            }
        }        
    }
    // 新增多組
    public async Task AddUsersToGroup(string groupId, IEnumerable<string> userIds)
    {
        if (string.IsNullOrWhiteSpace(groupId) || userIds == null || !userIds.Any())
        {
            throw new ArgumentException("GroupID or UserIDs cannot be empty", nameof(userIds));
        }
        var isGroupExists = await IsGroupExists(groupId);
        if (!isGroupExists)
        {
            throw new InvalidOperationException($"Group with ID {groupId} does not exist.");
        }

        // 先清空groupid裡面的使用者
        await RemoveUserFromGroup(groupId);

        foreach (var userId in userIds)
        {
            await AddUserToGroup(groupId, userId);
        }
    }

    // 刪除群組
    public async Task RemoveUserFromGroup(string groupId)
    {
        if (string.IsNullOrWhiteSpace(groupId))
        {
            throw new ArgumentException("UserID or GroupID cannot be empty", nameof(groupId));
        }
        var existingMappings = await _context.UserGroupMappings
            .Where(ug => ug.GroupId.ToString() == groupId)
            .ToListAsync();
        if (existingMappings != null)
        {
            existingMappings.ForEach(mapping => _context.UserGroupMappings.Remove(mapping));
            await _context.SaveChangesAsync();
        }
    }
}
