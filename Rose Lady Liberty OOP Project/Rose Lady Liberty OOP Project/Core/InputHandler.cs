using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace RoseLadyLibertyOOPProject.Core
{
    public class InputHandler
    {
        private KeyboardState keyboardState;
        private Dictionary<Keys, bool> handledKeys;
        private Dictionary<Keys, Action> handledKeysActions;
 
        public InputHandler()
        {
            handledKeys = new Dictionary<Keys, bool>();
            handledKeysActions = new Dictionary<Keys, Action>();
        }

        public static MouseState MouseState
        {
            get; private set;
        }

        public void AddKeyToHandle(Keys key, Action actionToPerform)
        {
            if (!this.handledKeys.ContainsKey(key))
            {
                this.handledKeys.Add(key, false);
                this.handledKeysActions.Add(key, actionToPerform);
            }
            else
            {
                throw new Exception("There is already handler for the key you specified!");
            }
        }

        public void Update()
        {
            MouseState = Mouse.GetState();
            this.keyboardState = Keyboard.GetState();

            for (int key = 0; key < handledKeys.Keys.Count; key++)
            {
                if (this.keyboardState.IsKeyDown(handledKeys.Keys.ElementAt(key)) 
                    && handledKeys[handledKeys.Keys.ElementAt(key)] == false)
                {
                    this.handledKeysActions[handledKeys.Keys.ElementAt(key)]();
                    this.handledKeys[handledKeys.Keys.ElementAt(key)] = true;
                }
                else if (this.keyboardState.IsKeyUp(handledKeys.Keys.ElementAt(key)) 
                    && handledKeys[handledKeys.Keys.ElementAt(key)] == true)
                {
                    this.handledKeys[handledKeys.Keys.ElementAt(key)] = false;
                }
            }
        }
    }
}
