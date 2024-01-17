using Microsoft.Xna.Framework.Graphics;

namespace PacMan.Map
{
    public class Pellet : Tile
    {
        public Pellet(int x, int y, int width, int height, string name, int i, int j, string path) : base(x, y, width, height, name, i, j, path)
        {

        }

        public void WhenEaten() 
        {
            string path = Game1.PathToPelletImages;
            this.name = $"empty_pellet.png";
            this.texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, path + this.name);
        }
    }
}
