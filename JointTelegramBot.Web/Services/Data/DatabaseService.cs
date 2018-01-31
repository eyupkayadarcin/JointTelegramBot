using JointTelegramBot.Web.Data;
using JointTelegramBot.Web.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace JointTelegramBot.Web.Services.Data
{
    public class DatabaseService
    {
        private readonly JointBotContext _context;
        private readonly ILogger<DatabaseService> _log;

        public DatabaseService(JointBotContext context, ILogger<DatabaseService> log)
        {
            _context = context;
            _log = log;
        }
        public async Task AddUser(int id, string firstName,string lastName, string userName)
        {
            if (_context.People.Any(o => o.UserId == id)) return;
            var users = new Models.User
            {
                UserId = id,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName
            };
                _context.People.Add(users);
                await _context.SaveChangesAsync();
      
            
        }

       
    }
}
