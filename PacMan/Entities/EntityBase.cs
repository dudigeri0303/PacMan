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
        protected int x, y, width, height;
        protected Vector2 position;
        protected Rectangle rectangle;
        protected Texture2D texture;
        protected string path;
        protected string fileName;
        protected Vector2 speed;
        protected List<Direction> possibleDirections;
        public List<Direction> PossibleDirections { get { return possibleDirections; } }
        protected static List<Tuple<int, int>> neighbourOffsets = new List<Tuple<int, int>>()
        {
                Tuple.Create(-1, 0),
                Tuple.Create(0, -1),
                Tuple.Create(1, 0),
                Tuple.Create(0, 1)
        };

        protected Direction direction;
        public Direction Direction 
        {
            get { return direction; }
            set { this.direction = value; }
        }

        public EntityBase(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.position = new Vector2((float)x, (float)y);
            this.rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, width, height);
            this.direction = Direction.NONE;
            this.speed = new Vector2((float)0, (float)0);
            this.possibleDirections = new List<Direction>();
        }

        private void UpdatePossibleDirections(TileMap tileMap) 
        {
            this.possibleDirections.Clear();
            this.possibleDirections.Add(Direction.NONE);

            Tuple<int, int> tilePosition = Tuple.Create((((int)this.position.X) / this.width), (((int)this.position.Y) / this.height));
            
            foreach (var item in neighbourOffsets) 
            {
                var tileIndexes = Tuple.Create(item.Item1 + tilePosition.Item1, item.Item2 + tilePosition.Item2);
                if (tileMap.Tiles[tileIndexes.Item1, tileIndexes.Item2].Name.ElementAt(4).ToString() == "0")
                {
                    if (item.Item1 == -1 & item.Item2 == 0)
                    {
                        this.possibleDirections.Add(Direction.LEFT);
                    }
                    else if (item.Item1 == 0 & item.Item2 == 1)
                    {
                        this.possibleDirections.Add(Direction.DOWN);
                    }
                    else if (item.Item1 == 1 & item.Item2 == 0)
                    {
                        this.possibleDirections.Add(Direction.RIGHT);
                    }
                    else if (item.Item1 == 0 & item.Item2 == -1) 
                    {
                        this.possibleDirections.Add(Direction.UP);
                    }
                }
            }
        }

        public void Draw() 
        {
            Game1._spriteBatch.Draw(this.texture, this.rectangle, Color.White);
        }

        protected void Move() 
        {
            this.position.X += this.speed.X;
            this.position.Y += this.speed.Y;

            this.rectangle.X = (int)this.position.X;
            this.rectangle.Y = (int)this.position.Y;
        }

        protected void UpdateSpeedVectorBasedOnDirection() 
        {
            if (!this.possibleDirections.Contains(this.direction)) 
            {
                this.Direction = Direction.NONE;
            }

            switch (this.direction) 
            {
                case Direction.LEFT:
                    this.speed.Y = (float)0;
                    this.speed.X = (float)-4;
                    break;
                case Direction.RIGHT:
                    this.speed.Y = (float)0;
                    this.speed.X = (float)4;
                    break;
                case Direction.UP:
                    this.speed.X = (float)0;
                    this.speed.Y = (float)-4;
                    break;
                case Direction.DOWN:
                    this.speed.X= (float)0;
                    this.speed.Y= (float)4;
                    break;
                case Direction.NONE:
                    this.speed.X = (float)0;
                    this.speed.Y = (float)0;
                    break;
            }
        }

        public void Update(TileMap tileMap) 
        {
            this.UpdatePossibleDirections(tileMap);
            this.UpdateSpeedVectorBasedOnDirection();
            this.Move();
        }
    }
}
