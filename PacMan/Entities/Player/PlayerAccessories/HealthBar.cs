using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacMan.Entities.Player.PlayerAccessories
{
    public class HealthBar
    {
        private int healthPoints;
        public int HealthPoints { get { return healthPoints; } }
        private List<Rectangle> healthPointRects;
        private Texture2D healtPointIcon;

        public HealthBar() 
        {
            this.healthPoints = 3;
            this.healtPointIcon = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToPlayerImages + "health_icon.png");
            this.healthPointRects = new List<Rectangle>()
            {
                new Rectangle(0, 816, 24, 24),
                new Rectangle(24, 816, 24, 24),
                new Rectangle(48, 816, 24, 24)
            };
        }

        public void DecreaseHealth() 
        {
            this.healthPoints--;
        }

        public void DrawHealth()
        {
            for (int i = 0; i < this.healthPoints; i++)
            {
                Game1._spriteBatch.Draw(this.healtPointIcon, this.healthPointRects[i], Color.White);
            }
        }
    }
}
