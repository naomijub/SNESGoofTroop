using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoofTroopRemake
{
    public class InputHandler
    {
        private MouseState currentMouseState, previousMouseState;
        private KeyboardState currentKeyboardState, previousKeyboardState;

        public InputHandler(MouseState mouseState, KeyboardState keyboardState) {
            currentMouseState = mouseState;
            currentKeyboardState = keyboardState; 
        }

        public void Update()
        {
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }

        public Vector2 MousePosition
        {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
        }

        public bool MouseLeftButtonPressed()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }

        public bool MouseLeftButtonDown()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool KeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
        }

        public bool KeyDown(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k);
        }
    }
}
