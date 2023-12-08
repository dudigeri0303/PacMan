using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Entities.Player
{
    public class Player : EntityBase
    {
        public Player(int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.path = $"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Assets\\EntityAssets\\PlayerAssets\\";
            this.fileName = "pacman_dummy24.png";
            this.texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, this.path + this.fileName);
        }
    }
}
