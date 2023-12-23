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
            this.speed = 2;
            this.movementMode = Modes.CHASE;
            this.direction = Direction.RIGHT;
            this.scatterTargetTile = TileMap.GetInstance().Tiles[2, 0];
            this.path = $"C:\\Users\\hp\\Source\\Repos\\PacMan\\PacMan\\Assets\\EntityAssets\\GhostAssets\\";
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
                    if (playerTile.j >= 7) 
                    {
                        incrasValue = 4;
                    }
                    else { incrasValue = 4 - (7 - (playerTile.j + 3)); }
                    targetTile = TileMap.GetInstance().Tiles[player.TileLocation.i, player.TileLocation.j - incrasValue];
                    break;
                case Direction.DOWN:
                    if (playerTile.j <= 29)
                    {
                        incrasValue = 4;
                    }
                    else { incrasValue = 4 - ((playerTile.j + 2) - 29); }
                    targetTile = TileMap.GetInstance().Tiles[player.TileLocation.i, player.TileLocation.j + incrasValue];
                    break;
                case Direction.LEFT:
                    if (playerTile.i >= 5)
                    {
                        incrasValue = 4;
                    }
                    else { incrasValue = 4 - (5 - playerTile.i); }
                    targetTile = TileMap.GetInstance().Tiles[player.TileLocation.i - incrasValue, player.TileLocation.j];
                    break;
                case Direction.RIGHT:
                    if (playerTile.i <= 23)
                    {
                        incrasValue = 4;
                    }
                    else { incrasValue = 4 - (playerTile.i) - 23; }
                    targetTile = TileMap.GetInstance().Tiles[player.TileLocation.i + incrasValue, player.TileLocation.j];
                    break;
            }
            this.ChangeDirectionBasedOnTarget(targetTile);
        }
    }
}
