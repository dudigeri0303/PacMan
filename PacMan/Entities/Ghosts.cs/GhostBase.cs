using Microsoft.Xna.Framework;
using PacMan.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Entities.Ghosts.cs
{
    public abstract class GhostBase : EntityBase
    {
        protected Modes movementMode;
        protected Tile scatterTargetTile;
        public GhostBase(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }
        protected abstract void Chase(Tile targetTile);
        protected abstract void Scatter();
        protected abstract void Frightened(Tile targetTile);
        private void ExecuteMovementBasedOnMode(Tile targetTile) 
        {
            switch (this.movementMode) 
            {
                case Modes.CHASE:
                    this.Chase(targetTile);
                    break;
                case Modes.SCATTER:
                    this.Scatter();
                    break;
                case Modes.FRIGHTENED:
                    this.Frightened(targetTile);
                    break;
            }
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
            if (TileMap.GetInstance().Intersections.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j)))
            {
                this.RemoveReverseDirection();

                var directionsWithGreedyDistance = new List<Tuple<Direction, double>>();
                foreach (var dir in this.possibleDirections)
                {
                    if (dir == Direction.LEFT)
                    {
                        Tile tileRect = TileMap.GetInstance().Tiles[this.tileLocation.i - 1, this.tileLocation.j];
                        directionsWithGreedyDistance.Add(Tuple.Create(Direction.LEFT, this.CalcGreedyValue(tileRect, targetTile)));
                    }
                    else if (dir == Direction.RIGHT)
                    {
                        Tile tileRect = TileMap.GetInstance().Tiles[this.tileLocation.i + 1, this.tileLocation.j];
                        directionsWithGreedyDistance.Add(Tuple.Create(Direction.RIGHT, this.CalcGreedyValue(tileRect, targetTile)));
                    }
                    else if (dir == Direction.UP)
                    {
                        Tile tileRect = TileMap.GetInstance().Tiles[this.tileLocation.i, this.tileLocation.j - 1];
                        directionsWithGreedyDistance.Add(Tuple.Create(Direction.UP, this.CalcGreedyValue(tileRect, targetTile)));
                    }
                    else if (dir == Direction.DOWN)
                    {
                        Tile tileRect = TileMap.GetInstance().Tiles[this.tileLocation.i, this.tileLocation.j + 1];
                        directionsWithGreedyDistance.Add(Tuple.Create(Direction.DOWN, this.CalcGreedyValue(tileRect, targetTile)));
                    }
                }
                directionsWithGreedyDistance = directionsWithGreedyDistance.OrderBy(tuple => tuple.Item2).ToList();
                this.nextDirection = directionsWithGreedyDistance[0].Item1;
            }
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

        public void UpdateGhost(Tile targetTile)
        {
            this.UpdateTilesAround();
            this.ExecuteMovementBasedOnMode(targetTile);
            this.Update();
        }
    }
}
