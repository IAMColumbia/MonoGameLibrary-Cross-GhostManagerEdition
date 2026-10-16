using Microsoft.Xna.Framework;
using System;

namespace MonoGameLibraryCross
{
    /*  GhostManager should be able to handle an arbitrary number of ghosts
        GhostManager should move and draw the ghosts on each update and draw
        GhostManager should check for Ghost on Ghost collision and change the ghosts direction if the collide
        GhostManager should create 4 ghosts in random locations and make sure that the ghosts don't spawn on top of each other
        GhostManager should be able to add and remove ghosts at any time,
    */
    public class GhostManager : GameComponent
    {
        public int numGhosts;
        public Ghost[] ghosts;
        public GhostManager(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            numGhosts = 4;
            ghosts = new Ghost[numGhosts];
            for (int i = 0; i < numGhosts; i++)
            {
                ghosts[i] = new Ghost(this.Game);
                this.Game.Components.Add(ghosts[i]);
            }
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < numGhosts; i++)
            {
                ghosts[i].Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void AddGhost(Ghost ghost)
        {
            numGhosts++;
            Array.Resize(ref ghosts, numGhosts);
            ghosts[numGhosts - 1] = ghost;
            this.Game.Components.Add(ghost);
        }

        public void RemoveGhost(Ghost ghost)
        {
            int index = Array.IndexOf(ghosts, ghost);
            if (index >= 0)
            {
                this.Game.Components.Remove(ghost);
                for (int i = index; i < numGhosts - 1; i++)
                {
                    ghosts[i] = ghosts[i + 1];
                }
                numGhosts--;
                Array.Resize(ref ghosts, numGhosts);
            }
        }
    }
}
