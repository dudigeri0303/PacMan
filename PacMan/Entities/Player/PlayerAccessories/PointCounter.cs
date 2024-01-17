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
            if(this.points % 244 == 0) 
            {
                Game1.LevelUp();
            }
        }

        public void DrawPoints() 
        {
            Game1._spriteBatch.DrawString(Game1._basicFont, this.points.ToString(), this.pointPosition, Color.White);
        }
    }
}
