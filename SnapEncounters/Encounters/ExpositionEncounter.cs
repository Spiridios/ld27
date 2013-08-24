using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class ExpositionEncounter : Encounter
    {
        private List<String> exposition = new List<String>();

        public ExpositionEncounter()
            : base()
        {
        }

        public ExpositionEncounter(String expositionLine)
            : base()
        {
            this.AddLine(expositionLine);
        }

        public ExpositionEncounter AddLine(String expositionLine)
        {
            this.exposition.Add(expositionLine);
            return this;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            int y = game.FirstTextLine;
            TextRenderer textRenderer = game.MessageTextRenderer;

            foreach(String line in exposition)
            {
                textRenderer.DrawText(spriteBatch, line, TextRenderer.CENTERED, y);
                y += textRenderer.LineHeight;
            }
        }

        public override void KeyUp()
        {
            this.done = true;
        }

    }
}
