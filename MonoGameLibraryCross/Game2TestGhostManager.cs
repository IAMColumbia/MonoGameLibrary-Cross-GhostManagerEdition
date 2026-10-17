using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;
namespace MonoGameLibraryCross
{
    public class Game2TestGhostManager : Game

    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public GhostManager ghostManager;
        public GameConsole console;
        FPS fps;

        public Game2TestGhostManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ghostManager = new GhostManager(this);
            console = new GameConsole(this);
            FPS fps = new FPS(this, false, false);
            this.Components.Add(fps);
            this.Components.Add(console);
            this.Components.Add(ghostManager);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            console.Initialize();
            console.ClearConsole();
        }
        KeyboardState prevKeys;

        private bool WasReleased(KeyboardState current, Keys key) { return current.IsKeyUp(key) && prevKeys.IsKeyDown(key); }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            console.DebugText = $"Num Ghosts = {ghostManager.numGhosts}\n" +
                                "To add a ghost press [Up Arrow].\n" +
                                "To remove a ghost press [Down Arrow].\n" +
                                "To quick-add or quick-remove use [W] and [S} respectively.";

            if (WasReleased(keys, Keys.Up))
                CallAddGhost();
            if (WasReleased(keys, Keys.Down))
                CallRemoveGhost();
            if (keys.IsKeyDown(Keys.W))
                CallAddGhost();
            if (keys.IsKeyDown(Keys.S))
                CallRemoveGhost();

            prevKeys = keys;
            base.Update(gameTime);
        }
        public void CallAddGhost()
        {
            Ghost newGhost = new Ghost(this);
            ghostManager.AddGhost(newGhost);
        }
        public void CallRemoveGhost()
        {
            if (ghostManager.numGhosts > 0)
                ghostManager.RemoveGhost(ghostManager.ghosts[^1]);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ghostManager.Draw(gameTime);
            base.Draw(gameTime);
        }

    }
}
