using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace MonoGameLibrary
{
    public class Ghost : DrawableSprite
    {
        public Ghost(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            this.SpriteTexture = this.spriteTexture = this.Game.Content.Load<Texture2D>("PurpleGhost");
            this.Location = new Vector2(100, 100);
            base.LoadContent();
        }
    }
}
