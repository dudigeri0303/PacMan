using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public int Speed {get { return speed;}}

        protected Vector2 horizontalAndVerticalSpeeds;
        public Vector2 HorizontalAndVerticalSpeeds { set { horizontalAndVerticalSpeeds = value; } }
        
        protected List<Rectangle> tilesAround;
        protected List<Tuple<int, int>> tileOffsetsAround;

        protected Direction direction;
        public Direction Direction 
        { 
            get { return direction;}
            set { direction = value; }
        }

        protected List<Direction> possibleDirections;
        public List<Direction> PossibleDirections { get {return possibleDirections; } }

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
                Tuple.Create(-1, 1),
            };

            this.direction = Direction.NONE;
            this.possibleDirections = new List<Direction>();
        }

        private void FillPossibleDirections() 
        {
            this.possibleDirections.Clear();
            this.possibleDirections.Add(Direction.LEFT);
            this.possibleDirections.Add(Direction.RIGHT);
            this.possibleDirections.Add(Direction.UP);
            this.possibleDirections.Add(Direction.DOWN);
        
        }

        private void UpdateTilesAround(TileMap tileMap) 
        {
            this.FillPossibleDirections();
            this.tilesAround.Clear();

            var tileLocation = Tuple.Create((int)Math.Floor((double)this.position.X/ (double)this.width), (int)Math.Floor((double)this.position.Y/(double)this.height));

            foreach (var tile in this.tileOffsetsAround) 
            {
                var tileIndex = Tuple.Create(tileLocation.Item1 + tile.Item1, tileLocation.Item2 + tile.Item2);
                if (tileMap.Tiles[tileIndex.Item1, tileIndex.Item2].Name.ElementAt(4).ToString() != "0")
                {
                    this.tilesAround.Add(tileMap.Tiles[tileIndex.Item1, tileIndex.Item2].Rect);
                    if (tile.Item1 == 1 & tile.Item2 == 0 & this.possibleDirections.Contains(Direction.RIGHT))
                    {
                        this.possibleDirections.Remove(Direction.RIGHT);
                    }
                    if (tile.Item1 == -1 & tile.Item2 == 0 & this.possibleDirections.Contains(Direction.LEFT))
                    {
                        this.possibleDirections.Remove(Direction.LEFT);
                    }
                    if (tile.Item2 == 1 & tile.Item1 == 0 & this.possibleDirections.Contains(Direction.DOWN))
                    {
                        this.possibleDirections.Remove(Direction.DOWN);
                    }
                    if (tile.Item2 == -1 & tile.Item1 == 0 & this.possibleDirections.Contains(Direction.UP))
                    {
                        this.possibleDirections.Remove(Direction.UP);
                    }
                }
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

        private Rectangle CreateRectForCollision() 
        {
            return new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
        }

        private void MoveHorizontaly() 
        {
            this.position.X += (int)this.horizontalAndVerticalSpeeds.X;
        }

        private void MoveVerticaly() 
        {
            this.position.Y += (int)this.horizontalAndVerticalSpeeds.Y;
        }

        private void HorizontalCollision()
        {
            var rectForCollsion = this.CreateRectForCollision();
            foreach (var rect in this.tilesAround) 
            {
                if (rectForCollsion.Intersects(rect)) 
                {
                    if ((int)this.horizontalAndVerticalSpeeds.X > 0) 
                    {
                        rectForCollsion.X = rect.X - Game1.TileWidth;
                    }  
                    if ((int)this.horizontalAndVerticalSpeeds.X < 0) 
                    {
                        rectForCollsion.X = rect.X + Game1.TileWidth;
                    }
                    this.position.X = rectForCollsion.X;
                }
            }

        }

        private void VerticalCollision()
        {
            var rectForCollsion = this.CreateRectForCollision();
            foreach (var rect in this.tilesAround)
            {
                if (rectForCollsion.Intersects(rect))
                {
                    if ((int)this.horizontalAndVerticalSpeeds.Y > 0)
                    {
                        rectForCollsion.Y = rect.Y - Game1.TileHeight;
                    }
                    if ((int)this.horizontalAndVerticalSpeeds.Y < 0)
                    {
                        rectForCollsion.Y  = rect.Y + Game1.TileHeight;
                    }
                    this.position.Y = rectForCollsion.Y;
                }
            }
        }


        private void UpdateRectValue() 
        {
            this.rectangle.X = (int)this.position.X;
            this.rectangle.Y = (int)this.position.Y;
        }

        public void Draw() 
        {
            Game1._spriteBatch.Draw(this.texture, this.rectangle, Color.White);
        }
        public void Update(TileMap tileMap)
        {
            this.UpdateTilesAround(tileMap); 
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
