using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Entities.Ghosts
{
    public class Clyde : GhostBase
    {
        public Clyde(int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.scatterTargetTile = Map.Map.GetInstance().Tiles[0, 31];
            this.fileName = "clyde_test.png";
            this.texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, this.path + this.fileName);
        }

        protected override void Chase(Player.Player player)
        {
            double distance = this.CalcGreedyValue(this.tileLocation, player.TileLocation);

            if (distance > 96) { this.ChangeDirectionBasedOnTarget(player.TileLocation); }
            else { this.ChangeDirectionBasedOnTarget(this.scatterTargetTile); }
        }
    }
}
