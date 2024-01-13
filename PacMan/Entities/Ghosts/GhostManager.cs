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
        public List<GhostBase> Ghosts { get { return ghosts; } }

        private float timeElapsed;

        public GhostManager()
        {
            this.blinky = new Blinky(312, 336, 24, 24, 3, Game1.PathToGhostImages, "blinky_body.png");
            this.pinky = new Pinky(312, 408, 24, 24, 3, Game1.PathToGhostImages, "pinky_body.png");
            this.inky = new Inky(312, 432, 24, 24, 3, Game1.PathToGhostImages, "inky_body.png", this.blinky);
            this.clyde = new Clyde(336, 408, 24, 24, 3, Game1.PathToGhostImages, "clyde_body.png");

            this.ghosts = new List<GhostBase>()
            {
                this.inky, this.clyde, this.blinky, this.pinky
            };

            this.timeElapsed = 0;
        }

        public void MakeGhostsFrightened() 
        {
            foreach (var ghost in this.ghosts) 
            {
                if (ghost.MovementMode != Modes.IDLEINHOUSE & ghost.MovementMode != Modes.RUNBACKTOHOUSE) 
                { 
                    ghost.MovementMode = Modes.FRIGHTENED;
                }
            }
        }

        public void Update(Player.Player player, float seconds)
        {
            foreach (var ghost in this.ghosts)
            {
                ghost.UpdateGhost(player, seconds, this.blinky);
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
