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

namespace PacMan.Entities.Ghosts
{
    public class Blinky : GhostBase
    {
        public Blinky(int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.timerRunning = true;
            this.movementMode = Modes.SCATTER;

            this.scatterTargetTile = Map.Map.GetInstance().Tiles[24, 0];
            this.houseTargetTile = Map.Map.GetInstance().Tiles[14, 18];
            
            this.fileName = "blinky_test.png";
            this.texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, this.path + this.fileName);
        }

        protected override void Chase(Player.Player player)
        {
            Tile targetTile = player.TileLocation;
            this.ChangeDirectionBasedOnTarget(targetTile);
        }
    }
}
