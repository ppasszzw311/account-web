using account_web.Data;
using account_web.Models;
using account_web.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace account_web.Services;

public class UserServices : BaseDbService
{
    public UserServices(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<User?> GetUserByUserId(string userId)
    {
        return await _context.Users.FirstOrDefaultAsync(m => m.UserId == userId);
    }

    // create
    public async Task InsertUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        var userExists = await _context.Users.AnyAsync(u => u.UserId == user.UserId);
        if (userExists)
        {
            throw new InvalidOperationException($"User with ID {user.UserId} already exists.");
        }
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    // update
    public async Task UpdateUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        // 先根据主键Id查找用户
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (existingUser == null)
        {
            throw new ArgumentNullException($"User with Id {user.Id} not found!");
        }

        // 更新用户信息
        existingUser.UserId = user.UserId;
        existingUser.Name = user.Name;
        existingUser.Password = user.Password;
        existingUser.FactoryId = user.FactoryId;
        existingUser.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    // update by UserId
    public async Task UpdateUserByUserId(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        // 先根据UserId查找用户
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
        if (existingUser == null)
        {
            throw new ArgumentNullException($"User with UserId {user.UserId} not found!");
        }

        // 更新用戶資訊（不包含密碼和UserId）
        existingUser.Name = user.Name;
        existingUser.FactoryId = user.FactoryId;
        existingUser.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    // delete
    public async Task DeleteUser(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentException("UserId cannot be null or empty", nameof(userId));
        }

        var userExist = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (userExist == null)
        {
            throw new InvalidOperationException($"User with UserId '{userId}' not found!");
        }

        _context.Users.Remove(userExist);
        await _context.SaveChangesAsync();
    }

    // update password
    public async Task UpdatePassword(string userId, string newPassword)
    {
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentException("UserId cannot be null or empty", nameof(userId));
        }

        if (string.IsNullOrEmpty(newPassword))
        {
            throw new ArgumentException("New password cannot be null or empty", nameof(newPassword));
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            throw new InvalidOperationException($"User with UserId '{userId}' not found!");
        }

        user.Password = newPassword;
        user.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
    }

    // validate user login
    public async Task<User?> ValidateUser(string userId, string password)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
        {
            return null;
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId && u.Password == password);
        if (user == null)
        {
            return null;
        }

        return user;
    }

    // create user with DTO
    public async Task<UserResponseDto> CreateUser(UserCreateDto userDto)
    {
        if (userDto == null) throw new ArgumentNullException(nameof(userDto));

        var userExists = await _context.Users.AnyAsync(u => u.UserId == userDto.UserId);
        if (userExists)
        {
            throw new InvalidOperationException($"User with ID {userDto.UserId} already exists.");
        }

        var user = new User
        {
            UserId = userDto.UserId,
            Password = userDto.Password,
            Name = userDto.Name,
            FactoryId = userDto.FactoryId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = user.Id,
            UserId = user.UserId,
            Name = user.Name,
            FactoryId = user.FactoryId,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    // get user by id with DTO
    public async Task<UserResponseDto?> GetUserResponseById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return null;
        }

        return new UserResponseDto
        {
            Id = user.Id,
            UserId = user.UserId,
            Name = user.Name,
            FactoryId = user.FactoryId,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    // get user by userId with DTO
    public async Task<UserResponseDto?> GetUserResponseByUserId(string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == userId);
        if (user == null)
        {
            return null;
        }

        return new UserResponseDto
        {
            Id = user.Id,
            UserId = user.UserId,
            Name = user.Name,
            FactoryId = user.FactoryId,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    // get all users with DTO
    public async Task<IEnumerable<UserResponseDto>> GetUserResponses()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(user => new UserResponseDto
        {
            Id = user.Id,
            UserId = user.UserId,
            Name = user.Name,
            FactoryId = user.FactoryId,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        });
    }
}
