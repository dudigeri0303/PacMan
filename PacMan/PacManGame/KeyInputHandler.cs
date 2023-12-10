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
            }
            if (this.keybordState.IsKeyDown(Keys.Down)) 
            {
                this.downKeyBool= true;
            }
            else if (this.keybordState.IsKeyDown(Keys.Left))
            { 
                this.leftKeyBool = true;
            }
            else if (this.keybordState.IsKeyDown(Keys.Right))
            {
                this.rightKeyBool = true;
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
            if (this.upKeyBool) 
            {
                player.NextDirection = Entities.Direction.UP;
            }
            else if (this.downKeyBool) 
            {
                player.NextDirection = Entities.Direction.DOWN;
            }
            else if (this.leftKeyBool) 
            {
                player.NextDirection = Entities.Direction.LEFT;
            }
            else if (this.rightKeyBool) 
            {
                player.NextDirection = Entities.Direction.RIGHT;
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
