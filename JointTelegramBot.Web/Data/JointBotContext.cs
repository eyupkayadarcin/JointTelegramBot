using JointTelegramBot.Web.Configuration;
using JointTelegramBot.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
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
            //builder.Entity<User>().Property(x => x.UserId)
            //   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            base.OnModelCreating(builder);
        }

    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<JointBotContext>
    {
        public JointBotContext CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<JointBotContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new JointBotContext(builder.Options);
        }
    }
}
