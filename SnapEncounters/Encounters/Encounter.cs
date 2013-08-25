using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class Encounter : Drawable, Updatable
    {
        public enum EncounterType { Exposition, Choice };
        private EncounterType encounterType;
        private Encounter nextEncounter;
        protected SnapEncounters game;
        private List<String> exposition = new List<String>();

        public enum Choice { NoChoice, LeftChoice, RightChoice, Expired, Continue};
        private Choice choice;

        public Encounter(EncounterType encounterType)
        {
            this.game = (SnapEncounters)SpiridiGame.Instance;
            this.encounterType = encounterType;
        }

        public Encounter(String expositionLine)
            : this(EncounterType.Exposition)
        {
            this.exposition.Add(expositionLine);
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

        public Encounter AddLine(String expositionLine)
        {
            this.exposition.Add(expositionLine);
            return this;
        }

        public void DrawExposition(SpriteBatch spriteBatch)
        {
            int y = game.FirstTextLine;
            TextRenderer textRenderer = game.MessageTextRenderer;

            foreach (String line in exposition)
            {
                textRenderer.DrawText(spriteBatch, line, TextRenderer.CENTERED, y);
                y += textRenderer.LineHeight;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawExposition(spriteBatch);
        }

        public virtual void Update(System.TimeSpan elapsedTime)
        {
            if (this.encounterType == EncounterType.Exposition && game.InputManager.IsAnyKeyTriggered)
            {
                this.choice = Choice.Continue;
            }
        }

        public virtual void KeyUp()
        {
        }

        public bool IsDone
        {
            get { return choice != Choice.NoChoice; }
        }

    }
}
