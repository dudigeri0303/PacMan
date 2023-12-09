using Microsoft.Xna.Framework.Input;
using PacMan.Entities.Player;

namespace PacMan.PacManGame
{
    public class KeyInputHandler
    {
        private KeyboardState keybordState;
        private bool upKeyBool;
        private bool downKeyBool;
        private bool leftKeyBool;
        private bool rightKeyBool;

        public KeyInputHandler()
        {
            this.upKeyBool = false;
            this.downKeyBool = false;
            this.leftKeyBool = false;
            this.rightKeyBool = false;
        }

        private void IsKeysPressed()
        {
            if (this.keybordState.IsKeyDown(Keys.Up))
            {
                this.upKeyBool = true;
               /* this.downKeyBool = false;
                this.leftKeyBool = false;
                this.rightKeyBool = false; */
            }
            if (this.keybordState.IsKeyDown(Keys.Down)) 
            {
                this.downKeyBool= true;
               /* this.upKeyBool = false;
                this.leftKeyBool = false;
                this.rightKeyBool = false;*/
            }
            else if (this.keybordState.IsKeyDown(Keys.Left))
            { 
                this.leftKeyBool = true;
                /*this.upKeyBool = false;
                this.downKeyBool = false;
                this.rightKeyBool = false;*/
            }
            else if (this.keybordState.IsKeyDown(Keys.Right))
            {
                this.rightKeyBool = true;
              /*  this.upKeyBool = false;
                this.downKeyBool = false;
                this.leftKeyBool = false; */
            }
        }

        private void IsKeysReleased()
        {
            if (this.keybordState.IsKeyUp(Keys.Up))
            {
                this.upKeyBool = false;
            }
            if (this.keybordState.IsKeyUp(Keys.Down))
            {
                this.downKeyBool = false;
            }
            if (this.keybordState.IsKeyUp(Keys.Left))
            {
                this.leftKeyBool = false;
            }
            if (this.keybordState.IsKeyUp(Keys.Right))
            {
                this.rightKeyBool = false;
            }
        }

        private void ActionBasedOnPressedKey(Player player)
        {
            //& player.PossibleDirections.Contains(Entities.Direction.UP)
            if (this.upKeyBool == true & player.Direction != Entities.Direction.UP )
            {
                if (!player.Collided & player.Direction != Entities.Direction.DOWN) { player.PreviousDirection = player.Direction; }
                player.Direction = Entities.Direction.UP;
            }
            //& player.PossibleDirections.Contains(Entities.Direction.DOWN)
            if (this.downKeyBool == true & player.Direction != Entities.Direction.DOWN )
            {
                if (!player.Collided & player.Direction != Entities.Direction.UP) { player.PreviousDirection = player.Direction; }
                player.Direction = Entities.Direction.DOWN;
            }
            //& player.PossibleDirections.Contains(Entities.Direction.LEFT)
            if (this.leftKeyBool == true & player.Direction != Entities.Direction.LEFT )
            {
                if (!player.Collided & player.Direction != Entities.Direction.RIGHT) { player.PreviousDirection = player.Direction; }
                player.Direction = Entities.Direction.LEFT;
            }
            //& player.PossibleDirections.Contains(Entities.Direction.RIGHT)
            if (this.rightKeyBool == true & player.Direction != Entities.Direction.RIGHT )
            {
                if (!player.Collided & player.Direction != Entities.Direction.LEFT) { player.PreviousDirection = player.Direction; }
                player.Direction = Entities.Direction.RIGHT;
            }
        }

        public void HandleKey(Player player)
        {
            this.keybordState = Keyboard.GetState();
            this.IsKeysPressed();
            this.IsKeysReleased();
            this.ActionBasedOnPressedKey(player);
        }
    }
}
