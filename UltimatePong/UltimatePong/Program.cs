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
        private static Form1 launcher;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            game = new UltimatePong();
            launcher = new Form1(game);
            Application.Run(launcher);

            if (launcher.pressed==true)
            game.Run();

        }

        public static void startPressed()
        {
            launcher.pressed = true;
        }
    }
#endif
}
