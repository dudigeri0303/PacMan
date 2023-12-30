using Microsoft.Xna.Framework.Graphics;
using PacMan.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Entities.Player
{
    public class Player : EntityBase
    {
        private int points;
        public int Points { get { return points; } }

        private List<Pellet> pelletsAround;
        public Player(int x, int y, int width, int height, Pellet[,] pelletArray) : base(x, y, width, height)
        {
            this.path = Game1.PathToPlayerImages;
            this.fileName = "pacman_dummy24.png";
            this.texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, this.path + this.fileName);
            this.points = 0;
            this.pelletsAround = new List<Pellet>();
        }

        private void UpdatePelletsAround()
        {
            this.pelletsAround.Clear();

            foreach (var tile in this.tileOffsetsAround)
            {
                var tileIndex = Tuple.Create(this.tileLocation.i + tile.Item1, this.tileLocation.j + tile.Item2);

                if (Map.Map.GetInstance().Pellets[tileIndex.Item1, tileIndex.Item2].Name.Contains("test")) 
                {
                    this.pelletsAround.Add(Map.Map.GetInstance().Pellets[tileIndex.Item1, tileIndex.Item2]);
                }
            }
        }

        private void PelletCollision() 
        {
            foreach (var pellet in this.pelletsAround) 
            {
                if (this.rectangle.Contains(pellet.Rect)) 
                {
                    this.points++;
                    pellet.WhenEaten();
                }
            }
        }

        public override void Update()
        {
            this.UpdateTilesAround();
            this.UpdatePelletsAround();

            this.UpdateDirection();
            this.UpdateSpeedVectorBasedOnDirection();
            
            this.MoveHorizontaly();
            this.HorizontalCollision();
            this.UpdateRectValue();

            this.MoveVerticaly();
            this.VerticalCollision();
            this.UpdateRectValue();

            this.PelletCollision();
        }
    }
}
