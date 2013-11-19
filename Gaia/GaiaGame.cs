#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Gaia;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Gaia
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GaiaGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //represents the player
        Player player;

        //represents the world
        World world;

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        //Mouse states used to track Mouse button press
        MouseState currentMouseState;
        MouseState previousMouseState;

        // A movement speed for the player
        float playerMoveSpeed
        {
            get;
            set;
        }



        public GaiaGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.world  = new World();

            this.player = new Player();
            // Set a constant player move speed
            this.playerMoveSpeed = 8.0f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //load the world ressources
            Vector2 playerPositionInWorld = new Vector2(world.Width / 2, world.Height / 2);
            world.Initialize(WorldSize.Small, playerPositionInWorld);

            //load the player ressources
            Vector2 playerPositionInScreen = new Vector2(
                GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(Content.Load<Texture2D>("Graphics\\player"), playerPositionInScreen);            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousGamePadState  = currentGamePadState;
            previousKeyboardState = currentKeyboardState;
            previousMouseState    = currentMouseState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState  = GamePad.GetState(PlayerIndex.One);
            currentMouseState    = Mouse.GetState();

            //Update the player
            UpdatePlayer(gameTime);


            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            float x_move = 0f;
            float y_move = 0f;

            if (currentGamePadState.ThumbSticks.Left.X != 0f || currentGamePadState.ThumbSticks.Left.Y != 0f)
            {
                // we use a gamepad
                x_move += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
                y_move -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;
            }
            else
            {
                // Use the Keyboard / Dpad
                if (currentKeyboardState.IsKeyDown(Keys.Left)
                 || currentKeyboardState.IsKeyDown(Keys.Q )
                 || currentGamePadState.DPad.Left == ButtonState.Pressed)
                {
                    x_move -= playerMoveSpeed;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Right)
                 || currentKeyboardState.IsKeyDown(Keys.D)
                 || currentGamePadState.DPad.Right == ButtonState.Pressed)
                {
                    x_move += playerMoveSpeed;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Up)
                 || currentKeyboardState.IsKeyDown(Keys.Z)
                 || currentGamePadState.DPad.Up == ButtonState.Pressed)
                {
                    y_move -= playerMoveSpeed;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Down)
                 || currentKeyboardState.IsKeyDown(Keys.S)                 
                 || currentGamePadState.DPad.Down == ButtonState.Pressed)
                {
                   y_move += playerMoveSpeed;
                }
            }

            if (x_move < 0)
            {
                this.player.Direction = PlayerDirection.Left;
            }
            else if (x_move > 0)
            {
                this.player.Direction = PlayerDirection.Right;
            }


                
            this.player.Position.X += x_move;
            this.player.Position.Y += y_move;

            // Make sure that the player does not go out of bounds
            this.player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            this.player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Start drawing
            this.spriteBatch.Begin();

            // Draw the world
            this.world.Draw(this.spriteBatch);

            // Draw the Player
            this.player.Draw(this.spriteBatch);

            // Stop drawing
            this.spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
