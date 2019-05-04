using System;

namespace Dyflissu.DesktopGL
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DyflissuGame())
                game.Run();
        }
    }
}