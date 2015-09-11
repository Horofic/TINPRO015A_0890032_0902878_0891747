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
            launcher = new Launcher(2,3,true); //give default values
            Application.Run(launcher); //start launcher first time
            runLauncher();
        }

        static void runLauncher() //this keeps looping until launcher is closed
        {
            
            if (launcher.StartPressed == true) //check if StartButton is pressed > start game. Else exit program.
            {
                game = new UltimatePong(launcher.playerAmount, launcher.livesAmount, launcher.powerups);
                game.Run();

                launcher = new Launcher(launcher.playerAmount, launcher.livesAmount, launcher.powerups);
                Application.Run(launcher);

                runLauncher();
            }
        }


    }
#endif
}
