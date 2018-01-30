using JointTelegramBot.Web.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
      
    }
}
