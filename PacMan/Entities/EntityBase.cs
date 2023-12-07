using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacMan.Entities
{
    public abstract class EntityBase
    {
        protected int x, y, width, height;
        protected Vector2 position;
        protected Rectangle rectangle;
        protected Texture2D texture;
        protected string path;
        protected string fileName;

        public EntityBase(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.position = new Vector2((float)x, (float)y);
            this.rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, width, height);
        }


        public void Draw() 
        {
            Game1._spriteBatch.Draw(this.texture, this.rectangle, Color.White);
        }
    }
}
