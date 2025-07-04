using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using Console = System.Console;
using Sys = Cosmos.System;

namespace guiOSbutnotmadebyai
{
    internal class guiOS
    {

           public void BeforeRun()
        {
            Console.WriteLine("guiOS v0.1 alpha");
            Console.WriteLine("Notice About The Calculator: When you click it, The screen will appear to freeze!");
            Console.WriteLine("To Unfreeze the system, enter the first number to add.");
            Console.Write("Name:");
            var name = Console.ReadLine();
            var canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Clear(Color.Aqua);
            canvas.Display();
            Sys.MouseManager.ScreenWidth = 1000;
            Sys.MouseManager.ScreenHeight = 753;
            canvas.Display();

            canvas.Display();
            var startshown = false;
            var calculatorshown = false;
            var calculatorstate = 1;
            var input = "";
            var input2 = "";
            var inputnumber = 0;
            var inputnumber2 = 0;
            var output = 0;
            while (true)
            {
                canvas.DrawFilledRectangle(Color.Tan, 10, 5, 100, 100);
                canvas.DrawFilledRectangle(Color.Green, 135, 5, 100, 100);
                canvas.DrawString("Calculator", PCScreenFont.Default, Color.Yellow, 148, 55);
                canvas.DrawString("Welcome, " + name + "!", Sys.Graphics.Fonts.PCScreenFont.Default, Color.Brown, 484, 372);
                if (startshown)
                {
                    canvas.DrawFilledRectangle(Color.Tan, 0, 710, 100, 100);
                    canvas.DrawString("Shutdown", PCScreenFont.Default, Color.Blue, 32, 744);
                }
                if (calculatorshown)
                {
                    canvas.DrawFilledRectangle(Color.Purple, 340, 240, 250, 250);
                    canvas.DrawFilledRectangle(Color.White, 421, 283, 100, 50);
                    if (calculatorstate == 1 | calculatorstate == 2 | calculatorstate == 3 | calculatorstate == 4)
                    {

                        if (input == "")
                        {
                            input = "e";
                            canvas.DrawString("Enter Number 1 to add (screen will freeze)", PCScreenFont.Default, Color.Red, 399, 256);
                            canvas.Display();
                            Console.Write("Number 1 to add:");
                            input = Console.ReadLine();

                        }
                        else if (input != "")
                        {
                            canvas.DrawString(input, PCScreenFont.Default, Color.Red, 459, 305);
                            canvas.DrawFilledRectangle(Color.Red, 419, 420, 100, 50);
                            canvas.DrawString("->", PCScreenFont.Default, Color.Blue, 461, 444);
                            try
                            {
                                inputnumber = int.Parse(input);
                            }
                            catch (Exception ex)
                            {
                                Sys.Power.Shutdown();
                            }
                        }
                        if (MouseManager.LastMouseState == MouseState.Left && MouseManager.Y >= 421 & MouseManager.Y <= 457 & MouseManager.X >= 420 & MouseManager.X <= 507)
                        {
                            calculatorstate = 2;
                            if (input2 == "")
                            {
                                input2 = "e";
                                Console.Write("Number 2 to add:");
                                input2 = Console.ReadLine();
                                try
                                {
                                    inputnumber2 = int.Parse(input2);
                                    canvas.Clear(Color.Aqua);
                                    canvas.DrawFilledRectangle(Color.Purple, 340, 240, 250, 250);
                                    canvas.DrawFilledRectangle(Color.White, 421, 283, 100, 50);
                                    canvas.DrawString(input2, PCScreenFont.Default, Color.Red, 459, 305);
                                    canvas.DrawFilledRectangle(Color.Red, 419, 420, 100, 50);
                                    canvas.DrawString("->", PCScreenFont.Default, Color.Blue, 461, 444);
                                    canvas.Display();

                                }
                                catch (Exception ex)
                                {
                                    Sys.Power.Shutdown();
                                }
                            }
                            else if (calculatorstate == 2)
                            {
                                tailsmath math = new tailsmath();
                                output = math.add(inputnumber, inputnumber2);
                                calculatorstate = 3;
                                canvas.Clear(Color.Aqua);
                                canvas.DrawFilledRectangle(Color.Purple, 340, 240, 250, 250);
                                canvas.DrawString(output.ToString(), PCScreenFont.Default, Color.Red, 459, 305);
                                canvas.DrawFilledRectangle(Color.Red, 419, 420, 100, 50);
                                canvas.DrawString("->", PCScreenFont.Default, Color.Blue, 461, 444);
                            }
                            else if (calculatorstate == 3)
                            {
                                canvas.Clear(Color.Aqua);
                                canvas.DrawFilledRectangle(Color.Purple, 340, 240, 250, 250);
                                canvas.DrawString("Press the arrow again to exit.", PCScreenFont.Default, Color.Red, 459, 305);
                                canvas.DrawFilledRectangle(Color.Red, 419, 420, 100, 50);
                                canvas.DrawString("->", PCScreenFont.Default, Color.Blue, 461, 444);
                                calculatorstate = 4;
                                canvas.Display();
                            }
                            else if (calculatorstate == 4)
                            {
                                canvas.Clear(Color.Aqua);
                                calculatorshown = false;
                                canvas.Display();
                            }

                        }
                    }
                }
                canvas.DrawLine(Color.Blue, (int)Cosmos.System.MouseManager.X, (int)Cosmos.System.MouseManager.Y,
                (int)Cosmos.System.MouseManager.X + 6, (int)Cosmos.System.MouseManager.Y);
                canvas.DrawLine(Color.Blue, (int)Cosmos.System.MouseManager.X, (int)Cosmos.System.MouseManager.Y,
                    (int)Cosmos.System.MouseManager.X, (int)Cosmos.System.MouseManager.Y + 6);
                canvas.DrawLine(Color.Pink, (int)Cosmos.System.MouseManager.X, (int)Cosmos.System.MouseManager.Y,
                    (int)Cosmos.System.MouseManager.X + 12, (int)Cosmos.System.MouseManager.Y + 12);
                canvas.DrawString("Start", PCScreenFont.Default, Color.Brown, 57, 57);

                if (MouseManager.LastMouseState == (MouseState.Left))
                {
                    canvas.DrawString("X: " + MouseManager.X + " Y: " + MouseManager.Y, PCScreenFont.Default, Color.Black, 20, 200);
                    canvas.Display();
                    if (MouseManager.LastMouseState == MouseState.Left && MouseManager.Y >= 8 & MouseManager.Y <= 92 & MouseManager.X >= 10 & MouseManager.X <= 99)
                    {
                        startshown = true;
                    }
                    else if (startshown && MouseManager.LastMouseState == MouseState.Left && MouseManager.Y >= 711 & MouseManager.Y <= 752 & MouseManager.X >= 0 & MouseManager.X <= 86)
                    {
                        return;
                    }
                    else if (MouseManager.LastMouseState == MouseState.Left && MouseManager.Y >= 6 & MouseManager.Y <= 90 & MouseManager.X >= 135 & MouseManager.X <= 222)
                    {
                        calculatorshown = true;
                    }
                }

                canvas.Display();
                canvas.Clear(Color.Aqua);


            }
        }
          public void Run()
        {

        }
    }
}
