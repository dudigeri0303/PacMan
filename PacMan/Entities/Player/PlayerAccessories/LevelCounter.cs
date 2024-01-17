using Microsoft.Xna.Framework;

namespace PacMan.Entities.Player.PlayerAccessories
{
    public  class LevelCounter
    {
        private int level;
        public int Level { get { return level; } }
        private Vector2 position;

        public LevelCounter() 
        {
            this.level = 1;
            this.position = new Vector2(504, 816);
        }

        public void IncraseLevel() 
        {
            this.level++;
        }

        public void DrawLevel() 
        {
            Game1._spriteBatch.DrawString(Game1._basicFont,"Level" + " " + this.level.ToString(), this.position, Color.White);
        }
    }
}
