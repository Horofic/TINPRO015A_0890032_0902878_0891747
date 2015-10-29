using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    class Timer : Game
    {
        int savedGameTime;
        int elapsedTime;

        bool done;

        //Initialize Timer
        //time is the time when done becomes true
        public Timer()
        {
            savedGameTime = 0;
            elapsedTime = 0;
        }

       
        //set current time
        public void setTime(double elapsedTime)
        {
            savedGameTime = (int)elapsedTime;
            done = false;
        }

        //get true when given time is met. else false
        public bool getTimeDone(double elapsedTime, int time)
        {
            if ((int)elapsedTime - savedGameTime >= time)
                return true;
            else
                return false;
        }

    }
}
