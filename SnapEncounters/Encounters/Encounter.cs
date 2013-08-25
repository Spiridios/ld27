using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Spiridios.SpiridiEngine;
using Microsoft.Xna.Framework;

namespace Spiridios.SnapEncounters.Encounters
{
    public class Encounter : Drawable, Updatable
    {
        public enum EncounterType { Exposition, Choice };
        private EncounterType encounterType;
        private Encounter nextEncounter;
        protected SnapEncounters game;

        private List<String> exposition = new List<String>();

        private Image leftImage;
        private Image rightImage;

        private const int CHOICE_Y = 300;
        private const int CHOICE_LEFT_X = 220;
        private const int CHOICE_RIGHT_X = 420;

        private double currentChoiceElapsedTime = 0;
        private const double MAX_CHOICE_TIME = 1;

        public enum Choice { NoChoice, LeftChoice, RightChoice, Expired, Continue};
        protected Choice choice;

        public Encounter(EncounterType encounterType)
        {
            this.game = (SnapEncounters)SpiridiGame.Instance;
            this.encounterType = encounterType;
        }

        public Encounter(Image leftChoiceImage, Image rightChoiceImage)
            : this(EncounterType.Choice)
        {
            leftImage = leftChoiceImage;
            rightImage = rightChoiceImage;
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

        public void DrawChoices(SpriteBatch spriteBatch)
        {
            if (this.encounterType == EncounterType.Choice)
            {
                this.leftImage.Draw(spriteBatch, new Vector2(CHOICE_LEFT_X - (leftImage.Width / 2), CHOICE_Y));
                this.rightImage.Draw(spriteBatch, new Vector2(CHOICE_RIGHT_X - (rightImage.Width / 2), CHOICE_Y));
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawExposition(spriteBatch);
            DrawChoices(spriteBatch);
        }

        public virtual void Update(System.TimeSpan elapsedTime)
        {
            if (this.encounterType == EncounterType.Exposition)
            {
                if (game.InputManager.IsAnyKeyTriggered)
                {
                    this.choice = Choice.Continue;
                }
            }
            else
            {
                this.currentChoiceElapsedTime += elapsedTime.TotalSeconds;
                if (this.choice == Choice.NoChoice)
                {
                    if (game.InputManager.IsTriggered("left-choice"))
                    {
                        this.choice = Choice.LeftChoice;
                    }
                    else if (game.InputManager.IsTriggered("right-choice"))
                    {
                        this.choice = Choice.RightChoice;
                    }
                    else if(this.currentChoiceElapsedTime > MAX_CHOICE_TIME)
                    {
                        this.choice = Choice.Expired;
                    }
                }
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
