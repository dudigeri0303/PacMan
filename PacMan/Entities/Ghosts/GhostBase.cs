using Microsoft.Xna.Framework;
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
        public Rectangle Rectangle { get { return rectangle; } }
        public Modes MovementMode 
        {
            get { return movementMode; }
            set {  movementMode = value; } 
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

        protected bool timerRunning;
        public bool TimerRunning
        {
            get { return timerRunning; }
            set { timerRunning = value; }
        }

        protected float timeElapsed;
        public float TimeElapsed {  set { timeElapsed += value; } }
        
        protected float frightenedTImeElapsed;
        protected bool frightenedTimerRunning;
        public GhostBase(int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.path = Game1.PathToGhostImages;

            this.speed = 2;

            this.canChangeDirection = true;
            
            this.allowDoor = true;

            this.frightenedTimerRunning = false;
            this.frightenedTImeElapsed = 0;

            this.startTargetTile = Map.Map.GetInstance().Tiles[12, 14];
            
            this.random = new Random();
        }

        //TODO visszameneskor legyen nyitva az ajto
        protected override void UpdateTilesAround()
        {
            base.UpdateTilesAround();
            
            if(this.allowDoor) 
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
                            | Map.Map.GetInstance().HouseTiles.Contains(Tuple.Create(this.tileLocation.i, this.tileLocation.j)))
                    {
                        if (this.movementMode == Modes.START)
                        {
                            this.possibleDirections.Add(Direction.UP);
                        }
                        else if (this.movementMode == Modes.RUNBACKTOHOUSE) 
                        {
                            this.possibleDirections.Add(Direction.DOWN);
                        }
                    
                    
                    }
                }
                this.tilesAround = tempList;
            }
        }

        protected void IdleInHouse() 
        {
            this.nextDirection = Direction.NONE;
        }

        protected void Start() 
        {
            this.ChangeDirectionBasedOnTarget(this.startTargetTile);
        
        }

        protected abstract void Chase(Player.Player player);
        protected void Scatter() 
        {
            this.ChangeDirectionBasedOnTarget(this.scatterTargetTile);
        }
        protected void Frightened(float seconds)
        {
            if (!this.frightenedTimerRunning)
            {
                this.frightenedTimerRunning = true;
                this.timerRunning = false;
            }
            else 
            {
                this.frightenedTImeElapsed += seconds;
                
            }

            if (this.frightenedTimerRunning & (int)Math.Floor(this.frightenedTImeElapsed) != 10)
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
            else if (this.frightenedTimerRunning & (int)Math.Floor(this.frightenedTImeElapsed) == 10) 
            {
                this.frightenedTImeElapsed = 0;
                this.frightenedTimerRunning = false;
                this.timerRunning = true;
                this.MovementMode = Modes.SCATTER;
            }
        }

        protected void RunBackToHouse() 
        {
            if (this.tileLocation != this.houseTargetTile)
            {
                if (this.speed != 4) 
                {
                    this.speed = 4;
                    this.allowDoor = true;
                }
                this.ChangeDirectionBasedOnTarget(this.houseTargetTile); ;
            }
            else 
            {
                this.allowDoor = false;
                this.speed = 2;
                this.movementMode = Modes.START;
            }
        }

        private void ExecuteMovementBasedOnMode(Player.Player player, float seconds) 
        {
            switch (this.movementMode) 
            {
                case Modes.IDLEINHOUSE:
                    this.IdleInHouse(); 
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
                || this.tileLocation == this.houseTargetTile)
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

        public void ChangeModeBasedOnTime() 
        {
            if (((int)Math.Floor(this.timeElapsed) == 7 || (int)Math.Floor(this.timeElapsed) == 34 || (int)Math.Floor(this.timeElapsed) == 59 || (int)Math.Floor(this.timeElapsed) == 84) & this.movementMode != Modes.CHASE) 
            { 
                this.movementMode = Modes.CHASE;
                if ((int)Math.Floor(this.timeElapsed) == 84) 
                {
                    this.timerRunning = false;
                }
            }
            else if (((int)Math.Floor(this.timeElapsed) == 27 || (int)Math.Floor(this.timeElapsed) == 54 || (int)Math.Floor(this.timeElapsed) == 79) & this.movementMode != Modes.SCATTER) 
            { 
                this.movementMode = Modes.SCATTER;
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

        public void UpdateGhost(Player.Player player, float seconds)
        {
            if (this.rectangle.X >= 24 & this.rectangle.X <= 624)
            {
                this.ResetTeleportedValue();
                this.UpdateTilesAround();
                this.ExecuteMovementBasedOnMode(player, seconds);
                this.Update();
            }
            else 
            {
                this.UpdateWhenOutOfBounds();
            }
            
        }
    }
}
