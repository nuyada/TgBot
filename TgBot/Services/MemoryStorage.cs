using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBot.Services
{
    internal class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Models.Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Models.Session>();
        }
        public Models.Session GetSession(long chatId)

        {
            // Возвращаем сессию по ключу, если она существует

            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];
            // Создаем и возвращаем новую, если такой не было
            var newSession = new Models.Session() { TextTask = "" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
