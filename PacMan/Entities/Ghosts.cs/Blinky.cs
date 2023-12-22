using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Entities.Ghosts.cs
{
    public class Blinky : GhostBase
    {
        public Blinky(int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.speed = 2;
            this.movementMode = Modes.SCATTER;
            this.direction = Direction.RIGHT;
            this.scatterTargetTile = TileMap.GetInstance().Tiles[24, 0];
            this.path = $"C:\\Users\\hp\\Source\\Repos\\PacMan\\PacMan\\Assets\\EntityAssets\\GhostAssets\\";
            this.fileName = "blinky_test.png";
            this.texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, this.path + this.fileName);
        }

        protected override void Chase(Tile targetTile)
        {
            this.ChangeDirectionBasedOnTarget(targetTile);
        }

        protected override void Frightened(Tile targetTile)
        {
            throw new NotImplementedException();
        }

        protected override void Scatter()
        {
            this.ChangeDirectionBasedOnTarget(this.scatterTargetTile);
        }
    }
}
