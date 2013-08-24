using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Spiridios.SpiridiEngine;


namespace Spiridios.SnapEncounters
{
    public class SnapEncounters : SpiridiGame
    {
        private TextRenderer messageTextRenderer;

        public SnapEncounters()
            : base()
        {
            Content.RootDirectory = "Content";
            this.SetWindowSize(640, 480);
            this.ClearColor = new Color(0, 0, 0);
#if(!JSIL)
            this.IsQuickExit = true;
#endif

#if(DEBUG)
            this.LockFramerate = false;
#else
            this.LockFramerate = true;
#endif
            this.NextState = new BootState(this, new TitleState(this));
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.InputManager.RegisterActionBinding("left-choice", Keys.Left);
            this.InputManager.RegisterActionBinding("right-choice", Keys.Right);
            this.InputManager.RegisterActionBinding("doStuff", Keys.Space);

#if(DEBUG)
            this.ShowFPS = true;
#else
            this.ShowFPS = false;
#endif
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.DefaultTextRenderer = new TextRenderer(this, "TitleScreenFont", new Color(0x39, 0x2d, 0x59));
            this.messageTextRenderer = new TextRenderer(this, "MessageFont", new Color(0x39, 0x2d, 0x59));

        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
