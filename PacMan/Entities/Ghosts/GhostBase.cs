using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.Map;
using System;
using System.Collections.Generic;
using PacMan.Entities.Player;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMan.Entities.Ghosts.GhostAccessories;
using PacMan.Entities.EntityAnimations;

namespace PacMan.Entities.Ghosts
{
    public abstract class GhostBase : EntityBase
    {
        public Rectangle Rectangle { get { return rectangle; } }
        protected Modes movementMode;
        public Modes MovementMode 
        {
            get { return movementMode; }
            set {  movementMode = value; } 
        }

        protected Modes previousMode;
        public Modes PreviousMode 
        {
            get { return previousMode; }
            set {  previousMode = value; }
        }

        protected Tile scatterTargetTile;
        protected Tile startTargetTile;
        public Tile StartTargetTile { get { return startTargetTile; } }
        protected Tile houseTargetTile;
        public Tile HouseTargetTile{ get { return houseTargetTile; } }

        protected bool canChangeDirection;
        protected bool allowDoor;
        public bool AllowDoor 
        { 
            get { return allowDoor; } 
            set { allowDoor = value; }
        }
        
        protected Random random;
        public Timer timer;
        protected EntityAnimations.Animation eyeAnimation = new EntityAnimations.Animation(8, 0.1f, 32, 32, Game1.PathToGhostImages, "ghost_eye.png");

        public GhostBase(int x, int y, int width, int height, int numOfFrames, string path, string fileName) : base(x, y, width, height, numOfFrames, path, fileName)
        {
            this.speed = 2;

            this.canChangeDirection = true;
            
            this.allowDoor = true;
            
            this.random = new Random();

            this.animation = new Animation(numOfFrames, 0.1f, 32, 32, path, fileName);
        }

        protected override void UpdateTilesAround()
        {
            base.UpdateTilesAround();

            if (this.allowDoor)
            {
                List<Tile> tempList = new List<Tile>();

                foreach (Tile tile in this.tilesAround)
                {
                    Tuple<int, int> tileLocation = Tuple.Create(tile.i, tile.j);

                    if (!Map.Map.GetInstance().DoorTiles.Contains(tileLocation))
                    {
                        tempList.Add(tile);
                    }
                    else if (Map.Map.GetInstance().GhostStartIntersections.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j))
                            | Map.Map.GetInstance().HouseTiles.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j))
                            | Map.Map.GetInstance().DoorTiles.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j)))
                    {
                        if (this.movementMode == Modes.START)
                        {
                            this.possibleDirections.Add(Direction.UP);
                        }
                        else if (this.movementMode == Modes.RUNBACKTOHOUSE)
                        {
                            this.possibleDirections.Add(Direction.DOWN);
                            this.nextDirection = Direction.DOWN;
                        }
                    }
                }
                this.tilesAround = tempList;
            }
        }

        protected abstract void IdleInHouse(Player.Player player, Blinky blinky);
        protected abstract void Chase(Player.Player player);
        protected void Start()
        {
            if (this.tileLocation != this.startTargetTile)
            {
                this.ChangeDirectionBasedOnTarget(this.startTargetTile);
            }
            else
            {
                this.timer.TimerRunning = true;
                this.allowDoor = false;
                this.movementMode = Modes.SCATTER;
            }
        }

        protected void Scatter() 
        {
            this.ChangeDirectionBasedOnTarget(this.scatterTargetTile);
        }
        protected void Frightened(float time)
        {
            if (!this.timer.FrightenedTimerRunning)
            {
                this.timer.FrightenedTimerRunning = true;
                this.timer.TimerRunning = false;
                this.speed = 1;
                this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToGhostImages + "scared_ghost_body.png");
            }

            if(this.timer.FrightenedTimerRunning) 
            {
                this.timer.IncraseFrightenedTimeElapsed(time);

                if ((int)Math.Floor(this.timer.FrightenedTimeElapsed) != 10)
                {
                    //Random mozgás
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

                else 
                {
                    this.timer.FrightenedTimeElapsed = 0;
                    this.timer.FrightenedTimerRunning = false;
                    this.timer.TimerRunning = true;
                    this.speed = 2;

                    this.MovementMode = Modes.SCATTER;
                    this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToGhostImages + this.animation.FileName);
                }
            }
        }

        protected void RunBackToHouse() 
        {
            if (!Map.Map.GetInstance().HouseTiles.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j)))
            {
                if (this.speed != 4) 
                {
                    this.speed = 4;
                    this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToGhostImages + "transparent_ghost_body.png");
                    this.allowDoor = true;
                }
                this.ChangeDirectionBasedOnTarget(this.houseTargetTile); ;
            }
            else 
            {
                this.timer.FrightenedTimeElapsed = 0;
                this.timer.FrightenedTimerRunning = false;
                this.timer.TimerRunning = true;

                this.allowDoor = true;
                this.speed = 2;
                this.movementMode = Modes.IDLEINHOUSE;
                this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToGhostImages + this.animation.FileName);
            }
        }

        private void ExecuteMovementBasedOnMode(Player.Player player, float seconds, Blinky blinky) 
        {
            switch (this.movementMode) 
            {
                case Modes.IDLEINHOUSE:
                    this.IdleInHouse(player, blinky); 
                    break;
                case Modes.START:
                    this.Start(); 
                    break;
                case Modes.CHASE:
                    this.Chase(player);
                    break;
                case Modes.SCATTER:
                    this.Scatter();
                    break;
                case Modes.FRIGHTENED:
                    this.Frightened(seconds);
                    break;
                case Modes.RUNBACKTOHOUSE:
                    this.RunBackToHouse();
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
            if (Map.Map.GetInstance().Intersections.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j))
                || Map.Map.GetInstance().GhostStartIntersections.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j))
                || Map.Map.GetInstance().HouseTiles.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j)))
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


        public override void Update(float seconds)
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

        public override void Draw()
        {
            Vector2 drawVector = new Vector2(this.position.X - 8, this.position.Y - 8);
            this.animation.DrawAnimation(drawVector);
            this.eyeAnimation.DrawAnimation(drawVector);
        }

        public void UpdateGhost(Player.Player player, float time, Blinky blinky)
        {
            this.timer.IncraseTimeElapsed(time);
            this.timer.ChangeGhostModeBasedOnTime(this);

            this.animation.Update();
            this.eyeAnimation.Update();
            if (this.rectangle.X >= Game1.LeftBoundary & this.rectangle.X <= Game1.RightBoundary)
            {
                this.ResetTeleportedValue();
                this.UpdateTilesAround();
                this.ExecuteMovementBasedOnMode(player, time, blinky);
                this.Update(time);
            }
            else 
            {
                this.UpdateWhenOutOfBounds();
            }
        }
    }
}
