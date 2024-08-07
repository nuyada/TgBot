using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TgBot.Utilities
{
    internal class BotTextFunction
    {
        /// <summary>
        /// считает количество
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int GetLenghtText(string text)
        {
            if(text == null)
            {
                return 0;
            }
            return text.Length;
        }
        /// <summary>
        /// Позволяет посчитать сумму переданных чисел
        /// </summary>
        /// <param name="stringNumbers"></param>
        /// <returns></returns>
        public int GetSumNumbers(string stringNumbers)
        {
            int[] numbers = null;
            int sum = 0;
            //проверяет есть ли другие символы помимо чисел если да возвращает false
            if (!stringNumbers.Split(' ').Any(s => !int.TryParse(s, out _)))
            {
                numbers = stringNumbers.Split(' ').Select(int.Parse).ToArray();
                                        
            }

            if (numbers == null)
            {
                return 0;
            }
            // подсчет суммы чисел
            else
            {
                foreach (var number in numbers)
                {
                    sum += number;
                }
                return sum;
            }

        }
    }
}
