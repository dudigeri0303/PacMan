using PacMan.Entities.Ghosts;
using PacMan.Entities.Player;

namespace PacMan.PacManGame
{
    public class GameBase
    {
        private Player player;
        private GhostManager ghostManager;
        private KeyInputHandler keyInputHandler;

        public GameBase() 
        {
            this.ghostManager = new GhostManager();
            this.player = new Player(320, 624, 24, 24, 8, Game1.PathToPlayerImages, "pacman_right.png", Map.Map.GetInstance().Pellets, this.ghostManager);
            this.keyInputHandler = new KeyInputHandler();
        }

        public void LevelUp() 
        {
            this.ghostManager.ResetForLevelUp();
            this.player.ResetForLevelUp(320, 624);
            Map.Map.GetInstance().ResetMap();
        }

        public void UpdateGame(float time) 
        {
            this.keyInputHandler.HandleKey(this.player);
            this.player.Update(time);
            this.ghostManager.Update(this.player, time);
        }
        public void DrawGame() 
        {
            Map.Map.GetInstance().DrawTileMap();
            this.player.Draw();
            this.ghostManager.Draw();
        }
    }
}
