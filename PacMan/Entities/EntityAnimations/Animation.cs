using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacMan.Entities.EntityAnimations
{
    public class Animation
    {
        private Texture2D spriteSheet;
        public Texture2D SpriteSheet 
        { 
            get { return spriteSheet; }
            set { this.spriteSheet = value; } 
        }

        private int numOfFrames;
        private List<Rectangle> sourceRectangles;
        private int frameIndex;
        private float frameTime;
        private float frameTimeLeft;
        private bool isActive;
        private Rectangle position;
        public Rectangle Rectangle { get { return position; } }
        private string fileName;
        public string FileName {
            get { return fileName; }
            set { fileName = value; }
        }

        public Animation(int numOfFrames, float frameTime, int frameWidth, int frameHeight, string path, string fileName)
        {
            this.fileName = fileName;
            this.spriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, path + fileName);
            this.numOfFrames = numOfFrames;
            this.frameIndex = 0;
            this.frameTime = frameTime;
            this.frameTimeLeft = this.frameTime;

            this.sourceRectangles = new List<Rectangle>();
            this.FillSourceRectangleList(frameWidth, frameHeight);
        }

        private void FillSourceRectangleList(int frameWidth, int frameHeight, int row = 1) 
        {
            for (int i = 0; i < numOfFrames; i++)
            {
                this.sourceRectangles.Add(new(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
            }
        }

        public void Reset() 
        {
            this.frameIndex = 0;
        }

        public void Update() 
        {
            this.frameTimeLeft -= Game1.TotalGameTime;
            if (this.frameTimeLeft <= 0) 
            {
                this.frameTimeLeft += this.frameTime;
                this.frameIndex = (this.frameIndex + 1) % this.numOfFrames;
            }
        }

        public void DrawAnimation(Vector2 position) 
        {
            Game1._spriteBatch.Draw(this.spriteSheet, position, this.sourceRectangles[this.frameIndex], Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }
}
