using Cosmos.System;
using Cosmos.System.Graphics;
using System;
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
            screen.Clear(Color.LightBlue);
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

                screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX + 6, mouseY); // Horizontal line
                screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX, mouseY + 6); // Vertical line
                screen.DrawLine(Color.Pink, mouseX, mouseY, mouseX + 12, mouseY + 12); // Diagonal line

                // Display all the drawings
                screen.Display();
            }
        }
    }
}