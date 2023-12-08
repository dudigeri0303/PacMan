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
                this.downKeyBool = false;
                this.leftKeyBool = false;
                this.rightKeyBool = false;
            }
            else if (this.keybordState.IsKeyDown(Keys.Down)) 
            {
                this.downKeyBool= true;
                this.upKeyBool = false;
                this.leftKeyBool = false;
                this.rightKeyBool = false;
            }
            else if (this.keybordState.IsKeyDown(Keys.Left))
            { 
                this.leftKeyBool = true;
                this.upKeyBool = false;
                this.downKeyBool = false;
                this.rightKeyBool = false;
            }
            else if (this.keybordState.IsKeyDown(Keys.Right))
            {
                this.rightKeyBool = true;
                this.upKeyBool = false;
                this.downKeyBool = false;
                this.leftKeyBool = false;
            }
        }

        private void IsKeysReleased()
        {
            if (this.keybordState.IsKeyUp(Keys.Up))
            {
                this.upKeyBool = false;
            }
            else if (this.keybordState.IsKeyUp(Keys.Down))
            {
                this.downKeyBool = false;
            }
            else if (this.keybordState.IsKeyUp(Keys.Left))
            {
                this.leftKeyBool = false;
            }
            else if (this.keybordState.IsKeyUp(Keys.Right))
            {
                this.rightKeyBool = false;
            }
        }

        private void ActionBasedOnPressedKey(Player player)
        {
            if (this.upKeyBool == true & player.Direction != Entities.Direction.UP & player.PossibleDirections.Contains(Entities.Direction.UP))
            {
                player.Direction = Entities.Direction.UP;
            }
            else if (this.downKeyBool == true & player.Direction != Entities.Direction.DOWN & player.PossibleDirections.Contains(Entities.Direction.DOWN))
            {
                player.Direction = Entities.Direction.DOWN;
            }
            else if (this.leftKeyBool == true & player.Direction != Entities.Direction.LEFT & player.PossibleDirections.Contains(Entities.Direction.LEFT))
            {
                player.Direction = Entities.Direction.LEFT;
            }
            else if (this.rightKeyBool == true & player.Direction != Entities.Direction.RIGHT & player.PossibleDirections.Contains(Entities.Direction.RIGHT))
            {
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
