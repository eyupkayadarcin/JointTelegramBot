using JointTelegramBot.Web.Configuration;
using JointTelegramBot.Web.Services.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JointTelegramBot.Web.Services
{
    public class StartupCheckingService
    {
        private readonly TelegramBot _bot;
        private readonly TelegramConfig _config;
        public StartupCheckingService(TelegramBot bot , TelegramConfig config)
        {
            _bot = bot;
            _config = config;
        }

        public void Start()
        {
            _bot.StartBot(_config);
        }

    }
}
