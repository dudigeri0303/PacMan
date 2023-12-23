﻿using Microsoft.Xna.Framework;
using PacMan.Map;
using System;
using System.Collections.Generic;
using PacMan.Entities.Player;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Entities.Ghosts
{
    public abstract class GhostBase : EntityBase
    {
        protected Modes movementMode;
        protected Tile scatterTargetTile;
        protected bool canChangeDirection;
        protected Random random;

        //Only for inky, couse he needs blinky to calc the target tile
        protected GhostBase blinky;
        public GhostBase Blinky { set { blinky = value; } }
        
        public GhostBase(int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.speed = 2;
            this.movementMode = Modes.SCATTER;
            this.direction = Direction.RIGHT;
            this.path = Game1.PathToGhostImages;
            this.canChangeDirection = true;
            this.random = new Random();
        }
        protected abstract void Chase(Player.Player player);
        protected void Scatter() 
        {
            this.ChangeDirectionBasedOnTarget(this.scatterTargetTile);
        }
        protected void Frightened()
        {
            if (Map.Map.GetInstance().Intersections.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j)))
            {
                if (this.canChangeDirection == true)
                {
                    this.canChangeDirection = false;
                    this.RemoveReverseDirection();
                    var randIndex = this.random.Next(0, this.possibleDirections.Count);
                    this.nextDirection = this.possibleDirections[randIndex];
                }
            }
            else if (!this.canChangeDirection) { this.canChangeDirection = true; }
        }
        private void ExecuteMovementBasedOnMode(Player.Player player) 
        {
            switch (this.movementMode) 
            {
                case Modes.CHASE:
                    this.Chase(player);
                    break;
                case Modes.SCATTER:
                    this.Scatter();
                    break;
                case Modes.FRIGHTENED:
                    this.Frightened();
                    break;
            }
        }

        private void ChangeMode(float seconds) 
        {
            Debug.WriteLine(seconds.ToString());
            if((int)Math.Floor(seconds) % 20 == 0) { this.movementMode = Modes.SCATTER; }
            else if((int)Math.Floor(seconds) % 7 == 0) { this.movementMode = Modes.CHASE; }
            
        }

        protected double CalcGreedyValue(Tile tile, Tile targetTile)
        {
            double value = Math.Sqrt(((tile.Rect.X - targetTile.Rect.X) * (tile.Rect.X - targetTile.Rect.X)) + ((tile.Rect.Y - targetTile.Rect.Y) * (tile.Rect.Y - targetTile.Rect.Y)));
            return value;
        }
        protected void RemoveReverseDirection()
        {
            if (this.direction == Direction.LEFT) { this.possibleDirections.Remove(Direction.RIGHT); }
            else if (this.direction == Direction.RIGHT) { this.possibleDirections.Remove(Direction.LEFT); }
            else if (this.direction == Direction.UP) { this.possibleDirections.Remove(Direction.DOWN); }
            else if (this.direction == Direction.DOWN) { this.possibleDirections.Remove(Direction.UP); }
        }

        protected void ChangeDirectionBasedOnTarget(Tile targetTile) 
        {
            if (Map.Map.GetInstance().Intersections.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j)))
            {
                if (this.canChangeDirection == true)
                {
                    this.canChangeDirection = false;
                    this.RemoveReverseDirection();

                    var directionsWithGreedyDistance = new List<Tuple<Direction, double>>();
                    foreach (var dir in this.possibleDirections)
                    {
                        if (dir == Direction.LEFT)
                        {
                            Tile tileRect = Map.Map.GetInstance().Tiles[this.tileLocation.i - 1, this.tileLocation.j];
                            directionsWithGreedyDistance.Add(Tuple.Create(Direction.LEFT, this.CalcGreedyValue(tileRect, targetTile)));
                        }
                        else if (dir == Direction.RIGHT)
                        {
                            Tile tileRect = Map.Map.GetInstance().Tiles[this.tileLocation.i + 1, this.tileLocation.j];
                            directionsWithGreedyDistance.Add(Tuple.Create(Direction.RIGHT, this.CalcGreedyValue(tileRect, targetTile)));
                        }
                        else if (dir == Direction.UP)
                        {
                            Tile tileRect = Map.Map.GetInstance().Tiles[this.tileLocation.i, this.tileLocation.j - 1];
                            directionsWithGreedyDistance.Add(Tuple.Create(Direction.UP, this.CalcGreedyValue(tileRect, targetTile)));
                        }
                        else if (dir == Direction.DOWN)
                        {
                            Tile tileRect = Map.Map.GetInstance().Tiles[this.tileLocation.i, this.tileLocation.j + 1];
                            directionsWithGreedyDistance.Add(Tuple.Create(Direction.DOWN, this.CalcGreedyValue(tileRect, targetTile)));
                        }
                    }
                    directionsWithGreedyDistance = directionsWithGreedyDistance.OrderBy(tuple => tuple.Item2).ToList();
                    this.nextDirection = directionsWithGreedyDistance[0].Item1;
                }
            }
            else if(!this.canChangeDirection) { this.canChangeDirection = true; }
        }

        public override void Update()
        {
            this.UpdateDirection();
            this.UpdateSpeedVectorBasedOnDirection();

            this.MoveHorizontaly();
            this.HorizontalCollision();
            this.UpdateRectValue();

            this.MoveVerticaly();
            this.VerticalCollision();
            this.UpdateRectValue();
        }

        public void UpdateGhost(Player.Player player, float seconds)
        {
            this.UpdateTilesAround();
            this.ChangeMode(seconds);
            this.ExecuteMovementBasedOnMode(player);
            this.Update();
        }
    }
}
