using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PacMan.Entities.Ghosts;
using PacMan.Entities.Player;
using PacMan.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PacMan.PacManGame
{
    public class GameBase
    {
        private Player player;
        public Player Player { get { return player; } }
        private GhostManager ghostManager;
        private KeyInputHandler keyInputHandler;
        private Scenes scene;
        private Timer timer;
        private Clock clock;
        public Scenes Scene 
        { 
            get { return scene; }
            set { scene = value; }
        }

        public GameBase() 
        {
            this.ghostManager = new GhostManager();
            this.player = new Player(320, 624, 24, 24, 8, Game1.PathToPlayerImages, "pacman_right.png", Map.Map.GetInstance().Pellets, this.ghostManager);
            this.keyInputHandler = new KeyInputHandler();
            this.scene = Scenes.START;
            this.timer = new Timer(true);
            this.clock = new Clock();   
        }

        private void UpdateStartScene() 
        {
        
        }

        private void DrawStartScene() 
        {
            Map.Map.GetInstance().DrawTileMap();
            this.player.Draw();
            this.ghostManager.Draw();
        }

        private void UpdateGameScene(float time) 
        {
            this.keyInputHandler.HandleKey(this.player);
            this.player.Update(time);
            this.ghostManager.Update(this.player, time);
        }

        private void DrawGameScene() 
        {
            Map.Map.GetInstance().DrawTileMap();
            this.player.Draw();
            this.ghostManager.Draw();
        }

        private void UpdateGameOverScene()
        {

        }

        private void DrawGameOverScene()
        {
            Map.Map.GetInstance().DrawTileMap();
            this.player.Draw();
            this.ghostManager.Draw();
        }

        public void UpdateGame(float time) 
        {
            this.clock.UpdateTime(time);

            if (this.timer.TimerRunning) 
            {
                this.timer.IncraseTimeElapsed(time);
            }
            this.timer.ChangeSceneBasedOnTime(this);

            switch (this.scene) 
            {
                case Scenes.START:
                    this.UpdateStartScene();
                    break;
                case Scenes.GAME:
                    this.UpdateGameScene(time);
                    break;
                case Scenes.GAMEOVER:
                    this.UpdateGameOverScene();
                    break;
            }
        }

        

        public void DrawGame() 
        {   
            switch (this.scene)
            {
                case Scenes.START:
                    this.DrawStartScene();
                    break;
                case Scenes.GAME:
                    this.DrawGameScene();
                    break;
                case Scenes.GAMEOVER:
                    this.DrawGameOverScene();
                    break;
            }
            this.clock.DrawTime();
        }
    }
}
