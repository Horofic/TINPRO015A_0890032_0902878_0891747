﻿using System;
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
        int time;

        public Timer(int time)
        {
            this.time = time;
            savedGameTime = 0;
            elapsedTime = 0;
            done = false;
        }

        public bool getDone()
        {
            if (elapsedTime == time)
            {
                done = true;
            }
            return done;
        }

        public void runTimer(GameTime gameTime)
        {
            if (savedGameTime < (int)gameTime.TotalGameTime.TotalSeconds && done == false)
            {
                savedGameTime = (int)gameTime.TotalGameTime.TotalSeconds;
                elapsedTime++;
            }
        }

        public int getElapsedTime()
        {
            return elapsedTime;
        }

        public void reset()
        {
            elapsedTime = 0;
            done = false;
        }


        
    }
}
