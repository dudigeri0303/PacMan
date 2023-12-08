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
        private KeyInputHandler keyInputHandler;

        public GameBase() 
        {
            this.tileMap = new TileMap($"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Assets\\MapAssets\\MapDummy.txt");
            this.player = new Player(24, 96, 24, 24);
            this.keyInputHandler = new KeyInputHandler();
        }

        public void UpdateGame() 
        {
            this.keyInputHandler.HandleKey(this.player);
            this.player.Update(this.tileMap);
        }
        public void DrawGame() 
        {
            this.tileMap.DrawTileMap();
            this.player.Draw();
        }
    }
}
