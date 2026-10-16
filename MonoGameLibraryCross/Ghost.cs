using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;

namespace MonoGameLibraryCross
{
    public class Ghost : DrawableSprite
    {
        public enum GhostState
        {
            Wander,
            Chasing,
            Scared,
            Eaten
        }
        public GhostState state;
        public Texture2D ghostTexture, ghostHitTexture;
        public GameConsole console;
        public Ghost(Game game) : base(game)
        {
            this.state = GhostState.Wander;
            console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
        }
        public Ghost(Game game, GhostState state) : base(game)
        {
            this.state = state;
            console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
        }
        protected override void LoadContent()
        {
            this.ghostTexture = this.SpriteTexture = this.Game.Content.Load<Texture2D>("PurpleGhost");
            this.ghostHitTexture = this.Game.Content.Load<Texture2D>("GhostHit");
            //TODO add ghost starting location
            this.Location = new Vector2(100, 100); //temp starting locations 
            this.Direction = new Vector2(1, 0);
            this.Speed = 20;
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            UpdateStateBehavior(gameTime);
            UpdateKeepGhostOnScreen();
            UpdateTexture();
            UpdateLogStateToConsole();
            base.Update(gameTime);
        }
        public void UpdateLogStateToConsole()
        {
            console.DebugTextOutput["GhostState"] = this.state.ToString();
            console.DebugTextOutput["GhostSpeed"] = this.Speed.ToString();
        }
        private void UpdateStateBehavior(GameTime gameTime)
        {
            switch (state)
            {
                case GhostState.Wander:
                    WanderToPoint(gameTime);
                    break;
                case GhostState.Chasing:
                    // Implement chasing behavior
                    break;
                case GhostState.Scared:
                    // Implement scared behavior
                    break;
                case GhostState.Eaten:
                    // Implement eaten behavior
                    break;
            }
        }

        private Vector2 wanderTarget;
        private bool hasWanderTarget;
        private readonly Random rng = new Random();

        private const float WanderSpeed = 100f;
        private const float MinTargetDistance = 100f;

        public void WanderToPoint(GameTime gameTime)
        {
            if (!hasWanderTarget)
            {
                wanderTarget = PickWanderTarget();
                hasWanderTarget = true;
            }

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float step = WanderSpeed * dt;

            Vector2 toTarget = wanderTarget - this.Location;
            float dist = toTarget.Length();

            if (dist <= step)
            {
                // Arrived (snap so we never overshoot or jitter), then pick a new spot
                this.Location = wanderTarget;
                hasWanderTarget = false;
            }
            else
            {
                this.Direction = toTarget / dist;
                this.Location += this.Direction * step;
            }
        }

        private Vector2 PickWanderTarget()
        {
            Viewport vp = this.Game.GraphicsDevice.Viewport;
            float maxX = vp.Width - this.ghostTexture.Width;
            float maxY = vp.Height - this.ghostTexture.Height;

            Vector2 target;
            do
            {
                target = new Vector2(
                    (float)rng.NextDouble() * maxX,
                    (float)rng.NextDouble() * maxY);
            }
            while (Vector2.Distance(target, this.Location) < MinTargetDistance);

            return target;
        }


        protected void GhostDead()
        {
            this.state = GhostState.Eaten;
            UpdateTexture();
        }
        protected void GhostScared()
        {
            this.state = GhostState.Scared;
            UpdateTexture();
        }
        protected void GhostChasing()
        {
            this.state = GhostState.Chasing;
            UpdateTexture();
        }
        protected void GhostWandering()
        {
            this.state = GhostState.Wander;
            UpdateTexture();
        }
        protected virtual void UpdateKeepGhostOnScreen()
        {
            if ((this.Location.Y + this.spriteTexture.Height / 2 > this.Game.GraphicsDevice.Viewport.Height)
                ||
                (this.Location.Y - this.spriteTexture.Height / 2 < 0)
                )
            {
                this.Direction.Y *= -1;
                this.state = GhostState.Wander;
            }
            if ((this.Location.X + this.spriteTexture.Width / 2 > this.Game.GraphicsDevice.Viewport.Width)
                ||
                (this.Location.X - this.spriteTexture.Width / 2 < 0)
                )
            {
                this.Direction.X *= -1;
                this.state = GhostState.Wander;
            }
        }

        protected virtual void UpdateTexture()
        {
            switch (this.state)
            {
                case GhostState.Wander:
                case GhostState.Chasing:
                    this.SpriteTexture = ghostTexture;
                    break;
                case GhostState.Eaten:
                case GhostState.Scared:
                    this.SpriteTexture = ghostHitTexture;
                    break;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            if (ghostTexture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(ghostTexture, Location, Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

    }
}
