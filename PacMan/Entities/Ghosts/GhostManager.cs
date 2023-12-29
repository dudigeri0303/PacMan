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

        public GhostManager()
        {
            this.blinky = new Blinky(312, 336, 24, 24);
            this.pinky = new Pinky(312, 432, 24, 24);
            this.inky = new Inky(288, 432, 24, 24, this.blinky);
            this.clyde = new Clyde(336, 432, 24, 24);

            this.ghosts = new List<GhostBase>()
            {
                this.inky, this.clyde, this.blinky, this.pinky
            };
        }


        public void Update(Player.Player player, float seconds)
        {
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
