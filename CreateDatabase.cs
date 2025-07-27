using Microsoft.EntityFrameworkCore;
using account_web.Data;

namespace account_web
{
    /// <summary>
    /// 資料庫建立輔助類別
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// 手動建立 SQLite 資料庫
        /// </summary>
        public static void CreateDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("Data Source=AccountApp.db")
                .Options;

            using var context = new ApplicationDbContext(options);

            // 建立資料庫
            context.Database.EnsureCreated();
            Console.WriteLine("SQLite 資料庫 'AccountApp.db' 已成功建立！");
        }

        /// <summary>
        /// 使用遷移建立或更新資料庫
        /// </summary>
        /// <param name="context">資料庫內容</param>
        public static void EnsureDatabaseCreated(ApplicationDbContext context)
        {
            try
            {
                context.Database.Migrate();
                Console.WriteLine("資料庫遷移已完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"資料庫遷移失敗: {ex.Message}");
                // 如果遷移失敗，嘗試建立資料庫
                context.Database.EnsureCreated();
                Console.WriteLine("資料庫已使用 EnsureCreated 建立！");
            }
        }
    }
}
