using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinger.GameElements
{
    class Zhuk
    {
        public static int ZhukX { get; set; }
        public static int ZhukY { get; set; }
        public static int isExist = 0;
        public void Draw()
        {
            if (isExist == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Random zh_rnd = new Random();
                ZhukX = zh_rnd.Next(3, 110);
                ZhukY = zh_rnd.Next(3, 30);
                Console.SetCursorPosition(ZhukX, ZhukY);
                Console.Write(" \\ o /");
                Console.SetCursorPosition(ZhukX, ZhukY + 1);
                Console.Write("--(_)--");
                Console.SetCursorPosition(ZhukX, ZhukY + 2);
                Console.Write(" /   \\");
                isExist = 1;
            }
        }
    }
}
