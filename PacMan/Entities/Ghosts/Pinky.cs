﻿using Microsoft.Xna.Framework.Graphics;
using PacMan.Entities.Ghosts.GhostAccessories;
using PacMan.Map;

namespace PacMan.Entities.Ghosts
{
    public class Pinky : GhostBase, ILevelUp
    {
        public Pinky(int x, int y, int width, int height, int numOfFrames, string path, string fileName) : base(x, y, width, height, numOfFrames, path, fileName)
        {
            this.movementMode = Modes.IDLEINHOUSE;
            
            this.scatterTargetTile = Map.Map.GetInstance().Tiles[2, 0];
            this.houseTargetTile = Map.Map.GetInstance().Tiles[13, 17];
            this.startTargetTile = Map.Map.GetInstance().Tiles[14, 14];

            this.timer = new Timer(false);
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
                case Direction.NONE:
                    targetTile = Map.Map.GetInstance().Tiles[player.TileLocation.i, player.TileLocation.j];
                    break;
            }
            this.ChangeDirectionBasedOnTarget(targetTile);
        }
        protected override void IdleInHouse(Player.Player player, Blinky blinky)
        {
            this.direction = Direction.NONE;
            if (blinky.TileLocation != null)
            {
                if (this.MovementMode == Modes.IDLEINHOUSE & (blinky.TileLocation.i < 13 || blinky.TileLocation.i > 14))
                {
                    this.AllowDoor = true;
                    this.MovementMode = Modes.START;
                }
            }
        }
        public void ResetForLevelUp(int x, int y)
        {
            this.position.X = x;
            this.position.Y = y;
            this.UpdateRectValue();
            this.MovementMode = Modes.IDLEINHOUSE;
            this.timer.TimerRunning = true;
            this.timer.TimeEladpsed = 0;
            this.timer.FrightenedTimeElapsed = 0;
            this.timer.FrightenedTimerRunning = false;
            this.speed = 2;
            this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToGhostImages + this.animation.FileName);
        }
    }
}

