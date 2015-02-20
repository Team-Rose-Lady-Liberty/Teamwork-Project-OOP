﻿using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RouteDefense.Core;
using RouteDefense.Enumerations;
using RouteDefense.UI;
using RouteDefense.UI.GUIElements;

namespace RouteDefense.Models.GameScreens
{
    public class MainMenu : GameScreen
    {
        private List<GUIElement> elements;

        public MainMenu()
        {
            elements = new List<GUIElement>()
            {
                new Button(new Rectangle(366, 178, 100, 24), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/new game.png"),
                    delegate(){SubGameEngine.CurrentGameState = GameState.CharacterSelection;}),

                new Button(new Rectangle(357, 228, 118, 24), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/game story.png"),
                    delegate(){SubGameEngine.CurrentGameState = GameState.Game;}),

                new Button(new Rectangle(364, 278, 104, 29), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/quit game.png"),
                    delegate(){}),
                new Label(new Rectangle(10,20, 0, 0), "Test")
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (GUIElement item in elements)
            {
                item.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GUIElement item in elements)
            {
                item.Update(gameTime);
            }
        }

        public override void HandleInput(InputHandler inputHandler)
        {
            
        }
    }
}
