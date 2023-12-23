using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMan.Map;

namespace PacMan.Entities
{
    public abstract class EntityBase
    {
        protected int width, height;
        protected Vector2 position;
        protected Rectangle rectangle;
        protected Texture2D texture;

        protected string path;
        protected string fileName;

        protected int speed;
        protected Vector2 horizontalAndVerticalSpeeds;

        protected List<Rectangle> tilesAround;
        protected List<Tuple<int, int>> tileOffsetsAround;

        protected Direction direction;
        public Direction Direction
        {
            get { return direction; }
        }
        protected Direction nextDirection;
        public Direction NextDirection
        {
            get { return nextDirection; }
            set { nextDirection = value; }
        }
        protected Direction previousDirection;
        protected List<Direction> possibleDirections;
        public List<Direction> PossibleDirections { get { return possibleDirections; } }
        protected bool illegalUpCollision, illegalDownCollision, illegalLeftCollision, illegalRightCollision;
        protected Tile tileLocation;
        public Tile TileLocation { get { return tileLocation; } }
  
        public EntityBase(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.speed = 3;

            this.position = new Vector2((float)x, (float)y);
            this.rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, width, height);
            
            
            this.tilesAround = new List<Rectangle>();    
            this.tileOffsetsAround = new List<Tuple<int, int>>()
            {
                Tuple.Create(0, 0),
                Tuple.Create(1, 0),
                Tuple.Create(0, 1),
                Tuple.Create(1, 1),
                Tuple.Create(-1, 0),
                Tuple.Create(0, -1),
                Tuple.Create(-1, -1),
                Tuple.Create(1, -1),
                Tuple.Create(-1, 1)
            };

            this.direction = Direction.RIGHT;
            this.possibleDirections = new List<Direction>();

            this.illegalDownCollision = false;
            this.illegalUpCollision = false;
            this.illegalLeftCollision = false;
            this.illegalRightCollision = false;
        }

        protected void FillPossibleDirections() 
        {
            this.possibleDirections.Clear();
            if (!this.illegalLeftCollision) { this.possibleDirections.Add(Direction.LEFT); }
            if (!this.illegalRightCollision) { this.possibleDirections.Add(Direction.RIGHT); }
            if (!this.illegalUpCollision) { this.possibleDirections.Add(Direction.UP); }
            if (!this.illegalDownCollision) { this.possibleDirections.Add(Direction.DOWN); }
            this.illegalDownCollision = false;
            this.illegalLeftCollision = false;
            this.illegalRightCollision = false;
            this.illegalUpCollision = false;
        }

        protected void UpdateTilesAround() 
        {
            this.FillPossibleDirections();
            this.tilesAround.Clear();

            int i = this.direction == Direction.LEFT? 
                (int)Math.Ceiling((double)this.position.X / (double)this.width) : (int)Math.Floor((double)this.position.X / (double)this.width);

            int j = this.direction == Direction.UP? 
                (int)Math.Ceiling((double)this.position.Y / (double)this.height) : (int)Math.Floor((double)this.position.Y / (double)this.height);

            var tileLocation = Tuple.Create(i, j);
            this.tileLocation = TileMap.GetInstance().Tiles[tileLocation.Item1, tileLocation.Item2];


            foreach (var tile in this.tileOffsetsAround) 
            {
                var tileIndex = Tuple.Create(tileLocation.Item1 + tile.Item1, tileLocation.Item2 + tile.Item2);
                if (TileMap.GetInstance().Tiles[tileIndex.Item1, tileIndex.Item2].Name.ElementAt(4).ToString() != "0")
                {
                    this.tilesAround.Add(TileMap.GetInstance().Tiles[tileIndex.Item1, tileIndex.Item2].Rect);
                    if (tile.Item1 == 1 & tile.Item2 == 0 & this.possibleDirections.Contains(Direction.RIGHT))
                    {
                        this.possibleDirections.Remove(Direction.RIGHT);
                    }
                    else if (tile.Item1 == -1 & tile.Item2 == 0 & this.possibleDirections.Contains(Direction.LEFT))
                    {
                        this.possibleDirections.Remove(Direction.LEFT);
                    }
                    else if (tile.Item2 == 1 & tile.Item1 == 0 & this.possibleDirections.Contains(Direction.DOWN))
                    {
                        this.possibleDirections.Remove(Direction.DOWN);
                    }
                    else if (tile.Item2 == -1 & tile.Item1 == 0 & this.possibleDirections.Contains(Direction.UP))
                    {
                        this.possibleDirections.Remove(Direction.UP);
                    }
                }
            }
        }

        protected void UpdateDirection() 
        {
            if (this.possibleDirections.Contains(this.nextDirection) & this.nextDirection != this.direction)
            {
                this.previousDirection = this.direction;
                this.direction = this.nextDirection;
            }
        }

        protected void UpdateSpeedVectorBasedOnDirection()
        {   
            switch (this.direction)
            {
                case Direction.LEFT:
                    this.horizontalAndVerticalSpeeds.Y = (float)0;
                    this.horizontalAndVerticalSpeeds.X = (int)-1*this.speed;
                    break;
                case Direction.RIGHT:
                    this.horizontalAndVerticalSpeeds.Y = (float)0;
                    this.horizontalAndVerticalSpeeds.X = (float)this.speed;
                    break;
                case Direction.UP:
                    this.horizontalAndVerticalSpeeds.X = (float)0;
                    this.horizontalAndVerticalSpeeds.Y = (float)-1*this.speed;
                    break;
                case Direction.DOWN:
                    this.horizontalAndVerticalSpeeds.X = (float)0;
                    this.horizontalAndVerticalSpeeds.Y = (float)this.speed;
                    break;
                case Direction.NONE:
                    this.horizontalAndVerticalSpeeds.X = (float)0;
                    this.horizontalAndVerticalSpeeds.Y = (float)0;
                    break;
            }
        }

        protected Rectangle CreateRectForCollision() 
        {
            return new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
        }

        protected void MoveHorizontaly() 
        {
            this.position.X += (int)this.horizontalAndVerticalSpeeds.X;
        }

        protected void MoveVerticaly() 
        {
            this.position.Y += (int)this.horizontalAndVerticalSpeeds.Y;
        }

        protected void HorizontalCollision()
        {
            var rectForCollsion = this.CreateRectForCollision();
            foreach (var rect in this.tilesAround) 
            {
                if (rectForCollsion.Intersects(rect)) 
                {
                    if ((int)this.horizontalAndVerticalSpeeds.X > 0) 
                    {
                        rectForCollsion.X = rect.X - Game1.TileWidth;

                        if (this.possibleDirections.Contains(Direction.RIGHT))
                        {
                            this.direction = this.previousDirection;
                            this.illegalRightCollision = true;
                            //this.UpdateSpeedVectorBasedOnDirection();
                            return;
                        }
                    }
                    if ((int)this.horizontalAndVerticalSpeeds.X < 0) 
                    {
                        rectForCollsion.X = rect.X + Game1.TileWidth;
                        if (this.possibleDirections.Contains(Direction.LEFT))
                        {
                            this.direction = this.previousDirection;
                            this.illegalLeftCollision = true;
                            //this.UpdateSpeedVectorBasedOnDirection();
                            return;
                        }
                    }
                    this.position.X = rectForCollsion.X;
                }
            }
        }

        protected void VerticalCollision()
        {
            var rectForCollsion = this.CreateRectForCollision();
            foreach (var rect in this.tilesAround)
            {
                if (rectForCollsion.Intersects(rect))
                {
                    if ((int)this.horizontalAndVerticalSpeeds.Y > 0)
                    {
                        rectForCollsion.Y = rect.Y - Game1.TileHeight;
                        if (this.possibleDirections.Contains(Direction.DOWN)) 
                        {
                            this.direction = this.previousDirection;
                            this.illegalDownCollision = true;
                           // this.UpdateSpeedVectorBasedOnDirection();
                            return;
                        }
                    }
                    if ((int)this.horizontalAndVerticalSpeeds.Y < 0)
                    {
                        rectForCollsion.Y  = rect.Y + Game1.TileHeight;
                        if (this.possibleDirections.Contains(Direction.UP))
                        {
                           this.direction = this.previousDirection;
                            this.illegalUpCollision = true;
                           // this.UpdateSpeedVectorBasedOnDirection();
                            return;
                        }
                    }
                    this.position.Y = rectForCollsion.Y;
                }
            }
        }

        protected void UpdateRectValue() 
        {
            this.rectangle.X = (int)this.position.X;
            this.rectangle.Y = (int)this.position.Y;
        }

        public void Draw() 
        {
            Game1._spriteBatch.Draw(this.texture, this.rectangle, Color.White);
        }
        public virtual void Update()
        {
            this.UpdateTilesAround();
            this.UpdateDirection();
            this.UpdateSpeedVectorBasedOnDirection();
            this.MoveHorizontaly();
            this.HorizontalCollision();
            this.UpdateRectValue();

            this.MoveVerticaly();
            this.VerticalCollision();
            this.UpdateRectValue();
        }
    }
}
