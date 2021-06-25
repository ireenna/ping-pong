using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Pinger.GameElements;

namespace Pinger
{
    class RealPlayer : Player
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);//для получения нажатия клавиш без задержки 
        public RealPlayer(int _posX, int _posY) : base(_posX, _posY)
        {
        }

        public void Play()
        {
            if (GetAsyncKeyState((int)ConsoleKey.UpArrow) != 0 && PosY > Console.WindowTop)
                PosY--;
            else if (GetAsyncKeyState((int)(ConsoleKey.DownArrow)) != 0 && PosY + LineSize[GameSettings.Lvl] < Console.WindowHeight)
                PosY++;
        }
    }
}
