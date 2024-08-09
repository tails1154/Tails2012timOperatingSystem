using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core;
using Cosmos.System.Network;
using Cosmos.HAL.Drivers.Audio;
using Cosmos.System.Audio;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;
using Console = System.Console;

namespace Tails2012timOperatingSystem
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("Tails2012tim Operating System (ToS)");
            Console.WriteLine("Kernel Version 0.1");
            Console.WriteLine("Would You Like to boot legacy guiOS? (yes/n)");
            var choice = Console.ReadLine();
            if (choice == "yes")
            {
                guiOSbutnotmadebyai.guiOS guiOS = new guiOSbutnotmadebyai.guiOS();
                guiOS.BeforeRun();
            }
            Console.WriteLine("Tails2012tim Operating System (ToS)");
            Console.WriteLine("Loading Non-Existant Drivers...");
            //put drivers here if I decide to make any
            Console.WriteLine("Loaded Drivers");
            Console.WriteLine("Starting GUI...");
            //var screen = FullScreenCanvas.GetFullScreenCanvas();

        }

        protected override void Run()
        {
            var screen = FullScreenCanvas.GetFullScreenCanvas();
            screen.Clear(Color.Aqua);
            MouseManager.ScreenWidth = 1000;
            MouseManager.ScreenHeight = 753;
            screen.DrawRectangle(Color.Black, 10, 10, 20, 20);
            screen.DrawRectangle(Color.Red, (int)MouseManager.X, (int)MouseManager.Y, 10, 10);
            screen.Display();

        }
    }
}