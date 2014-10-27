using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using DebugTerminal;
using System.Collections;

namespace TheMurk
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("courier");
            Terminal.Init(this, spriteBatch, font, GraphicsDevice);
            
            Terminal.SetSkin(TerminalThemeType.FIRE);
        }

        protected override void UnloadContent()
        {
            spriteBatch = null;
        }

        protected override void Update(GameTime gameTime)
        {
            Terminal.CheckOpen(Keys.P, Keyboard.GetState());
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (spriteManager.gameOver || spriteManager.gameRunOutOfTime || spriteManager.gameWon)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.N))
                {
                    this.Exit();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Y))
                {
                    UnloadContent();
                    Initialize();
                    LoadContent();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (spriteManager.gameOver)
            {
                GraphicsDevice.Clear(Color.AliceBlue);

                spriteBatch.Begin();
                string gameString = "You have been converted into a zombie....";
                spriteBatch.DrawString(font, gameString,
                    new Vector2((Window.ClientBounds.Width / 2)
                    - (font.MeasureString(gameString).X / 2),
                    (Window.ClientBounds.Height / 2)
                    - (font.MeasureString(gameString).Y / 2)),
                    Color.SaddleBrown);
                gameString = "Would you like to play again (Y/N)?";
                spriteBatch.DrawString(font, gameString,
                    new Vector2((Window.ClientBounds.Width / 2)
                    - (font.MeasureString(gameString).X / 2),
                    (Window.ClientBounds.Height / 2)
                    - (font.MeasureString(gameString).Y / 2) + 30),
                    Color.SaddleBrown);
                spriteBatch.End();
            }
            else if (spriteManager.gameRunOutOfTime)
            {
                GraphicsDevice.Clear(Color.AliceBlue);

                spriteBatch.Begin();
                string gameString = "Without a light to guide your way, The Murk is merely just another zombie...";
                spriteBatch.DrawString(font, gameString,
                    new Vector2((Window.ClientBounds.Width / 2)
                    - (font.MeasureString(gameString).X / 2),
                    (Window.ClientBounds.Height / 2)
                    - (font.MeasureString(gameString).Y / 2)),
                    Color.SaddleBrown);
                gameString = "Would you like to play again (Y/N)?";
                spriteBatch.DrawString(font, gameString,
                    new Vector2((Window.ClientBounds.Width / 2)
                    - (font.MeasureString(gameString).X / 2),
                    (Window.ClientBounds.Height / 2)
                    - (font.MeasureString(gameString).Y / 2) + 30),
                    Color.SaddleBrown);
                spriteBatch.End();
            }
            else if (spriteManager.gameWon)
            {
                GraphicsDevice.Clear(Color.AliceBlue);
                ArrayList list = new ArrayList();
                String gameString = "";
                String one = "You found a gps to guide your way to saftey!!!";
                String two = "It took you seconds.";
                String three = "Can you do better? That is something only you can decided.";
                String four = "So it begs the question? Would you like to play again? (Y/N)";
                spriteBatch.Begin();
                gameString = one;
                spriteBatch.DrawString(font, gameString,
                        new Vector2((Window.ClientBounds.Width / 2)
                        - (font.MeasureString(gameString).X / 2),
                        (Window.ClientBounds.Height /4)
                        - (font.MeasureString(gameString).Y / 2)),
                        Color.SaddleBrown);
                gameString = two;
                spriteBatch.DrawString(font, gameString,
                    new Vector2((Window.ClientBounds.Width / 2)
                    - (font.MeasureString(gameString).X / 2),
                    (Window.ClientBounds.Height / 2 - 25)
                    - (font.MeasureString(gameString).Y / 2)),
                    Color.SaddleBrown);
                gameString = three;
                spriteBatch.DrawString(font, gameString,
                    new Vector2((Window.ClientBounds.Width / 2)
                    - (font.MeasureString(gameString).X / 2),
                    (Window.ClientBounds.Height /2 + 25)
                    - (font.MeasureString(gameString).Y / 2)),
                    Color.SaddleBrown);
                gameString = four;
                spriteBatch.DrawString(font, gameString,
                    new Vector2((Window.ClientBounds.Width / 2)
                    - (font.MeasureString(gameString).X / 2),
                    (Window.ClientBounds.Height - Window.ClientBounds.Height / 4)
                    - (font.MeasureString(gameString).Y / 2)),
                    Color.SaddleBrown);


                spriteBatch.End();
            }
            else
            {
                Terminal.CheckDraw(true);
                GraphicsDevice.Clear(Color.CornflowerBlue);
                base.Draw(gameTime);
            }
        }
    }
}
