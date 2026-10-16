using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using MonoGameLibraryCross;

namespace MonoGameLibrary
{
    public class PacMan : DrawableSprite
    {
        float acceleration = 500f;
        float maxSpeed = 300f;
        float deceleration = 700f;

        public PacMan(Game game) : base(game)
        {
        }

        public PacMan(Game game, Texture2D texture, Vector2 location, Vector2 direction, float speed) : base(game)
        {
            this.SpriteTexture = this.spriteTexture = texture;
            this.Location = location;
            this.Direction = direction;
            this.Speed = speed;

            this.Origin = Vector2.Zero;
            this.DrawColor = Color.White;
        }

        protected override void LoadContent()
        {
            //this.SpriteTexture = this.spriteTexture = this.Game.Content.Load<Texture2D>("pacmanSingle");
            //this.Location = new Vector2(GraphicsDevice.Viewport.Width / 2,
            //    GraphicsDevice.Viewport.Height / 2);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Location += Direction * Speed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;
            Gravity.ApplyGrav(gameTime, this);
            //Location = clampToViewport(Location);
            UpdatePacmanKeepOnScreen(GraphicsDevice.Viewport.Width,
              GraphicsDevice.Viewport.Height);
            //UpdatePacmanSpeed(gameTime);


            base.Update(gameTime);
        }
        #region pacman on screen code
        public void UpdatePacmanKeepOnScreen(int screenWidth, int screenHeight)
        {
            if (Location.X >
                screenWidth - SpriteTexture.Width)
            {
                Direction *= new Vector2(-1, 1);

                Location.X =
                    screenWidth - SpriteTexture.Width;
            }

            if (Location.X < 0)
            {
                Direction *= new Vector2(-1, 1);

                Location.X = 0;
            }

            if (Location.Y >
                screenHeight - SpriteTexture.Height)
            {
                Direction *= new Vector2(1, -1);

                Location.Y =
                    screenHeight - SpriteTexture.Height;
            }

            if (Location.Y < 0)
            {
                Direction *= new Vector2(1, -1);

                Location.Y = 0;
            }
        }
        #endregion

        #region pacman smooth acceleration code
        private void UpdatePacmanSpeed(GameTime gameTime)
        {
            //Speed for next frame
            if (this.Speed < 3000)
            {
                if (Keyboard.GetState().GetPressedKeys().Length > 0) //If there is any key press, the length of the Array of keys returned by GetPressedKeys wil be greater that 0
                {
                    this.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;  //Key down pacman has speed
                }
                else
                {
                    this.Speed -= deceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;    //No key down stop
                }
                if (this.Speed < 0)
                    this.Speed = 0;
            }
        }
        #endregion
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
