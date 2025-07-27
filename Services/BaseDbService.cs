using account_web.Data;

namespace account_web.Services;

public class BaseDbService
{
   protected readonly ApplicationDbContext _context;
   public BaseDbService(ApplicationDbContext context)
   {
      _context = context;
   }
}