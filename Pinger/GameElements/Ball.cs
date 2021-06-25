using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Pinger.GameElements;

namespace Pinger
{
    public class Ball
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int VelX { get; private set; }
        public int VelY { get; private set; }

        public Ball()
        {
            Reset();
        }

        public void Reset()//ставим мяч по центру
        {
            Console.Clear();
            PosX = Console.WindowWidth / 2;
            PosY = Console.WindowHeight / 2;
            VelX = -1;
            VelY = 1;
        }

        public void Update(ref Player p1, ref Player p2, QueueService qService)
        {
            Console.SetCursorPosition(PosX, PosY);
            Console.Write(' ');  //очищаю пред. позицию мяча
            PosX += VelX;
            PosY += VelY;

            if (PosY <= Console.WindowTop || PosY >= Console.WindowHeight - 1)//если мяч касается нижней точки консоли
                VelY *= -1;

            //отскок мяча от полоски
            else if ((PosX == p1.PosX) && (PosY >= p1.PosY && PosY <= p1.PosY + RealPlayer.LineSize[GameSettings.Lvl])) //|| (PosX == p2.PosX) && (PosY >= p2.PosY && PosY <= p2.PosY + RealPlayer.LineSize[GameSettings.Lvl]))
            {
                PosX -= VelX;
                VelX *= -1;
                qService.Ping("Ping " + DateTime.Now.ToString());
            }
            else if((PosX == p2.PosX) && (PosY >= p2.PosY && PosY <= p2.PosY + RealPlayer.LineSize[GameSettings.Lvl]))
            {
                PosX -= VelX;
                VelX *= -1;
            }
            if (PosX >= Zhuk.ZhukX && PosX <= (Zhuk.ZhukX + 7) && PosY >= Zhuk.ZhukY && PosY <= Zhuk.ZhukY + 3)
            {
                if (VelX == -1)
                {
                    p1.BallsMissed++;
                }
                else
                {
                    p2.BallsMissed++;
                }
                Console.SetCursorPosition(Zhuk.ZhukX, Zhuk.ZhukY);
                Console.Write("       ");
                Console.SetCursorPosition(Zhuk.ZhukX, Zhuk.ZhukY + 1);
                Console.Write("       ");
                Console.SetCursorPosition(Zhuk.ZhukX, Zhuk.ZhukY + 2);
                Console.Write("       ");
                Zhuk.isExist = 0;
            }
            else    //игрок пропустил мяч
            {
                if (PosX < p1.PosX)
                {
                    p1.MissTheBall(this);
                }
                else if (PosX > p2.PosX)
                {
                    p2.MissTheBall(this);
                }
            }
        }

        public void Draw(ConsoleColor color = ConsoleColor.White)   //рисую мяч
        {
            Console.SetCursorPosition(PosX, PosY);
            Console.ForegroundColor = color;
            Console.Write('O');
        }
    }
}
