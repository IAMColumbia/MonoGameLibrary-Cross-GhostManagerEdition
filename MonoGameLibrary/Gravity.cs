using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
//using Microsoft.Xna.Framework.Graphics;
//using MonoGameLibrary;
//using System.Collections.Generic;
namespace MonoGameLibraryCross
{
    public class Gravity
    {
        static Vector2 GravityDir = new Vector2(0, 1);
        static float GravityAccel = 1.8f;
        static Gravity() { }

        public static void ApplyGrav(GameTime gameTime, Sprite sprite)
        {
            sprite.Direction += GravityDir * GravityAccel * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
