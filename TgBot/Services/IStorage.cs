﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgBot.Models;

namespace TgBot.Services
{
    internal interface IStorage
    {
        Session GetSession(long chatId);
    }
}
