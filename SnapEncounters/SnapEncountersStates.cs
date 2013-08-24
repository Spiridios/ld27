using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Spiridios.SpiridiEngine;
using Spiridios.SpiridiEngine.Audio;
using Spiridios.SpiridiEngine.Input;
using Spiridios.SpiridiEngine.Scene;
using Spiridios.SnapEncounters.Encounters;
using System;
using System.Collections.Generic;

namespace Spiridios.SnapEncounters
{
    public class TitleState : State
    {
        private Background background;

        public TitleState(SpiridiGame game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            // Load the continents 
            this.game.ImageManager.AddImage("Background", "Background.png");

            this.background = new StaticBackground("Background");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            game.NextState = new PlayGameState(game, this, background);
            game.NextState.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.background.Draw(game.SpriteBatch);
            int lineHeight = 40;
            int numLines = 6;
            int firstLine = (game.WindowHeight - (numLines * lineHeight)) / 2;
            game.DrawText("Snap Encounters", TextRenderer.CENTERED, firstLine);
            game.DrawText("A game of 10 one-second encounters", TextRenderer.CENTERED, firstLine + (1 * lineHeight));
            game.DrawText("By Spiridios for Ludum Dare 27", TextRenderer.CENTERED, firstLine + (3 * lineHeight));
            game.DrawText("Press any key to start", TextRenderer.CENTERED, firstLine + (5 * lineHeight));
            game.DrawFPS();

        }

        public override void KeyUp(KeyboardEvent keyState)
        {
            base.KeyUp(keyState);
            game.NextState = new PlayGameState(game, this, background);
            game.NextState.Initialize();
        }

    }

    public class PlayGameState : State
    {
        private Background background;
        private Encounter encounters;
        private State titleState;

        public PlayGameState(SpiridiGame game, State titleState, Background background)
            : base(game)
        {
            this.background = background;
            this.titleState = titleState;
        }

        public override void Initialize()
        {
            base.Initialize();
            encounters = new ExpositionEncounter("Welcome to Snap Encounters")
                .AddLine("")
                .AddLine("You are an adventurer and must make")
                .AddLine("snap decisions in order to progress.")
                .AddLine("")
                .AddLine("Failure to make a choice in time")
                .AddLine("will lead to your grisly demise.")
                .AddLine("")
                .AddLine("(continue)");

            encounters.AddEncounter(new ExpositionEncounter("Test")
                
                );
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (encounters.IsDone)
            {
                encounters = encounters.NextEncounter;
                if (encounters == null)
                {
                    //game.NextState = titleState;
                    Initialize();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.background.Draw(game.SpriteBatch);
            encounters.Draw(game.SpriteBatch);
            game.DrawFPS();
        }

        public override void KeyUp(KeyboardEvent keyState)
        {
            base.KeyUp(keyState);
            encounters.KeyUp();
        }

    }
}