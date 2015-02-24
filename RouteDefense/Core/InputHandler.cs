using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace RouteDefense.Core
{
    public class InputHandler
    {
        private Dictionary<Keys, bool> handledKeys;
        private Dictionary<Keys, Action> handledKeysActions;

        public InputHandler()
        {
            handledKeys = new Dictionary<Keys, bool>();
            handledKeysActions = new Dictionary<Keys, Action>();
        }

        public static MouseState MouseState
        {
            get { return Mouse.GetState(); }
        }

        public static KeyboardState KeyboardState
        {
            get { return Keyboard.GetState(); }
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
            for (int key = 0; key < handledKeys.Keys.Count; key++)
            {
                if (KeyboardState.IsKeyDown(handledKeys.Keys.ElementAt(key))
                    && handledKeys[handledKeys.Keys.ElementAt(key)] == false)
                {
                    this.handledKeysActions[handledKeys.Keys.ElementAt(key)]();
                    this.handledKeys[handledKeys.Keys.ElementAt(key)] = true;
                }
                else if (KeyboardState.IsKeyUp(handledKeys.Keys.ElementAt(key))
                         && handledKeys[handledKeys.Keys.ElementAt(key)] == true)
                {
                    this.handledKeys[handledKeys.Keys.ElementAt(key)] = false;
                }
            }
        }
    }
}
