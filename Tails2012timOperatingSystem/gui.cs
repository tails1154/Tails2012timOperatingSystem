using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.HAL.BlockDevice;
using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Drawing;
using System.Linq;
using Console = System.Console;

namespace Tails2012timOperatingSystem
{
    internal class Gui
    {
        private static VBECanvas screen;
        private static Bitmap bitmap;
        private static bool isSettingsOpen;
        public static CosmosVFS VirtualFileSystem;
        public static bool isFormatDialogOpen = false;
        public static bool isFormattingDrive = false;
        public static string textboxText = "";
        public static bool isFormatDialogEnterPressed = false;
	public static int mouseX = 0;
	public static int mouseY = 0;

        public static void InitGui()
        {
            // Set the graphics mode to a higher resolution and color depth using VBECanvas
            screen = new VBECanvas();
            screen.Mode = new Mode(1024, 768, ColorDepth.ColorDepth32); // Set desired resolution and color depth
            startupPercent(30, "Tails2012tim OS Is Loading Files (VFS)");
            VirtualFileSystem = new CosmosVFS();

            //Start Filesystem
//            VFSManager.RegisterVFS(VirtualFileSystem);
            startupPercent(60, "Tails2012tim OS Is Loading Files (Drivers)");
            drivers.initDrivers();
            //startupLoader("The keyboard handler was from aura os");
            startupLoader("Tails2012tim OS");
            screen.Clear(Color.LightBlue);
            screen.Display();
            startupPercent(100, "Tails2012tim OS Is Loading Files (Starting GUI)");
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
                keyboardhandle.updateKeyboardKey();
                // Drawing operations
                screen.DrawFilledRectangle(Color.LightBlue, 10, 10, 20, 20);
                screen.DrawFilledRectangle(Color.DarkBlue, 0, 705, 1019, 767);
                screen.DrawString(Cosmos.HAL.RTC.Month + "/" + RTC.DayOfTheMonth + "/" + RTC.Year, PCScreenFont.Default, Color.White, 903, 720);
                screen.DrawString(RTC.Hour - 12 + ":" + RTC.Minute + ":" + RTC.Second, PCScreenFont.Default, Color.Black, 876, 743);
                screen.DrawFilledRectangle(Color.Green, 564, 713, 100, 50);
                screen.DrawString("Settings", PCScreenFont.Default, Color.Black, 600, 736);
                if (isSettingsOpen)
                {
                    screen.DrawFilledRectangle(Color.Red, 370, 225, 400, 400);
                    if (isFormatDialogOpen)
                    {
                        screen.DrawFilledRectangle(Color.Gray, 741, 232, 30, 30);
                        screen.DrawFilledRectangle(Color.Gray, 419, 325, 200, 55);
                        if (isFormattingDrive)
                        {
                            screen.DrawFilledRectangle(Color.Gray, 741, 232, 30, 30);
                            screen.DrawFilledRectangle(Color.Gray, 419, 325, 200, 55);
                        }
                    }
                    else
                    {
                        screen.DrawFilledRectangle(Color.Yellow, 741, 232, 30, 30);
                        screen.DrawFilledRectangle(Color.White, 419, 325, 200, 55);
                    }
                    screen.DrawString("X", PCScreenFont.Default, Color.Black, 760, 236);
                    screen.DrawString("Format Drive", PCScreenFont.Default, Color.Black, 455, 349);
                    screen.DrawString("Settings", PCScreenFont.Default, Color.Black, 433, 238);
                }

                // Drawing cursor with lines
                int mouseX = getMouseX();
                int mouseY = getMouseY();
                // Some dialog items
                if (isFormatDialogOpen)
                {
                    screen.DrawFilledRectangle(Color.Gold, 386, 60, 400, 200);
                    screen.DrawString(@"Enter the drive number & push enter. There are " + drivers.volumes.Length.ToString() + " drive(s)", PCScreenFont.Default, Color.Black, 397, 68);
                    keyboardhandle.updateKeyboardKey();
                    if (keyboardhandle.key == "Backspace")
                    {
                        if (textboxText.Length != 0)
                        {
                            textboxText = textboxText.Remove(textboxText.Length - 1);
                        }
                    }
                    else if (!isFormatDialogEnterPressed && keyboardhandle.key == "Enter")
                    {
                        isFormatDialogEnterPressed = true;
                    }
                    else if (isFormatDialogEnterPressed)
                    {
                        screen.DrawString("Format this Drive?", PCScreenFont.Default, Color.Red, 555, 109);
                        screen.Display();
                        keyboardhandle.updateKeyboardKey();
                        if ( keyboardhandle.key == "Enter")
                        {
                            isFormattingDrive = true;
                            startupPercent(0, "Getting Device Info");
                            Cosmos.HAL.Global.PIT.Wait(1000);
                            try
                            {
                                VirtualFileSystem.GetVolume(textboxText + @":\");
                                CosmosVFS cosmosVFS = new CosmosVFS();
                                var textboxTextInt = int.Parse(textboxText);
                                Disk disk = new Disk(BlockDevice.Devices[textboxTextInt]);
                                startupPercent(10, "Formatting Disk");
                                Cosmos.HAL.Global.PIT.Wait(1000);
                                disk.Clear();
                                disk.CreatePartition(1000);
                                var partitions = disk.Partitions.Count;
                                startupPercent(30, "Formatting Partition");
                                Cosmos.HAL.Global.PIT.Wait(1000);
                                disk.FormatPartition(partitions, "FAT32", true);
                                startupPercent(90, "Mounting Partition");
                                Cosmos.HAL.Global.PIT.Wait(1000);
                                disk.MountPartition(partitions);
                                startupPercent(100, "Finished!");
                                Cosmos.HAL.Global.PIT.Wait(1000);
                                isFormattingDrive = false;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "")
                                {
                                }
                                else
                                {
                                    isFormattingDrive = false;
                                    startupLoader("Error while formatting. Error: " + ex.Message);
                                }
                            }
                        }
                        else if (keyboardhandle.key == "Escape")
                        {
                            isFormatDialogEnterPressed = false;
                        }

                    }
                    else if (keyboardhandle.key == "Escape")
                    {
                        isFormatDialogOpen = false;
                        isFormatDialogEnterPressed = false;
                        isSettingsOpen = false;
                        textboxText = "";
                    }
                    else
                    {
                        textboxText = textboxText + keyboardhandle.key;
                        screen.DrawString(textboxText, PCScreenFont.Default, Color.White, 555, 109);
                    }
                    keyboardhandle.key = "";
                }
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
                                        if (!isFormatDialogOpen)
                                        {
                                            if (!isFormattingDrive)
                                            {
                                                isSettingsOpen = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (mouseX > 418 && mouseX < 611 && mouseY > 326 && mouseY < 367 && !isFormatDialogOpen)
                        {
                            if (!isFormattingDrive)
                            {
                                textboxText = "";
                                isFormatDialogOpen = true;
                            }
                        }
                    }
                    else if (mouseX > 566)
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
                screen.DrawString("X: " + mouseX + " Y: " + mouseY, PCScreenFont.Default, Color.Black, 100, 100);
                screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX + 6, mouseY); // Horizontal line
                screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX, mouseY + 6); // Vertical line
                screen.DrawLine(Color.Pink, mouseX, mouseY, mouseX + 12, mouseY + 12); // Diagonal line

                // Keyboard events (some of it taken from aura os)

               


                // Display all the drawings
                screen.Display();
            }
        }
        public static void startupLoader(string status)
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
        public static void startupPercent(int percent, string context)
        {
            screen.Clear(Color.Black);
            screen.Display();
            screen.DrawString(context, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString(percent.ToString() + "%", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
        }
	public static int getMouseX()
	{
		return mouseX;
	}
	public static int getMouseY()
	{
		return mouseY;
	}
	public static void setMouseX(int x)
	{
		mouseX = x;
	}
	public static void setMouseY(int y)
	{
		mouseY = y;
	}
    }
}
