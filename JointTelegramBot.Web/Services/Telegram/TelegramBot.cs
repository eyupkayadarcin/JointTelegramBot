using JointTelegramBot.Web.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace JointTelegramBot.Web.Services.Telegram
{
    public class TelegramBot
    {
        private readonly ILogger<TelegramBot> _log;
        private readonly TelegramMessageReceiveService _telegramMessageRecieveService;

        public TelegramBot(TelegramMessageReceiveService telegramMessageReceive, ILogger<TelegramBot> log)
        {
            _log = log;
            _telegramMessageRecieveService = telegramMessageReceive;
        }
        
        public long ChatId { get; set; }

        public void StartBot(TelegramConfig config)
        {
            try
            {
                ChatId = config.ChatId;
                var bot = new TelegramBotClient(config.BotToken);
                _telegramMessageRecieveService.StartReceivingMessages(bot);
            }
            catch (Exception ex)
            {
                _log.LogError("Could not start key. Invalid bot token\n" + ex.Message);
                throw;
            }
        }
    }
}
