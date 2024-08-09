using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tails2012timOperatingSystem
{
    internal class gui
    {
        public static void InitGui()
        {
            var screen = FullScreenCanvas.GetFullScreenCanvas();
            VGAScreen.SetGraphicsMode(Cosmos.HAL.Drivers.Video.VGADriver.ScreenSize.Size640x480, ColorDepth.ColorDepth8);
            screen.Clear(Color.LightBlue);
            screen.Display();
            MouseManager.ScreenWidth = 640;
            MouseManager.ScreenHeight = 480;
            mainGui();
        }
        private static void mainGui()
        {
            //var screen = FullScreenCanvas.GetFullScreenCanvas();
            var screen = FullScreenCanvas.GetFullScreenCanvas();
            screen.Clear(Color.Aqua);
            screen.Display();
            MouseManager.ScreenWidth = 1000;
            MouseManager.ScreenHeight = 753;
            screen.Display();
            screen.Clear(Color.LightBlue);
            screen.DrawRectangle(Color.LightBlue, 10, 10, 20, 20);
            screen.DrawLine(Color.Blue, (int)Cosmos.System.MouseManager.DeltaX, (int)Cosmos.System.MouseManager.DeltaY,
                    (int)Cosmos.System.MouseManager.DeltaX + 6, (int)Cosmos.System.MouseManager.DeltaY);
            screen.DrawLine(Color.Blue, (int)Cosmos.System.MouseManager.DeltaX, (int)Cosmos.System.MouseManager.DeltaY,
                (int)Cosmos.System.MouseManager.DeltaX, (int)Cosmos.System.MouseManager.DeltaY + 6);
            screen.DrawLine(Color.Pink, (int)Cosmos.System.MouseManager.DeltaX, (int)Cosmos.System.MouseManager.DeltaY,
                (int)Cosmos.System.MouseManager.DeltaX + 12, (int)Cosmos.System.MouseManager.DeltaY + 12);
            screen.Display();
            screen.Display();
        }
    }
}
