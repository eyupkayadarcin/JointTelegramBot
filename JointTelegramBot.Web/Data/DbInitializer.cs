using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JointTelegramBot.Web.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(JointBotContext context)
        {
            await context.Database.MigrateAsync();

            // Add Seed Data...
        }
    }
}
