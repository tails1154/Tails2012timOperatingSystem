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
using Console = System.Console;
using guiOSbutnotmadebyai;


namespace Tails2012timOperatingSystem
{
    internal class Gui
    {
        private static VBECanvas screen;
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
            // Initialize graphics
            screen = new VBECanvas();
            screen.Mode = new Mode(1024, 768, ColorDepth.ColorDepth32);

            // Loading sequence
            startupPercent(30, "Tails2012tim OS Is Loading Files (VFS)");
            VirtualFileSystem = new CosmosVFS();

            startupPercent(60, "Tails2012tim OS Is Loading Files (Drivers)");
            drivers.initDrivers();

            startupLoader("Tails2012tim OS");
            screen.Clear(Color.LightBlue);
            screen.Display();

            startupPercent(100, "Tails2012tim OS Is Loading Files (Starting GUI)");

            // Set mouse boundaries
            MouseManager.ScreenWidth = screen.Mode.Width;
            MouseManager.ScreenHeight = screen.Mode.Height;

            MainGui();
        }

        private static void MainGui()
        {
            while (true)
            {
                screen.Clear(Color.Aqua);

                // Handle input
                keyboardhandle.updateKeyboardKey();

                // Draw UI elements
                DrawMainUI();

                // Handle settings window
                if (isSettingsOpen)
                {
                    DrawSettingsWindow();
                    HandleSettingsInteraction();
                }

                // Handle format dialog
                if (isFormatDialogOpen)
                {
                    HandleFormatDialog();
                }

                // Draw mouse cursor
                DrawMouseCursor();

                // Update display
                screen.Display();
            }
        }

        private static void DrawMainUI()
        {
            // Draw background elements
            screen.DrawFilledRectangle(Color.LightBlue, 10, 10, 20, 20);
            screen.DrawFilledRectangle(Color.DarkBlue, 0, 705, 1019, 767);

            // Draw time/date
            string date = $"{Cosmos.HAL.RTC.Month}/{RTC.DayOfTheMonth}/{RTC.Year}";
            string time = $"{RTC.Hour - 12}:{RTC.Minute}:{RTC.Second}";
            screen.DrawString(date, PCScreenFont.Default, Color.White, 903, 720);
            screen.DrawString(time, PCScreenFont.Default, Color.Black, 876, 743);

            // Draw settings button
            screen.DrawFilledRectangle(Color.Green, 564, 713, 100, 50);
            screen.DrawString("Settings", PCScreenFont.Default, Color.Black, 600, 736);
            if (mouseX > 566 && mouseX < 659 && mouseY > 712 && mouseY < 757 && MouseManager.MouseState == MouseState.Left)
            {
                isSettingsOpen = true;
            }
        }

        // Add this to the class fields
        public static bool isLegacyGuiRequested = false;

        private static void DrawSettingsWindow()
        {
            // Draw settings window
            screen.DrawFilledRectangle(Color.Red, 370, 225, 400, 400);

            // Draw close button
            var closeBtnColor = isFormatDialogOpen ? Color.Gray : Color.Yellow;
            screen.DrawFilledRectangle(closeBtnColor, 741, 232, 30, 30);
            screen.DrawString("X", PCScreenFont.Default, Color.Black, 760, 236);

            // Draw format button
            var formatBtnColor = isFormatDialogOpen ? Color.Gray : Color.White;
            screen.DrawFilledRectangle(formatBtnColor, 419, 325, 200, 55);

            // Draw legacy GUI button (added below format button)
            screen.DrawFilledRectangle(Color.Purple, 419, 390, 200, 55);
            screen.DrawString("Legacy GUI", PCScreenFont.Default, Color.White, 455, 414);

            // Draw text
            screen.DrawString("Settings", PCScreenFont.Default, Color.Black, 433, 238);
            screen.DrawString("Format Drive", PCScreenFont.Default, Color.Black, 455, 349);
        }

        private static void HandleSettingsInteraction()
        {
            if (MouseManager.MouseState != MouseState.Left) return;

            // Check close button
            if (mouseX > 740 && mouseX < 767 &&
                mouseY > 233 && mouseY < 252 &&
                !isFormatDialogOpen && !isFormattingDrive)
            {
                isSettingsOpen = false;
            }
            // Check format button
            else if (mouseX > 418 && mouseX < 611 &&
                    mouseY > 326 && mouseY < 367 &&
                    !isFormatDialogOpen && !isFormattingDrive)
            {
                textboxText = "";
                isFormatDialogOpen = true;
            }
            // Check legacy GUI button (added this condition)
            else if (mouseX > 418 && mouseX < 611 &&
                    mouseY > 390 && mouseY < 445 &&
                    !isFormatDialogOpen && !isFormattingDrive)
            {
                guiOS guios = new guiOS();
                guios.BeforeRun();

            }
            // Check settings button
            else if (mouseX > 566 && mouseX < 659 &&
                    mouseY > 712 && mouseY < 757)
            {
                isSettingsOpen = true;
            }
        }
        private static void HandleFormatDialog()
        {
            // Draw dialog
            screen.DrawFilledRectangle(Color.Gold, 386, 60, 400, 200);
            string driveInfo = $"Enter the drive number & push enter. There are {drivers.volumes.Length} drive(s)";
            screen.DrawString(driveInfo, PCScreenFont.Default, Color.Black, 397, 68);

            keyboardhandle.updateKeyboardKey();

            // Handle input
            if (keyboardhandle.key == "Backspace" && textboxText.Length > 0)
            {
                textboxText = textboxText.Remove(textboxText.Length - 1);
            }
            else if (keyboardhandle.key == "Escape")
            {
                isFormatDialogOpen = false;
                isFormatDialogEnterPressed = false;
                isSettingsOpen = false;
                textboxText = "";
            }
            else if (!isFormatDialogEnterPressed && keyboardhandle.key == "Enter")
            {
                isFormatDialogEnterPressed = true;
            }
            else if (isFormatDialogEnterPressed)
            {
                HandleFormatConfirmation();
            }
            else if (!string.IsNullOrEmpty(keyboardhandle.key))
            {
                textboxText += keyboardhandle.key;
            }

            // Show current input
            screen.DrawString(textboxText, PCScreenFont.Default, Color.White, 555, 109);
            keyboardhandle.key = "";
        }

        private static void HandleFormatConfirmation()
        {
            screen.DrawString("Format this Drive?", PCScreenFont.Default, Color.Red, 555, 109);
            screen.Display();

            keyboardhandle.updateKeyboardKey();

            if (keyboardhandle.key == "Enter")
            {
                FormatDrive();
            }
            else if (keyboardhandle.key == "Escape")
            {
                isFormatDialogEnterPressed = false;
            }
        }

        private static void FormatDrive()
        {
            isFormattingDrive = true;

            try
            {
                startupPercent(0, "Getting Device Info");
                Cosmos.HAL.Global.PIT.Wait(1000);

                VirtualFileSystem.GetVolume(textboxText + @":\");
                var cosmosVFS = new CosmosVFS();
                var textboxTextInt = int.Parse(textboxText);
                var disk = new Disk(BlockDevice.Devices[textboxTextInt]);

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
            }
            catch (Exception ex) when (!string.IsNullOrEmpty(ex.Message))
            {
                startupLoader($"Error while formatting. Error: {ex.Message}");
            }
            finally
            {
                isFormattingDrive = false;
            }
        }

        private static void DrawMouseCursor()
        {
            // Display mouse coordinates
            screen.DrawString($"X: {mouseX} Y: {mouseY}", PCScreenFont.Default, Color.Black, 100, 100);

            // Draw cursor
            screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX + 6, mouseY);      // Horizontal
            screen.DrawLine(Color.Blue, mouseX, mouseY, mouseX, mouseY + 6);      // Vertical
            screen.DrawLine(Color.Pink, mouseX, mouseY, mouseX + 12, mouseY + 12); // Diagonal
        }

        public static void startupLoader(string status)
        {
            for (int i = 1; i <= 5; i++)
            {
                screen.Clear(Color.Black);
                screen.DrawString(status, PCScreenFont.Default, Color.Green, 400, 300);
                screen.DrawString(new string('.', i), PCScreenFont.Default, Color.Blue, 400, 400);
                screen.Display();
                Cosmos.HAL.Global.PIT.Wait(500);
            }
        }

        public static void startupPercent(int percent, string context)
        {
            screen.Clear(Color.Black);
            screen.DrawString(context, PCScreenFont.Default, Color.Green, 400, 300);
            screen.DrawString($"{percent}%", PCScreenFont.Default, Color.Blue, 400, 400);
            screen.Display();
        }

        public static int getMouseX() => mouseX;
        public static int getMouseY() => mouseY;
        public static void setMouseX(int x) => mouseX = x;
        public static void setMouseY(int y) => mouseY = y;
    }
}
