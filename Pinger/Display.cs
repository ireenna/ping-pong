using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinger
{
    static class Display
    {
        public static string Score {get;set;}
        public static void Draw()
        {
            for (int i = 1; i < Console.WindowHeight; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(Console.WindowWidth / 2, i);
                Console.Write("|");
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 3, 0);

            Console.Write(Score);
        }
    }
}
