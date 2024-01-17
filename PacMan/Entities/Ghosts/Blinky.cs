using Microsoft.Xna.Framework.Graphics;
using PacMan.Entities.Ghosts.GhostAccessories;
using PacMan.Map;

namespace PacMan.Entities.Ghosts
{
    public class Blinky : GhostBase, ILevelUp
    {
        public Blinky(int x, int y, int width, int height, int numOfFrames, string path, string fileName) : base(x, y, width, height, numOfFrames, path, fileName)
        {
            this.movementMode = Modes.SCATTER;

            this.scatterTargetTile = Map.Map.GetInstance().Tiles[24, 0];
            this.houseTargetTile = Map.Map.GetInstance().Tiles[14, 18];
            this.startTargetTile = Map.Map.GetInstance().Tiles[13, 14];

            this.timer = new Timer(true);
        }

        protected override void IdleInHouse(Player.Player player, Blinky blinky)
        {
            this.direction = Direction.NONE;

            if (this.MovementMode == Modes.IDLEINHOUSE)
            {
                this.AllowDoor = true;
                this.MovementMode = Modes.START;
            }
        }

        protected override void Chase(Player.Player player)
        {
            Tile targetTile = player.TileLocation;
            this.ChangeDirectionBasedOnTarget(targetTile);
        }

        public void ResetForLevelUp(int x, int y)
        {
            this.position.X = x;
            this.position.Y = y;
            this.UpdateRectValue();
            this.MovementMode = Modes.SCATTER;
            this.timer.TimerRunning = true;
            this.timer.TimeEladpsed = 0;
            this.timer.FrightenedTimeElapsed = 0;
            this.timer.FrightenedTimerRunning = false;
            this.speed = 2;
            this.animation.SpriteSheet = Texture2D.FromFile(Game1._graphics.GraphicsDevice, Game1.PathToGhostImages + this.animation.FileName);
        }
    }
}
