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
        private TileMap tileMap;
        private Player player;

        public GameBase() 
        {
            this.tileMap = new TileMap($"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Assets\\MapAssets\\Map.txt");
            this.player = new Player(24, 96, 32, 32);
        }

        public void UpdateGame() 
        {
        
        }
        public void DrawGame() 
        {
            this.tileMap.DrawTileMap();
            this.player.Draw();
        }
    }
}
