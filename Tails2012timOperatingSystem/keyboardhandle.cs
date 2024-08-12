using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.ScanMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tails2012timOperatingSystem
{
    internal class keyboardhandle
    {
        public static string key = "";
        public static void updateKeyboardKey()
        {
            // This function was used from aura os
            KeyEvent keyEvent;
            while (Cosmos.System.KeyboardManager.TryReadKey(out keyEvent))
            {
                if (Cosmos.System.KeyboardManager.ControlPressed && Cosmos.System.KeyboardManager.AltPressed && keyEvent.Key == ConsoleKeyEx.Delete)
                {
                    key = "";
                    Gui.startupLoader("Shutting Down");
                    Cosmos.HAL.Power.ACPIShutdown();
                    continue;
                }
                else if (Cosmos.System.KeyboardManager.AltPressed && keyEvent.Key == ConsoleKeyEx.F4)
                {
                    key = "";
                    continue;
                }
                else if (keyEvent.Key == ConsoleKeyEx.LWin)
                {
                    key = "";
                    Gui.startupLoader("Start menu will be added in another version!");
                }
                else if (keyEvent.Key == ConsoleKeyEx.Enter)
                {
                    key = "Enter";
                }
                else if (keyEvent.Key == ConsoleKeyEx.Escape)
                {
                    key = "Escape";
                }
                else if (keyEvent.Key == ConsoleKeyEx.Backspace)
                {
                    key = "Backspace";
                }
                else
                {
                    //if (key != keyEvent.KeyChar.ToString())
                   // {
                        key = keyEvent.KeyChar.ToString();
                    //}
                }
            }
        }
    }
}