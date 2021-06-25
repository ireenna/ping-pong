using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinger.GameElements
{
    class BotPlayer : Player
    {
        public static IReadOnlyDictionary<Level, int> MissProbability = new Dictionary<Level, int>()
        {
            {Level.Easy, 30},
            {Level.Normal, 20},
            {Level.Hard, 5}
        };

        public BotPlayer(int _posX, int _posY) : base(_posX, _posY)
        {
        }
        public void Play(Ball ball)//искуственный интелект
        {
            Random ran = new Random();

            if (ball.VelX > 0 && ran.Next(0, 100) > MissProbability[GameSettings.Lvl])  //если мяч летит в сторону ИИ
            {
                if (PosY + LineSize[GameSettings.Lvl] / 2 > ball.PosY && PosY - 1 >= Console.WindowTop)
                    PosY--;
                else if (PosY + LineSize[GameSettings.Lvl] / 2 < ball.PosY && PosY + LineSize[GameSettings.Lvl] + 1 <= Console.WindowHeight)
                    PosY++;
            }
        }
    }
}
