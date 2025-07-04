﻿using System;
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
using Cosmos.HAL.Drivers.Video;
using System.IO.Compression;
using guiOSbutnotmadebyai;


namespace Tails2012timOperatingSystem
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("Tails2012tim Operating System (ToS)");
            Console.WriteLine("Kernel Version 0.1");
            //Console.WriteLine("Would You Like to boot legacy guiOS? (yes/n)");
            // var choice = Console.ReadLine();
            var choice = "no";
            Console.WriteLine("Tails2012tim Operating System (ToS)");
            Console.WriteLine("Loading Non-Existant Drivers...");
            //put drivers here if I decide to make any
            Console.WriteLine("Loaded Drivers");
            Console.WriteLine("Starting GUI...");

            guiOS guios = new guiOS();
            guios.BeforeRun();

            VBECanvas screen = new VBECanvas(new Mode(1000, 753, ColorDepth.ColorDepth32));

            //var screen = FullScreenCanvas.GetFullScreenCanvas();
            Gui.InitGui();

        }

        protected override void Run()
        {

        }
    }
}
