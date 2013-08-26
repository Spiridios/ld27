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
        private int firstLine;

        internal Adventurer Adventurer { get; set; }

        public SnapEncounters()
            : base()
        {
            Content.RootDirectory = "Content";
            this.SetWindowSize(640, 480);
            this.ClearColor = new Color(0, 0, 0);
            this.DrawBoundingShapes = false;

#if(JSIL)
            this.IsQuickExit = false;
#else
            this.IsQuickExit = true;
#endif

#if(DEBUG)
            this.LockFramerate = false;
#else
            this.LockFramerate = true;
#endif
            this.NextState = new BootState(this, new PlayGameState(this));
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
            this.DefaultTextRenderer = new TextRenderer(this, "TitleScreenFont", Color.White );

            this.messageTextRenderer = new TextRenderer(this, "MessageFont", Color.White);
            this.messageTextRenderer.DropShadow = true;
            this.messageTextRenderer.DropShadowColor = new Color(0.0f,0.0f,0.0f,0.75f);
            this.messageTextRenderer.DropShadowOffset = new Vector2(1,1);
            this.firstLine = this.messageTextRenderer.LineHeight * 3;
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        internal TextRenderer MessageTextRenderer
        {
            get { return this.messageTextRenderer; }
        }

        internal int FirstTextLine
        {
            get { return this.firstLine; }
        }
    }
}
