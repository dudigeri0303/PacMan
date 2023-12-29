using Microsoft.Xna.Framework.Graphics;
using PacMan.Entities.Ghosts;
using PacMan.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Entities.Ghosts
{
    public class Pinky : GhostBase
    {
        public Pinky(int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.movementMode = Modes.IDLEINHOUSE;
            this.scatterTargetTile = Map.Map.GetInstance().Tiles[2, 0];
            this.fileName = "pinky_test.png";
            this.texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, this.path + this.fileName);
        }

        protected override void Chase(Player.Player player)
        {
            Tile playerTile = player.TileLocation;
            Direction playerDirection = player.Direction;
            Tile targetTile = null;
            int incrasValue = 0;
            
            
            switch (playerDirection) 
            {
                case Direction.UP:
                    if (playerTile.j >= 8) 
                    {
                        incrasValue = 4;
                    }
                    else { incrasValue = 4 - (8 - playerTile.j); }
                    targetTile = Map.Map.GetInstance().Tiles[player.TileLocation.i, player.TileLocation.j - incrasValue];
                    break;
                case Direction.DOWN:
                    if (playerTile.j <= 24)
                    {
                        incrasValue = 4;
                    }
                    else { incrasValue = 4 - (playerTile.j - 24); }
                    targetTile = Map.Map.GetInstance().Tiles[player.TileLocation.i, player.TileLocation.j + incrasValue];
                    break;
                case Direction.LEFT:
                    if (playerTile.i >= 5)
                    {
                        incrasValue = 4;
                    }
                    else { incrasValue = 4 - (5 - playerTile.i); }
                    targetTile = Map.Map.GetInstance().Tiles[player.TileLocation.i - incrasValue, player.TileLocation.j];
                    break;
                case Direction.RIGHT:
                    if (playerTile.i <= 22)
                    {
                        incrasValue = 4;
                    }
                    else { incrasValue = 4 - ((playerTile.i) - 22); }
                    targetTile = Map.Map.GetInstance().Tiles[player.TileLocation.i + incrasValue, player.TileLocation.j];
                    break;
            }
            this.ChangeDirectionBasedOnTarget(targetTile);
        }
    }
}

