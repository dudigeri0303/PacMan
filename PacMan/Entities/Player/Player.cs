using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PacMan.Entities.EntityAnimations;
using PacMan.Entities.Ghosts;
using PacMan.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using PacMan.Entities.Player.PlayerAccessories;

namespace PacMan.Entities.Player
{
    public class Player : EntityBase, ILevelUp
    {
        private List<Pellet> pelletsAround;

        private bool canCollideWithGhost;

        private GhostManager ghostManager;
        private Clock clock;
        private HealthBar healthBar;
        private LevelCounter levelCounter;
        private PointCounter pointCounter;
        public PointCounter PointCounter { get { return pointCounter; } }

        public Player(int x, int y, int width, int height, int numOfFrames, string path, string fileName, Pellet[,] pelletArray, GhostManager ghostManager) : base(x, y, width, height, numOfFrames, path, fileName)
        {
            this.speed = 2;
            this.animation = new Animation(numOfFrames, 0.03f, 32, 32, path, fileName);
            this.pelletsAround = new List<Pellet>();

            this.canCollideWithGhost = true;
            
            this.ghostManager = ghostManager;
            this.clock = new Clock();
            this.healthBar = new HealthBar();
            this.levelCounter = new LevelCounter();
            this.pointCounter = new PointCounter();
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
                    this.pointCounter.IncrasePoints();
                    pellet.WhenEaten();
                }
            }
        }

        private void GhostCollision() 
        {
            int collideCount = 0;
            foreach (var ghost in this.ghostManager.Ghosts)
            {
                if (this.rectangle.Intersects(ghost.Rectangle))
                {
                    collideCount++;
                    if (ghost.MovementMode == Modes.FRIGHTENED)
                    {
                        ghost.MovementMode = Modes.RUNBACKTOHOUSE;
                    }

                    else if (this.canCollideWithGhost & ghost.MovementMode != Modes.RUNBACKTOHOUSE)
                    {
                        if (this.healthBar.HealthPoints != 1)
                        {
                            this.canCollideWithGhost = false;
                            this.healthBar.DecreaseHealth();
                        }
                        else { Game1.NewGame(); }
                    }
                }
            }
            if (collideCount == 0) 
            {
                this.canCollideWithGhost = true;
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

        public void ResetForLevelUp(int x, int y)
        {
            this.position.X = x;
            this.position.Y = y;
            this.UpdateRectValue();
            this.healthBar.HealthPoints = 3;
            this.direction = Direction.NONE;
            this.levelCounter.IncraseLevel();
        }


        public override void Draw()
        {
            Vector2 drawVector = new Vector2(this.position.X - 8, this.position.Y - 8);
            this.animation.DrawAnimation(drawVector);
            this.healthBar.DrawHealth();
            this.clock.DrawTime();
            this.pointCounter.DrawPoints();
            this.levelCounter.DrawLevel();
            
        }

        public override void Update(float time)
        {
            this.ChangeAnimationBasedOnDirection();
            
            this.animation.Update();
            this.clock.UpdateTime(time);

            if (this.rectangle.X >= Game1.LeftBoundary & this.rectangle.X <= Game1.RightBoundary)
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
