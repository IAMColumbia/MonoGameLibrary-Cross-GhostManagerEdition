using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using System.Collections.Generic;

namespace MonoGameLibrary
{
    public class Wall : DrawableSprite
    {
        public enum WallState
        {
            None,
            Horizontal,
            Vertical,
            Both
        }

        WallState wallState = WallState.None;

        List<Rectangle> wallRectangles = new List<Rectangle>();

        public Wall(Game game) : base(game) { }

        public Wall(Game game, Texture2D texture) : base(game)
        {
            this.SpriteTexture = this.spriteTexture = texture;
            this.Origin = Vector2.Zero;
            this.DrawColor = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.D0))
                wallState = WallState.None;

            if (keyboard.IsKeyDown(Keys.D1))
                wallState = WallState.Horizontal;

            if (keyboard.IsKeyDown(Keys.D2))
                wallState = WallState.Vertical;

            if (keyboard.IsKeyDown(Keys.D3))
                wallState = WallState.Both;

            CreateWallRectangles();

            base.Update(gameTime);
        }

        private void CreateWallRectangles()
        {
            wallRectangles.Clear();

            int spacing = SpriteTexture.Width;

            // Starting position of the wall
            int startX = 0;
            int startY = 0;

            if (wallState == WallState.Horizontal ||
                wallState == WallState.Both)
            {
                for (int i = 0; i < 15; i++)
                {
                    wallRectangles.Add(
                        new Rectangle(
                            startX + i * spacing,
                            startY + 250,
                            SpriteTexture.Width,
                            SpriteTexture.Height
                        )
                    );
                    startY = 0;
                }
            }

            if (wallState == WallState.Vertical ||
                wallState == WallState.Both)
            {
                for (int i = 0; i < 10; i++)
                {
                    wallRectangles.Add(
                        new Rectangle(
                            startX + 350,
                            startY + i * spacing,
                            SpriteTexture.Width,
                            SpriteTexture.Height
                        )
                    );
                }
                startX = 0;
            }
        }

        public List<Rectangle> GetWallRectangles()
        {
            return wallRectangles;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Rectangle rectangle in wallRectangles)
            {
                spriteBatch.Draw(
                    SpriteTexture,
                    rectangle,
                    Color.White
                );
            }
        }
    }
}