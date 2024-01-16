using System;
using Microsoft.Xna.Framework;

namespace PacMan.PacManGame
{
    public class Clock
    {
        private float timeElapsed;
        private int mins;
        private int secs;

        private Vector2 timePosition;

        public Clock()
        {
            timeElapsed = 0;
            mins = 0;
            secs = 0;

            timePosition = new Vector2(504, 0);
        }

        public void UpdateTime(float time)
        {
            timeElapsed += time;

            mins = (int)Math.Floor((double)timeElapsed / 60);
            secs = (int)Math.Floor((double)timeElapsed - mins * 60);
        }

        public void DrawTime()
        {
            Game1._spriteBatch.DrawString(Game1._basicFont, mins.ToString() + ":" + secs.ToString(), timePosition, Color.White);
        }
    }
}
