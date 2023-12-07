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

        public GameBase() 
        {
            this.tileMap = new TileMap($"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Map\\Map.txt");
        }

        public void UpdateGame() 
        {
        
        }
        public void DrawGame() 
        {
            this.tileMap.DrawTileMap();
        }
    }
}
