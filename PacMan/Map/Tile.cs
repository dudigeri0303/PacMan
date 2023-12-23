using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Map
{
    public class Tile
    {
        private int x, y, width, height;
        private Vector2 position;
        protected Rectangle rectangle;
        public Rectangle Rect { get { return rectangle; } }
        protected Texture2D texture;
        protected string name;
        public int i, j;
        public string Name { 
            get { return name; } 
            set { name = value; }
        }

        public Tile(int x, int y, int width, int height, string name, int i, int j, string path)
        {
            this.i = i;
            this.j = j;
            this.name = name;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.position = new Vector2(this.x, this.y);
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, this.width, this.height);
            this.texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, path + this.name);
        }
        public void DrawTile()
        {
            Game1._spriteBatch.Draw(this.texture, this.rectangle, Color.White);
        }
    }
}
