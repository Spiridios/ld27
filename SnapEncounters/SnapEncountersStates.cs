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
    public class PlayGameState : State
    {
        private Background background;
        private Encounter encounters;

        public PlayGameState(SpiridiGame game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            if (this.background == null)
            {
                this.game.ImageManager.AddImage("Background", "Background.png");
                this.game.ImageManager.AddImage("Male", "Male.png");
                this.game.ImageManager.AddImage("Female", "Female.png");
                this.game.ImageManager.AddImage("Mele", "Mele.png");
                this.game.ImageManager.AddImage("Ranged", "Ranged.png");

                this.background = new StaticBackground("Background");
            }

            ((SnapEncounters)game).Adventurer = new Adventurer();

            encounters = new Encounter("Welcome, Adventurer, to Ludum Dare 27!")
                .AddLine("")
                .AddLine("You have discovered Snap Encounters!")
                .AddLine("As a seasoned adventurer, this is obviously")
                .AddLine("not the first amazing discovery you have")
                .AddLine("made. Hopefully it shall not be your last.")
                ;

            encounters.AddEncounter(new Encounter("")
                .AddLine("As an adventurer, you are quite familiar")
                .AddLine("with snap decisions. When a goul pops out")
                .AddLine("of a treasure chest, you must quickly")
                .AddLine("decide whether to flee, fight, or ask for")
                .AddLine("your three wishes to be granted.")
                );

            encounters.AddEncounter(new Encounter("")
                .AddLine("Hey, it was dark. It could have been a genie.")
                .AddLine("Anyhow, the point is your choice can easily")
                .AddLine("be the difference between life and death.")
                .AddLine("(or great fortune!)")
                );

            encounters.AddEncounter(new Encounter("")
                .AddLine("In Snap Encounters, your choices must be")
                .AddLine("made quickly. Exactly how quickly?")
                .AddLine("1 second.")
                .AddLine("What can we say, we bore easily.")
                .AddLine("Failure to make any choice within 1 second")
                .AddLine("will lead to....")
                );

            encounters.AddEncounter(new Encounter("")
                .AddLine("Grisly results.")
                .AddLine("")
                .AddLine("(We also believe in strict rules)")
                .AddLine("But that shouldn't matter to a seasoned")
                .AddLine("adventurer like you, right?")
                );

            encounters.AddEncounter(new Encounter("")
                .AddLine("In this adventure, you will need to make")
                .AddLine("10 decisions. Your decisions will determine")
                .AddLine("how you progress. Why 10? Because, uh, you")
                .AddLine("have ten fingers, that's why.")
                );

            encounters.AddEncounter(new Encounter("")
                .AddLine("You do have ten fingers right?")
                .AddLine("(you never know with adventurers)")
                .AddLine("")
                .AddLine("Anyhow, you will see your choices flash")
                .AddLine("on screen. Use the left or right arrow")
                .AddLine("keys to make your choice.")
                );


            encounters.AddEncounter(new Encounter("")
                .AddLine("Ready to see if you measure up while under")
                .AddLine("pressure, like the great Freddie Mercury?")
                .AddLine("")
                .AddLine("You better be, we sense your first decision")
                .AddLine("coming up...")
                );

            encounters.AddEncounter(new GenderEncounter());
            encounters.AddEncounter(new WeaponEncounter());

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            encounters.Update(gameTime.ElapsedGameTime);
            if (encounters.IsDone)
            {
                encounters = encounters.NextEncounter;
                if (encounters == null)
                {
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

        public void DrawTitleText(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Vestigial code - TitleState is only really needed as an asset loader.
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
            encounters.KeyUp();
        }

    }
}