using Microsoft.Xna.Framework.Graphics;
using PacMan.Entities.Ghosts.GhostAccessories;
using PacMan.Entities.Player;
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
            this.movementMode = Modes.IDLEINHOUSE;

            this.scatterTargetTile = Map.Map.GetInstance().Tiles[0, 31];
            this.houseTargetTile = Map.Map.GetInstance().Tiles[14, 17];
            this.startTargetTile = Map.Map.GetInstance().Tiles[13, 14];

            this.fileName = "clyde_test.png";
            this.texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, this.path + this.fileName);
            
            this.timer = new Timer(false);
        }


        protected override void IdleInHouse(Player.Player player, Blinky blinky)
        {
            this.nextDirection = Direction.NONE;

            if (player.Points > 72 & this.MovementMode == Modes.IDLEINHOUSE)
            {
                this.AllowDoor = true;
                this.MovementMode = Modes.START;
            }
        }

        protected override void Chase(Player.Player player)
        {
            double distance = this.CalcGreedyValue(this.tileLocation, player.TileLocation);

            if (distance > 96) { this.ChangeDirectionBasedOnTarget(player.TileLocation); }
            else { this.ChangeDirectionBasedOnTarget(this.scatterTargetTile); }
        }
    }
}
