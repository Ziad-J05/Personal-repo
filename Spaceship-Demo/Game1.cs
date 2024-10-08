﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship_Demo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Spaceship ship;
        private StarRenderer starRenderer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Texture2D shipTexture = Content.Load<Texture2D>("ship");
            Texture2D particleTexture = Content.Load<Texture2D>("trail particle");
            Texture2D starTexture = Content.Load<Texture2D>("star");

            ship = new Spaceship(
                new Vector2(_graphics.PreferredBackBufferWidth * 1/5, _graphics.PreferredBackBufferHeight / 2),
                0.2f,
                shipTexture,
                particleTexture,
                200,
                20);

            starRenderer = new StarRenderer(
                new Vector2(_graphics.PreferredBackBufferWidth * 4/5, _graphics.PreferredBackBufferHeight/2),
                _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight,
                starTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState keyboardState = Keyboard.GetState();
            ship.Move(keyboardState, _graphics.PreferredBackBufferHeight);

            starRenderer.AddStars(4);
            starRenderer.MoveStars();
            starRenderer.DiscardStars();
                
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            starRenderer.DrawStars(_spriteBatch);
            ship.Draw(_spriteBatch);            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
