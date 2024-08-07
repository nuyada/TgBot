using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.Services;
using TgBot.Utilities;

namespace TgBot.Controllers
{
    internal class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly BotTextFunction _textFunction;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, BotTextFunction textFunction, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _textFunction = textFunction;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"🔤 Длина строки" , $"len"),
                        InlineKeyboardButton.WithCallbackData($"🔢 Сумма чисел" , $"sum"),
                        
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  🎁 Наш бот имеет функцию подсчета длины текста.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Также можно посчитать сумму чисел.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    //проверяем выбранную функцию бота
                    switch (_memoryStorage.GetSession(message.Chat.Id).TextTask)
                    {
                        case "len":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"В вашем собщение {_textFunction.GetLenghtText(message.Text)} символов.", cancellationToken: ct);
                            break;
                        case "sum":
                            int? sum = _textFunction.GetSumNumbers(message.Text);
                            if (sum != 0)
                                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {sum}", cancellationToken: ct);
                            else
                                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Невозможно вычислить сумму!", cancellationToken: ct);
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }
    }
}
