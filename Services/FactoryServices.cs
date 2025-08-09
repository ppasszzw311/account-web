using account_web.Data;
using account_web.Models;
using Microsoft.EntityFrameworkCore;

namespace account_web.Services;

public class FactoryServices
{
    private readonly ApplicationDbContext _context;

    public FactoryServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Factory>> GetFactories()
    {
        return await _context.Factories.ToListAsync();
    }

    public async Task<Factory?> GetFactoryById(int id)
    {
        return await _context.Factories.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Factory?> GetFactoryByFactoryId(string factoryId)
    {
        return await _context.Factories.FirstOrDefaultAsync(m => m.FactoryId == factoryId);
    }

    public async void InsertFactory(Factory factory)
    {
        if (factory == null) throw new ArgumentNullException(nameof(factory));
        var factoryExists = await _context.Factories.AnyAsync(f => f.FactoryId == factory.FactoryId);
        if (factoryExists)
        {
            throw new InvalidOperationException($"Factory with ID {factory.FactoryId} already exists.");
        }
        factory.CreatedAt = DateTime.Now;
        factory.UpdatedAt = DateTime.Now;
        _context.Factories.Add(factory);
        await _context.SaveChangesAsync();
    }

    public async void UpdateFactory(Factory factory)
    {
        if (factory == null) throw new ArgumentNullException(nameof(factory));
        var existingFactory = await _context.Factories.FindAsync(factory.Id);
        if (existingFactory == null)
        {
            throw new InvalidOperationException($"Factory with ID {factory.Id} does not exist.");
        }
        existingFactory.FactoryId = factory.FactoryId;
        existingFactory.FactoryName = factory.FactoryName;
        existingFactory.UpdatedAt = DateTime.Now;
        _context.Factories.Update(existingFactory);
        await _context.SaveChangesAsync();
    }

    public async void DeleteFactory(int id)
    {
        var factory = await _context.Factories.FindAsync(id);
        if (factory == null)
        {
            throw new InvalidOperationException($"Factory with ID {id} does not exist.");
        }
        _context.Factories.Remove(factory);
        await _context.SaveChangesAsync();
    }
}
