using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pinger.GameElements
{
    public abstract class Player
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int BallsMissed { get; set; }

        public static IReadOnlyDictionary<Level, int> LineSize = new Dictionary<Level, int>()
        {
            {Level.Easy, 9},
            {Level.Normal, 7},
            {Level.Hard, 5}
        };

        public Player(int _posX, int _posY)
        {
            PosX = _posX;
            PosY = _posY;
            BallsMissed = 0;
        }
        public void MissTheBall(Ball ball)
        {
            ball.Draw(ConsoleColor.Red);
            BallsMissed++;
            Thread.Sleep(500);
            Zhuk.isExist = 0;
            ball.Reset();
        }
        public void Draw()
        {
            if (PosY > Console.WindowTop)//рисовать без предварительной очистки экрана, устраняя мерцание
            {
                Console.SetCursorPosition(PosX, PosY - 1);
                Console.Write(' ');//стираю пред. позицию полоски сверху
            }
            if (PosY + LineSize[GameSettings.Lvl] < Console.WindowHeight)
            {
                Console.SetCursorPosition(PosX, PosY + LineSize[GameSettings.Lvl]);
                Console.Write(' ');//стираю пред. позицию полоски снизу
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < LineSize[GameSettings.Lvl]; i++)
            {
                Console.SetCursorPosition(PosX, PosY + i);
                Console.Write('█'); //полоски
            }
        }
    }
}
