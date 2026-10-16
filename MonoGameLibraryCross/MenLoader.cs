using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using System.Collections.Generic;
namespace MonoGameLibraryCross
{
    public class MenLoader
    {
        List<PacMan> pacManList = new List<PacMan>();

        int TotalPacMan = 5;
        float pspeed = 100f;

        public MenLoader(Game game)
        {
        }

        public void LoadContent(Game game)
        {
            int columns = 10;
            int spacing = 100;

            for (int i = 0; i < TotalPacMan; i++)
            {
                int column = i % columns;
                int row = i / columns;

                float xOffset = (row % 2) * (spacing / 2f);

                pacManList.Add(
                    new PacMan(
                        game,
                        game.Content.Load<Texture2D>("pacmanSingle"),
                        new Vector2(column * spacing + xOffset, row * spacing),
                        new Vector2(1, 0),
                        pspeed
                    )
                );
            }
        }

        public void Update(GameTime gameTime, Wall wall)
        {
            foreach (var pacMan in pacManList)
            {
                pacMan.Update(gameTime);
                foreach (var pacMan1 in pacManList)
                {
                    if (pacMan.PerPixelCollision(pacMan1))
                    {
                        pacMan1.Speed *= -1;
                        pacMan.Speed *= -1;
                    }
                }
                foreach (Rectangle wallRectangle in wall.GetWallRectangles())
                {
                    if (pacMan.LocationRect.Intersects(wallRectangle))
                    {
                        pacMan.Speed *= -1;
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var pacMan in pacManList)
            {
                pacMan.Draw(spriteBatch);
            }
        }
    }

}
