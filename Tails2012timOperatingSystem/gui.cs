using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System.Drawing;
using Console = System.Console;

namespace Tails2012timOperatingSystem
{
    internal class Gui
    {
        private static VBECanvas screen;
        private static Bitmap bitmap;
        private static bool isSettingsOpen;
        private static CosmosVFS vfs;

        public static void InitGui()
        {
            // Set the graphics mode to a higher resolution and color depth using VBECanvas
            screen = new VBECanvas();
            screen.Mode = new Mode(1024, 768, ColorDepth.ColorDepth32); // Set desired resolution and color depth
            startupPercent(0, "Tails2012tim OS Is Loading Files (VFS)");
            CosmosVFS vfs = new CosmosVFS();
            startupPercent(50, "Tails2012tim OS Is Loading Files (Registering VFS");
            VFSManager.RegisterVFS(vfs);
            startupLoader("Tails2012tim OS");
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
                screen.DrawFilledRectangle(Color.DarkBlue, 0, 705, 1019, 767);
                screen.DrawString(Cosmos.HAL.RTC.Month + "/" + RTC.DayOfTheMonth + "/" + RTC.Year, PCScreenFont.Default, Color.Black, 903, 720);
                screen.DrawString(RTC.Hour + ":" + RTC.Minute + ":" + RTC.Second, PCScreenFont.Default, Color.Black, 876, 743);
                screen.DrawFilledRectangle(Color.Green, 564, 713, 100, 50);
                screen.DrawString("Settings", PCScreenFont.Default, Color.Black, 600, 736);
                if (isSettingsOpen)
                {
                    screen.DrawFilledRectangle(Color.Red, 370, 225, 400, 400);
                    screen.DrawFilledRectangle(Color.Yellow, 741, 232, 30, 30);
                    screen.DrawString("X", PCScreenFont.Default, Color.Black, 760, 236);
                }
                // Drawing cursor with lines
                int mouseX = (int)MouseManager.X;
                int mouseY = (int)MouseManager.Y;
                screen.DrawString("X: " + mouseX + " Y: " + mouseY, PCScreenFont.Default, Color.Black, 100, 100);
                screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX + 6, mouseY); // Horizontal line
                screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX, mouseY + 6); // Vertical line
                screen.DrawLine(Color.Pink, mouseX, mouseY, mouseX + 12, mouseY + 12); // Diagonal line

                // Mouse Stuff

                if (MouseManager.MouseState == MouseState.Left)
                {
                    if (isSettingsOpen)
                    {
                        if (mouseX > 740)
                        {
                            if (mouseX < 767)
                            {
                                if (mouseY > 233)
                                {
                                    if (mouseY < 252)
                                    {
                                        isSettingsOpen = false;
                                    }
                                }
                            }
                        }
                    }
                    if (mouseX > 566)
                    {
                        //startupLoader("Settings Part 1 [DEBUG]");
                        if (mouseX < 659)
                        {
                            //startupLoader("Part 2");
                            if (mouseY > 712)
                            {
                                //startupLoader("Part 3");
                                if (mouseY < 757)
                                {
                                    //startupLoader("Done!");
                                    isSettingsOpen = true;
                                }
                            }
                        }
                    }
                }


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
            Cosmos.HAL.Global.PIT.Wait(500);
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString("..", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
            Cosmos.HAL.Global.PIT.Wait(500);
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString("...", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
            Cosmos.HAL.Global.PIT.Wait(500);
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString("....", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
            Cosmos.HAL.Global.PIT.Wait(500);
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString(".....", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
            Cosmos.HAL.Global.PIT.Wait(500);
        }
        private static void startupPercent(int percent, string context)
        {
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(context, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString(percent.ToString() + "%", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
        }
    }
}