using System;
using System.Threading;
using Pinger.GameElements;
using RabbitMQ.Client;
using RabbitMQ.Wrapper.Interfaces;
using RabbitMQ.Wrapper.QueueServices;

namespace Pinger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 35);
            Console.CursorVisible = false;
            Console.Title = "Ping Pong";
            ConsoleKeyInfo k_dif;//= Console.ReadKey(true);

            Console.SetCursorPosition(55, 13);
            Console.WriteLine("PING PONG");
            Console.SetCursorPosition(42, 15);
            Console.WriteLine("Choose the difficulty from 1 to 3:");
            
            int kol = 0;

            Console.SetCursorPosition(56, 16);
            Console.WriteLine((int)Level.Easy + " level");
            Console.SetCursorPosition(49, 18);
            Console.WriteLine("(press Enter to start)");
            while (kol == 0)
            {
                k_dif = Console.ReadKey(true);

                if (k_dif.Key == ConsoleKey.RightArrow && (int)GameSettings.Lvl < 3)
                {
                    GameSettings.Lvl++;
                    Console.SetCursorPosition(56, 16);
                    Console.Write((int)GameSettings.Lvl);
                }
                if (k_dif.Key == ConsoleKey.LeftArrow && (int)GameSettings.Lvl > 1)
                {
                    GameSettings.Lvl--;
                    Console.SetCursorPosition(56, 16);
                    Console.Write((int)GameSettings.Lvl);
                }
                if (k_dif.Key == ConsoleKey.Enter)
                {
                    kol++;
                }
            }

            IConnectionFactory connect = new ExtendedConnectionFactory();
            IMessageProducerScopeFactory producer1 = new MessageProducerScopeFactory(connect);
            IMessageConsumerScopeFactory consumer1 = new MessageConsumerScopeFactory(connect);
            QueueService ms1 = new QueueService(producer1, consumer1);

            while (true)
            {
                Player p1 = new RealPlayer(Console.WindowLeft + 1, Console.WindowHeight / 2 - RealPlayer.LineSize[GameSettings.Lvl] / 2);
                Player p2 = new BotPlayer(Console.WindowWidth - 2, Console.WindowHeight / 2 - RealPlayer.LineSize[GameSettings.Lvl] / 2);
                Ball ball = new Ball();
                Zhuk zhuk = new Zhuk();
                Zhuk.isExist = 0;

                while (p1.BallsMissed < GameSettings.Max_score && p2.BallsMissed < GameSettings.Max_score)
                {
                    (p1 as RealPlayer).Play();
                    (p2 as BotPlayer).Play(ball);
                    ball.Update(ref p1, ref p2, ms1);
                    p1.Draw();
                    p2.Draw();
                    zhuk.Draw();
                    ball.Draw();

                    Display.Score = p2.BallsMissed.ToString() + "  :  " + p1.BallsMissed.ToString();
                    Display.Draw();
                    Thread.Sleep(1);
                }

                if (p1.BallsMissed == GameSettings.Max_score)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 2);
                    Console.WriteLine("GAME OVER");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 6, Console.WindowHeight / 2);
                    Console.WriteLine("COMPUTER WON!");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 12, Console.WindowHeight / 2 + 2);
                    Console.WriteLine("(Press Enter to restart)");
                }
                else
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("YOU WON!");
                    
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - 12, Console.WindowHeight / 2 + 2);
                Console.WriteLine("(Press Enter to restart)");
                while (true)
                {
                    ConsoleKeyInfo k = Console.ReadKey(true);
                    if (k.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }

            }
        }
    }
}
