using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.Services;

namespace TgBot.Controllers
{
    internal class InlineKeyboardController
    {
        readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            {
                _telegramClient = telegramBotClient;
                _memoryStorage = memoryStorage;
            }
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).TextTask = callbackQuery.Data;

            // Генерим информационное сообщение
            string function = callbackQuery.Data switch
            {
                "len" => "подсчёт количества символов в тексте",
                "sum" => "вычисление суммы чисел",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбрана функция - {function}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Ее можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
