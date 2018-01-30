using JointTelegramBot.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JointTelegramBot.Web.Data
{
    public class JointBotContext: DbContext
    {
        public JointBotContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<User> People { get; set; }
        public DbSet<UserStats> Stats { get; set; }
        public DbSet<Twitter> Twitter { get; set; }
        public DbSet<UserStatus> Status { get; set; }
        public DbSet<Wallet> Wallet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserStats>().HasIndex(x => x.RefLink).IsUnique();
        }

    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<JointBotContext>
    {
        public JointBotContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<JointBotContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TelegramBot;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new JointBotContext(builder.Options);
        }
    }
}
