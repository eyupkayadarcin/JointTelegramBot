using JointTelegramBot.Web.Configuration;
using JointTelegramBot.Web.Services.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace JointTelegramBot.Web.Services.Telegram
{
    public class TelegramMessageReceiveService
    {
        private static ILogger<TelegramMessageReceiveService> _log;
        private readonly TelegramConfig _config;
        private readonly DatabaseService _databaseService;
        private TelegramBotClient _bot;

        public TelegramMessageReceiveService(ILogger<TelegramMessageReceiveService> log,  TelegramConfig options, DatabaseService databaseService)
        {
            _log = log;
            _config = options;
            _databaseService = databaseService;
            
        }

        public void StartReceivingMessages(TelegramBotClient bot)
        {
            SetupBot(bot);
        }
        private async void BotOnMessageReceivedAsync(object sender, UpdateEventArgs e)
        {
            var message = e.Update.Message;
            await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await _bot.SendTextMessageAsync(message.Chat.Id, "Merhaba Deneme Botuna Hoşgeldiniz! /help'ten istediğiniz özelliklere ulaşabilirsiniz");
            if (message.Type != MessageType.TextMessage) return;
            try
            {
                await CheckMessage(message.Text, message.Chat.Id);
                await _databaseService.AddUser(e.Update.Message.From.Id, e.Update.Message.From.FirstName, e.Update.Message.From.LastName, e.Update.Message.From.Username);
            }
            catch (Exception ex)
            {
                _log.LogError($"Oopps. {ex}");
                
            }

        }

        private async Task CheckMessage(string text, long id)
        {
            if (text.StartsWith("/start"))
            {
                await _bot.SendTextMessageAsync(id, "Start'da yazılacak metin");
                

            }

        }

        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            _log.LogError($"Error received {e.ApiRequestException.Message}");

            _bot.OnUpdate += BotOnMessageReceivedAsync;
            _bot.OnReceiveError -= BotOnReceiveError;

            var bot = new TelegramBotClient(_config.BotToken);
            SetupBot(bot);
        }

        private void SetupBot(TelegramBotClient bot)
        {
            _bot = bot;
            _bot.OnUpdate += BotOnMessageReceivedAsync;
            _bot.OnReceiveError += BotOnReceiveError;
            _bot.StartReceiving();
        }

       
    }
}
