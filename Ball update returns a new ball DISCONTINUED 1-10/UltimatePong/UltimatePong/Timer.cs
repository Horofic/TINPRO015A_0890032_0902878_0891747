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

        public void startTimer()
        {
            done = false;
        }

        public void stopTimer()
        {
            done = true;
        }

        //run this method continuosly to run the timer
        //runtimer with a given set time 
        public bool runTimer(GameTime gameTime, int time)
        {
            if (savedGameTime < (int)gameTime.TotalGameTime.TotalSeconds && done == false)
            {
                savedGameTime = (int)gameTime.TotalGameTime.TotalSeconds;
                elapsedTime++;
            }
            if (elapsedTime >= time)
            {
                done = true;
            }
            return done;
        }

        //runtimer without a set time
        public void runTimer(GameTime gameTime)
        {
            if (savedGameTime < (int)gameTime.TotalGameTime.TotalSeconds && done == false)
            {
                savedGameTime = (int)gameTime.TotalGameTime.TotalSeconds;
                elapsedTime++;
            }
        }

        //returns true if your given time matches the elapsed time
        public bool getDone(int time)
        {
            if (elapsedTime >= time)
            {
                done = true;
            }
            return done;
        }

        //returns the elapsed time
        public int getElapsedTime()
        {
            return elapsedTime;
        }

        //reset runTimer()
        public void reset()
        {
            elapsedTime = 0;
            done = false;
        }

        //set current time
        public void setTime(GameTime gameTime)
        {
            savedGameTime = (int)gameTime.TotalGameTime.Seconds;
            done = false;
        }

        //get true when given time is met. else false
        public bool getTimeDone(GameTime gameTime, int time)
        {
            if ((int)gameTime.TotalGameTime.Seconds - savedGameTime >= time)
                return true;
            else
                return false;
        }

    }
}
