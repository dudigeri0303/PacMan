using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PacMan.Map;
using PacMan.PacManGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMan.Entities.Ghosts.GhostAccessories;

namespace PacMan.Entities.Ghosts
{
    public class Inky : GhostBase

    {
        private Blinky blinky;
        public Blinky Blinky { set { blinky = value; } }
        public Inky(int x, int y, int width, int height, int numOfFrames, string path, string fileName,  Blinky blinky) : base(x, y, width, height, numOfFrames, path, fileName)
        {
            this.movementMode = Modes.IDLEINHOUSE;

            this.scatterTargetTile = Map.Map.GetInstance().Tiles[27, 31];
            this.houseTargetTile = Map.Map.GetInstance().Tiles[13, 17];
            this.startTargetTile = Map.Map.GetInstance().Tiles[14, 14];

            this.blinky = blinky;

            this.timer = new Timer(false);
        }

        protected override void IdleInHouse(Player.Player player, Blinky blinky)
        {
            this.direction = Direction.NONE;
            if (player.PointCounter.Points >= 23 & this.MovementMode == Modes.IDLEINHOUSE)
            {
                this.AllowDoor = true;
                this.MovementMode = Modes.START;
            }

        }

        protected override void Chase(Player.Player player)
        {
            Tile playerTile = player.TileLocation;
            Direction playerDirection = player.Direction;
            int incrasValue = 0;

            switch (playerDirection)
            {
                case Direction.UP:
                    if (playerTile.j >= 6)
                    {
                        incrasValue = 2;
                    }
                    else { incrasValue = 2 - (6 - playerTile.j); }
                    playerTile = Map.Map.GetInstance().Tiles[player.TileLocation.i, player.TileLocation.j - incrasValue];
                    break;
                case Direction.DOWN:
                    if (playerTile.j <= 26)
                    {
                        incrasValue = 2;
                    }
                    else { incrasValue = 2 - (playerTile.j - 26); }
                    playerTile = Map.Map.GetInstance().Tiles[player.TileLocation.i, player.TileLocation.j + incrasValue];
                    break;
                case Direction.LEFT:
                    if (playerTile.i >= 3)
                    {
                        incrasValue = 2;
                    }
                    else { incrasValue = 2 - (3 - playerTile.i); }
                    playerTile = Map.Map.GetInstance().Tiles[player.TileLocation.i - incrasValue, player.TileLocation.j];
                    break;
                case Direction.RIGHT:
                    if (playerTile.i <= 24)
                    {
                        incrasValue = 2;
                    }
                    else { incrasValue = 2 - ((playerTile.i) - 24); }
                    playerTile = Map.Map.GetInstance().Tiles[player.TileLocation.i + incrasValue, player.TileLocation.j];
                    break;
            }

            int targetTileI = this.blinky.TileLocation.i + 2 * (playerTile.i - this.blinky.TileLocation.i);
            int targetTileJ = this.blinky.TileLocation.j + 2 * (playerTile.j - this.blinky.TileLocation.j);

            if (targetTileI <= 0) { targetTileI = 1; }
            else if (targetTileI >= 27) { targetTileI = 26; }
            if (targetTileJ <= 3) { targetTileJ = 4; }
            else if (targetTileJ >= 29) { targetTileJ = 28; }

            Tile targetTile = Map.Map.GetInstance().Tiles[targetTileI, targetTileJ];

            this.ChangeDirectionBasedOnTarget(targetTile);
        }

        
    }
}
