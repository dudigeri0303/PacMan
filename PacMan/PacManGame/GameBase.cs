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
        private List<GhostBase> ghosts;
        private KeyInputHandler keyInputHandler;

        public GameBase() 
        {
            this.player = new Player(96, 96, 24, 24, Map.Map.GetInstance().Pellets);
            this.ghosts = new List<GhostBase>()
            {
                new Blinky(312, 336, 24, 24),
                new Pinky(264, 672, 24, 24),
                new Inky(120, 96, 24, 24),
                new Clyde(576, 96, 24, 24)
            };
            this.ghosts[2].Blinky = this.ghosts[0];
            this.keyInputHandler = new KeyInputHandler();
        }

        public void UpdateGame(float seconds) 
        {
            this.keyInputHandler.HandleKey(this.player);
            this.player.Update();

            foreach(var ghost in this.ghosts) 
            {
                ghost.UpdateGhost(this.player, seconds);
            }
        }
        public void DrawGame() 
        {
            Map.Map.GetInstance().DrawTileMap();
            this.player.Draw();

            foreach (var ghost in this.ghosts)
            {
                ghost.Draw();
            }
        }
    }
}
