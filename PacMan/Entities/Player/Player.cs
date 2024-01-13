using Microsoft.Xna.Framework.Graphics;
using PacMan.Entities.EntityAnimations;
using PacMan.Entities.Ghosts;
using PacMan.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Entities.Player
{
    public class Player : EntityBase
    {
        private int points;
        public int Points { get { return points; } }

        private int healthPoints;
        
        private List<Pellet> pelletsAround;

        private GhostManager ghostManager;

        public Player(int x, int y, int width, int height, int numOfFrames, string path, string fileName, Pellet[,] pelletArray, GhostManager ghostManager) : base(x, y, width, height, numOfFrames, path, fileName)
        {
            this.points = 0;
            this.healthPoints = 3;
            this.pelletsAround = new List<Pellet>();
            this.ghostManager = ghostManager;
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
                if (pellet.Name.ElementAt(6).ToString() != "_") 
                {
                    this.ghostManager.MakeGhostsFrightened();
                }

                if (this.rectangle.Contains(pellet.Rect)) 
                {
                    this.points++;
                    pellet.WhenEaten();
                }
            }
        }

        private void GhostCollision() 
        {
            foreach (var ghost in this.ghostManager.Ghosts)
            {
                if (this.rectangle.Contains(ghost.Rectangle))
                {
                    if (ghost.MovementMode == Modes.FRIGHTENED)
                    {
                        ghost.MovementMode = Modes.RUNBACKTOHOUSE;
                    }
                    else 
                    {
                        this.healthPoints--;
                    }
                }
            }
        }

        private void ChangeAnimationBasedOnDirection() 
        {
            if (this.Direction == Direction.UP & this.animation.FileName != "pacman_up.png")
            {
                this.animation.FileName = "pacman_up.png";
                this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToPlayerImages + "pacman_up.png");
            }
            else if (this.Direction == Direction.DOWN & this.animation.FileName != "pacman_down.png")
            {
                this.animation.FileName = "pacman_down.png";
                this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToPlayerImages + "pacman_down.png");
            }
            else if (this.Direction == Direction.LEFT & this.animation.FileName != "pacman_left.png")
            {
                this.animation.FileName = "pacman_left.png";
                this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToPlayerImages + "pacman_left.png");
            }
            else if (this.Direction == Direction.RIGHT & this.animation.FileName != "pacman_right.png")
            {
                this.animation.FileName = "pacman_right.png";
                this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToPlayerImages + "pacman_right.png");
            }
        }

        public override void Update()
        {
            this.ChangeAnimationBasedOnDirection();
            this.animation.Update();
            if (this.rectangle.X >= 24 & this.rectangle.X <= 624)
            {
                this.ResetTeleportedValue();
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

                this.GhostCollision();
                this.PelletCollision();
            }
            else
            {
                this.UpdateWhenOutOfBounds();
            }
        }
    }
}
