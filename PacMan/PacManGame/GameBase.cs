using PacMan.Entities.Ghosts;
using PacMan.Entities.Player;
using PacMan.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.PacManGame
{
    public class GameBase
    {
        private Player player;
        private List<GhostBase> ghosts;
        private KeyInputHandler keyInputHandler;

        public GameBase() 
        {
            this.player = new Player(96, 96, 24, 24);
            this.ghosts = new List<GhostBase>()
            {
                new Blinky(48, 96, 24, 24),
                new Pinky(96, 96, 24, 24)
            };
            this.keyInputHandler = new KeyInputHandler();
        }

        public void UpdateGame() 
        {
            this.keyInputHandler.HandleKey(this.player);
            this.player.Update();

            foreach(var ghost in this.ghosts) 
            {
                ghost.UpdateGhost(this.player);
            }
        }
        public void DrawGame() 
        {
            TileMap.GetInstance().DrawTileMap();
            this.player.Draw();

            foreach (var ghost in this.ghosts)
            {
                ghost.Draw();
            }
        }
    }
}
