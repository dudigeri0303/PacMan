using Microsoft.Xna.Framework;
using PacMan.Entities.Ghosts;
using PacMan.Entities.Player;
using PacMan.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PacMan.PacManGame
{
    public class GameBase
    {
        private Player player;
        private GhostManager ghostManager;
        private KeyInputHandler keyInputHandler;

        public GameBase() 
        {
            this.player = new Player(96, 96, 24, 24, Map.Map.GetInstance().Pellets);
            this.ghostManager = new GhostManager();
            this.keyInputHandler = new KeyInputHandler();
        }

        public void UpdateGame(float seconds) 
        {
            this.keyInputHandler.HandleKey(this.player);
            this.player.Update();
            this.ghostManager.Update(this.player, seconds);

            
        }
        public void DrawGame() 
        {
            Map.Map.GetInstance().DrawTileMap();
            this.player.Draw();
            this.ghostManager.Draw();
        }
    }
}
