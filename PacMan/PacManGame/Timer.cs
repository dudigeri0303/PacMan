using System;

namespace PacMan.PacManGame
{
    public class Timer
    {
        protected bool timerRunning;
        public bool TimerRunning
        {
            get { return timerRunning; }
            set { timerRunning = value; }
        }
        protected float timeElapsed;
        public float TimeEladpsed { get { return timeElapsed; } }
        
        public Timer(bool timerRunning)
        {
            this.timerRunning = timerRunning;
            this.timeElapsed = (float)0;
        }

        public void IncraseTimeElapsed(float time)
        {
            this.timeElapsed += time;
        }

        public void ChangeSceneBasedOnTime(GameBase gameBase)
        {
            switch (gameBase.Scene) 
            {
                case Scenes.START:
                    if ((int)Math.Floor(this.timeElapsed) == 10) 
                    {
                        this.timerRunning = false;
                        this.timeElapsed = 0;
                        gameBase.Scene = Scenes.GAME;
                    }
                    break;
                case Scenes.GAME:
                    if (gameBase.Player.HealthBar.HealthPoints == 0) 
                    {
                        gameBase.Scene = Scenes.GAMEOVER;
                    }
                    break;
                case Scenes.GAMEOVER: 
                    if(!this.timerRunning) 
                    {
                        this.timerRunning = true;
                    }

                    if ((int)Math.Floor(this.timeElapsed) == 10)
                    {
                        this.timeElapsed = 0;
                        Game1.NewGame();
                        gameBase.Scene = Scenes.START;
                    }

                    break;
            
            }
            
            
        }

    }
}
