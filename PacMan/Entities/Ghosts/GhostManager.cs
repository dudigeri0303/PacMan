using PacMan.Entities.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Entities.Ghosts
{
    public class GhostManager
    {
        private Blinky blinky;
        private Inky inky;
        private Pinky pinky;
        private Clyde clyde;
        private List<GhostBase> ghosts;

        private float timeElapsed;

        public GhostManager()
        {
            this.blinky = new Blinky(312, 336, 24, 24);
            this.pinky = new Pinky(312, 408, 24, 24);
            this.inky = new Inky(312, 432, 24, 24, this.blinky);
            this.clyde = new Clyde(336, 408, 24, 24);

            this.ghosts = new List<GhostBase>()
            {
                this.inky, this.clyde, this.blinky, this.pinky
            };

            this.timeElapsed = 0;
        }

        private void ManageBlinky() 
        {
        
        }

        private void ManagePinky()
        {
            if(this.blinky.TileLocation != null) 
            {
                if (this.pinky.MovementMode == Modes.IDLEINHOUSE & (this.blinky.TileLocation.i < 13 || this.blinky.TileLocation.i > 14))
                {
                    this.pinky.AllowDoor = true;
                    this.pinky.MovementMode = Modes.START;
                }
            }
            

        }

        private void ManageInky(Player.Player player)
        {
            if(player.Points > 40 & this.inky.MovementMode == Modes.IDLEINHOUSE) 
            {
                this.inky.AllowDoor = true;
                this.inky.MovementMode = Modes.START;
            }

        }

        private void ManageClyde(Player.Player player)
        {
            if (player.Points > 72 & this.clyde.MovementMode == Modes.IDLEINHOUSE)
            {
                this.clyde.AllowDoor = true;
                this.clyde.MovementMode = Modes.START;
            }
        }

        private void ChangeMode(Player.Player player, float seconds) 
        {

            foreach (GhostBase ghost in this.ghosts)
            {
                if (ghost.TimerRunning) 
                { 
                    ghost.TimeElapsed = seconds;
                    ghost.ChangeModeBasedOnTime();
                }

                if (ghost.TileLocation == ghost.StartTargetTile & ghost.MovementMode == Modes.START)
                {
                    ghost.AllowDoor = false;
                    ghost.TimerRunning = true;
                    ghost.MovementMode = Modes.SCATTER;
                }
            }

            this.ManageBlinky();
            this.ManagePinky();
            this.ManageInky(player);
            this.ManageClyde(player);

            
        }


        public void Update(Player.Player player, float seconds)
        {
            this.ChangeMode(player, seconds);
            foreach (var ghost in this.ghosts)
            {
                ghost.UpdateGhost(player, seconds);
            }
        }

        public void Draw() 
        {
            foreach (var ghost in this.ghosts)
            {
                ghost.Draw();
            }
        }
    }
}
