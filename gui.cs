using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Tails2012timOperatingSystem
{
    internal class Gui
    {
        private static VBECanvas screen;

        public static void InitGui()
        {
            // Set the graphics mode to a higher resolution and color depth using VBECanvas
            screen = new VBECanvas();
            screen.Mode = new Mode(1024, 768, ColorDepth.ColorDepth32); // Set desired resolution and color depth
            startupLoader("Starting PS2 Mouse");
            Cosmos.HAL.Global.PS2Controller.Initialize(true);
            screen.Clear(Color.LightBlue);
            startupLoader("[OK] Started PS2 Mouse");
            screen.Display();

            // Set the Mouse Manager boundaries to the screen size
            MouseManager.ScreenWidth = screen.Mode.Width;
            MouseManager.ScreenHeight = screen.Mode.Height;

            MainGui();
        }

        private static void MainGui()
        {
            while (true)
            {
                screen.Clear(Color.Aqua);

                // Drawing operations
                screen.DrawFilledRectangle(Color.LightBlue, 10, 10, 20, 20);

                // Drawing cursor with lines
                int mouseX = (int)MouseManager.X;
                int mouseY = (int)MouseManager.Y;
                if (mouseX < 0)
                {
                    mouseX = 0;
                    mouseY = 0;
                }
                if (mouseY < 0)
                {
                    mouseX = 0;
                    mouseY = 0;
                }
                if (mouseX > 1024)
                {
                    mouseX = 100;
                    mouseY = 100;
                }
                if (mouseY > 768)
                {
                    mouseX = 100;
                    mouseY = 100;
                }
                screen.DrawString("X: " + mouseX + " Y: " + mouseY, PCScreenFont.Default, Color.Black, 100, 100);
                screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX + 6, mouseY); // Horizontal line
                screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX, mouseY + 6); // Vertical line
                screen.DrawLine(Color.Pink, mouseX, mouseY, mouseX + 12, mouseY + 12); // Diagonal line

                // Display all the drawings
                screen.Display();
            }
        }
        private static void startupLoader(string status)
        {
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString(".",PCScreenFont.Default,Color.Blue, 400, 400);
            screen.Display();
            Cosmos.HAL.Global.PIT.Wait(1000);
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString("..", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
            Cosmos.HAL.Global.PIT.Wait(1000);
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString("...", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
            Cosmos.HAL.Global.PIT.Wait(1000);
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString("....", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
            Cosmos.HAL.Global.PIT.Wait(1000);
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString(".....", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
            Cosmos.HAL.Global.PIT.Wait(1000);
        }
    }
}