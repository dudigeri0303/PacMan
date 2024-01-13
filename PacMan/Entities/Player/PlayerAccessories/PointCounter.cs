using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PacMan.Entities.Player.PlayerAccessories
{
    public class PointCounter
    {
        private Vector2 pointPosition;
        private int points;
        public int Points { get { return points; } }    

        public PointCounter() 
        {
            this.points = 0;
            this.pointPosition = new Vector2(28, 0);
        }

        public void IncrasePoints() 
        {
            this.points++;
        }

        public void DrawPoints() 
        {
            Game1._spriteBatch.DrawString(Game1._basicFont, this.points.ToString(), this.pointPosition, Color.White);
        }
    }
}
