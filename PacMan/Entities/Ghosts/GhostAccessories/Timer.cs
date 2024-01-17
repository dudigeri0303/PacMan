using System;
using System.Diagnostics;

namespace PacMan.Entities.Ghosts.GhostAccessories
{
    public class Timer
    {
        private bool timerRunning;
        public bool TimerRunning
        {
            get { return timerRunning; }
            set { timerRunning = value; }
        }
        private bool frightenedTimerRunning;
        public bool FrightenedTimerRunning
        {
            get { return frightenedTimerRunning; }
            set { frightenedTimerRunning = value; }
        }

        private float timeElapsed;
        public float TimeEladpsed { get { return timeElapsed; } }

        private float frightenedTimeElapsed;
        public float FrightenedTimeElapsed 
        { 
            get { return frightenedTimeElapsed; }
            set { frightenedTimeElapsed = value; }
        }
        public Timer(bool timerRunning)
        {
            this.timerRunning = timerRunning;
            this.frightenedTimerRunning = false;
            this.timeElapsed = (float)0;
            this.frightenedTimeElapsed = (float)0;
        }

        public void IncraseTimeElapsed(float time) 
        {
            this.timeElapsed += time;
        }

        public void IncraseFrightenedTimeElapsed(float time) 
        {
            this.frightenedTimeElapsed += time;
        }

        public void ChangeGhostModeBasedOnTime(GhostBase ghost) 
        {
            if (this.timerRunning) 
            {
                if (((int)Math.Floor(this.timeElapsed) == 7 || (int)Math.Floor(this.timeElapsed) == 34 || (int)Math.Floor(this.timeElapsed) == 59 || (int)Math.Floor(this.timeElapsed) == 84) & ghost.MovementMode != Modes.CHASE)
                {
                    ghost.MovementMode = Modes.CHASE;
                }
                else if (((int)Math.Floor(this.timeElapsed) == 27 || (int)Math.Floor(this.timeElapsed) == 54 || (int)Math.Floor(this.timeElapsed) == 79) & ghost.MovementMode != Modes.SCATTER)
                {
                    ghost.MovementMode = Modes.SCATTER;
                }
                else if ((int)Math.Floor(this.timeElapsed) >= 84)
                {
                    ghost.MovementMode = Modes.CHASE;    
                }
            }
        }
    }
}

  
