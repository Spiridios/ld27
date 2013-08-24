using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Spiridios.SpiridiEngine;
using Spiridios.SpiridiEngine.Audio;
using Spiridios.SpiridiEngine.Input;
using Spiridios.SpiridiEngine.Scene;
using System;
using System.Collections.Generic;

namespace Spiridios.SnapEncounters
{
    public class TitleState : State
    {
        public TitleState(SpiridiGame game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

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
            game.NextState = new PlayGameState(game);
            game.NextState.Initialize();
        }

    }

    public class PlayGameState : State
    {
        public PlayGameState(SpiridiGame game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}