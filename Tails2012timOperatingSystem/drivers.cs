using Cosmos.HAL;
using Cosmos.HAL.Drivers.USB;
using Cosmos.System.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tails2012timOperatingSystem
{
    internal class drivers
    {
        public static Array volumes;
        public static void initDrivers()
        {
            loadDriverLaptopPS2();
        }
        private static void loadDriverLaptopPS2()
        {
            Gui.startupPercent(90, "Tails2012tim OS is loading drivers (Network)");
            Cosmos.HAL.Network.NetworkInit.Init();
            Gui.startupPercent(95, "Tails2012tim OS is locating disks...");
            try
            {
                volumes = Gui.VirtualFileSystem.GetVolumes().ToArray();
            }
            catch (Exception ex)
            {
                Gui.startupLoader(@"Could not locate disk (0:\)");
                Gui.startupLoader("Would you like to load another disk? (y/n)");
                var readtest = Console.Read();
                Gui.startupLoader(readtest.ToString());
            }
        }
    }
}
