using System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            launcher = new Launcher(); //give default values
            Application.Run(launcher); //start launcher first time
            runLauncher();
        }

        static void runLauncher() //this keeps looping until launcher is closed
        {
            //check if StartButton is pressed > start game. Else exit program.
            if (launcher.StartPressed == true)
            {
                game = new UltimatePong(launcher.playerAmount, launcher.livesAmount, launcher.powerups,launcher.bounceType);
                game.Run();

                // Restart the app passing "/restart [processId]" as cmd line args
                Application.Exit();
                Process.Start(Application.ExecutablePath, "/restart" + Process.GetCurrentProcess().Id);
               
                /*
                launcher = new Launcher(launcher.playerAmount, launcher.livesAmount, launcher.powerups,launcher.bounceType);
                Application.Run(launcher);

                runLauncher();*/
            }
        }


    }
#endif
}
