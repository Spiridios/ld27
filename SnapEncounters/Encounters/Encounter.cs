using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class Encounter : Drawable
    {
        private Encounter nextEncounter;
        protected SnapEncounters game;

        protected bool done = false;

        public Encounter()
        {
            this.game = (SnapEncounters)SpiridiGame.Instance;
        }

        public Encounter NextEncounter
        {
            get { return nextEncounter; }
            set { this.nextEncounter = value; }
        }

        public void AddEncounter(Encounter encounter)
        {
            if (nextEncounter == null)
            {
                nextEncounter = encounter;
            }
            else
            {
                nextEncounter.AddEncounter(encounter);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public virtual void KeyUp()
        {
        }

        public bool IsDone
        {
            get { return done; }
        }

    }
}
