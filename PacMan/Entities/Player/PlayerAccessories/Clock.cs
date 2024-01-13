using System;
using Microsoft.Xna.Framework;

namespace PacMan.Entities.Player.PlayerAccessories
{
    public class Clock
    {
        private float timeElapsed;
        private int mins;
        private int secs;

        private Vector2 timePosition;

        public Clock() 
        {
            this.timeElapsed = 0;
            this.mins = 0;
            this.secs = 0;

            this.timePosition = new Vector2(504, 0);
        }

        public void UpdateTime(float time)
        {
            this.timeElapsed += time;

            this.mins = (int)Math.Floor((double)this.timeElapsed / 60);
            this.secs = (int)Math.Floor((double)this.timeElapsed - this.mins * 60);
        }

        public void DrawTime() 
        {
            Game1._spriteBatch.DrawString(Game1._basicFont, this.mins.ToString() + ":" + this.secs.ToString(), this.timePosition, Color.White);
        }
    }
}
