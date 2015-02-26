using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace RouteDefense.Core
{
    public class InputHandler
    {
        public static KeyboardState CurrentKeyboardState;
        public static KeyboardState OldKeyboardState;

        public static MouseState CurrentMouseState;
        public static MouseState OldMouseState;

        public InputHandler()
        {
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
        }

        public static bool IsClicked(Keys key)
        {
            if (CurrentKeyboardState.IsKeyUp(key) && OldKeyboardState.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }

        public static bool IsHolding(Keys key)
        {
            if (CurrentKeyboardState.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }

        public void Update()
        {
            OldMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
            
            OldKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }
    }
}
