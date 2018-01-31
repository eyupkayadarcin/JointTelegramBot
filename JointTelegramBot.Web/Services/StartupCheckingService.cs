using JointTelegramBot.Web.Configuration;
using JointTelegramBot.Web.Services.Telegram;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace JointTelegramBot.Web.Services
{
    public class StartupCheckingService
    {
        private TelegramBotClient client; 
        private readonly TelegramBot _bot;
        private readonly TelegramConfig _config;
        public StartupCheckingService(TelegramBot bot , TelegramConfig options)
        {
            _bot = bot;
            _config = options;
        }

        public void Start()
        {
            _bot.StartBot(_config);
           

        }

        
    }
}
