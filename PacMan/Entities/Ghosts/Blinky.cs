using PacMan.Entities.Ghosts.GhostAccessories;
using PacMan.Map;

namespace PacMan.Entities.Ghosts
{
    public class Blinky : GhostBase
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
    }
}
