using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace MonoGameLibraryCross
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        //PacMan pacMan;
        MenLoader menLoader;
        Wall wall;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //pacMan = new PacMan(this);
            menLoader = new MenLoader(this);
            wall = new Wall(this);
            //Add pacman to the game
            //this.Components.Add(pacMan);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menLoader.LoadContent(this);
            wall.spriteTexture = Content.Load<Texture2D>("pacmanSingle");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            menLoader.Update(gameTime, wall);
            wall.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            menLoader.Draw(gameTime, spriteBatch);
            wall.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Have a handful of pacmen bouncing around the screen. have input to add walls. have input to add more pacmen.
        //first: have multiple pacmen who collide with one another

    }
}
