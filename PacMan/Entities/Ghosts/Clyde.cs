using Microsoft.Xna.Framework.Graphics;
using PacMan.Entities.Ghosts.GhostAccessories;

namespace PacMan.Entities.Ghosts
{
    public class Clyde : GhostBase, ILevelUp
    {
        public Clyde(int x, int y, int width, int height, int numOfFrames, string path, string fileName) : base(x, y, width, height, numOfFrames, path, fileName)
        {
            this.movementMode = Modes.IDLEINHOUSE;

            this.scatterTargetTile = Map.Map.GetInstance().Tiles[0, 31];
            this.houseTargetTile = Map.Map.GetInstance().Tiles[14, 17];
            this.startTargetTile = Map.Map.GetInstance().Tiles[13, 14];

            
            this.timer = new Timer(false);
        }


        protected override void IdleInHouse(Player.Player player, Blinky blinky)
        {
            this.nextDirection = Direction.NONE;

            if (player.PointCounter.Points > 37 & this.MovementMode == Modes.IDLEINHOUSE)
            {
                this.AllowDoor = true;
                this.MovementMode = Modes.START;
            }
        }

        protected override void Chase(Player.Player player)
        {
            double distance = this.CalcGreedyValue(this.tileLocation, player.TileLocation);

            if (distance > 96) { this.ChangeDirectionBasedOnTarget(player.TileLocation); }
            else { this.ChangeDirectionBasedOnTarget(this.scatterTargetTile); }
        }

        public void ResetForLevelUp(int x, int y)
        {
            this.position.X = x;
            this.position.Y = y;
            this.UpdateRectValue();
            this.MovementMode = Modes.IDLEINHOUSE;
            this.timer.TimerRunning = true;
            this.timer.TimeEladpsed = 0;
            this.timer.FrightenedTimeElapsed = 0;
            this.timer.FrightenedTimerRunning = false;
            this.speed = 2;
            this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToGhostImages + this.animation.FileName);
        }
    }
}
