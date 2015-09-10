using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimatePong;

namespace UltimatePong
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        private static UltimatePong game;
        private static Launcher launcher;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            runLauncher();
        }

        static void runLauncher()
        {
            launcher = new Launcher();
            Application.Run(launcher);
            if (launcher.StartPressed == true) //check if StartButton is pressed
            {
                game = new UltimatePong(launcher.playerAmount, launcher.livesAmount, launcher.powerups);
                game.Run();
                runLauncher();
            }
        }


    }
#endif
}
