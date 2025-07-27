using Microsoft.EntityFrameworkCore;
using account_web.Models;

namespace account_web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<UserGroupMapping> UserGroupMappings { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public DbSet<LoginRecord> LoginRecords { get; set; }
        public DbSet<AccountActionRecord> AccountActionRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 設定 User 實體
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // 設定 ID 自動生成
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FactoryId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.UserId).IsUnique();
            });

            // 設定 Account 實體
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.AccountId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.AccountPassword).IsRequired().HasMaxLength(255);
                entity.Property(e => e.DomainCategory).IsRequired();
                entity.Property(e => e.DomainType).IsRequired();
                entity.Property(e => e.ProjectId).HasMaxLength(20);
                entity.Property(e => e.ServerIp).HasMaxLength(15);
                entity.Property(e => e.ServerPort).HasMaxLength(10);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // 設定 Factory 實體
            modelBuilder.Entity<Factory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FactoryId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.FactoryName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.FactoryId).IsUnique();
            });

            // 設定 Group 實體
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.GroupName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ProjectId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // 設定 Project 實體
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.ProjectId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.ProjectName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FactoryId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.RoleId).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.ProjectId).IsUnique();
            });

            // 設定 ProjectMember 實體
            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.ProjectId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => new { e.ProjectId, e.UserId }).IsUnique();
            });

            // 設定 UserGroupMapping 實體
            modelBuilder.Entity<UserGroupMapping>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.GroupId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => new { e.UserId, e.GroupId }).IsUnique();
            });

            // 設定 UserRoleMapping 實體
            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.RoleId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.ScopeType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ScopeId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // 設定 LoginRecord 實體
            modelBuilder.Entity<LoginRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LoginTime).IsRequired().HasMaxLength(50);
                entity.Property(e => e.IpAddress).IsRequired().HasMaxLength(15);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // 設定 AccountActionRecord 實體
            modelBuilder.Entity<AccountActionRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.AccountId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ActionType).IsRequired();
                entity.Property(e => e.Detail).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // 初始資料
            modelBuilder.Entity<Factory>().HasData(
                new Factory
                {
                    Id = 1,
                    FactoryId = "F001",
                    FactoryName = "台北廠",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Factory
                {
                    Id = 2,
                    FactoryId = "F002",
                    FactoryName = "台中廠",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    ProjectId = "P001",
                    ProjectName = "專案一",
                    FactoryId = "F001",
                    RoleId = RoleId.Class01,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Project
                {
                    Id = 2,
                    ProjectId = "P002",
                    ProjectName = "專案二",
                    FactoryId = "F001",
                    RoleId = RoleId.Class02,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserId = "admin",
                    Password = "password123",
                    Name = "張三",
                    FactoryId = "F001",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new User
                {
                    Id = 2,
                    UserId = "user01",
                    Password = "password456",
                    Name = "李四",
                    FactoryId = "F001",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
        }
    }
}
