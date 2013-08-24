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
        }

        protected override void Initialize()
        {
            base.Initialize();
#if(DEBUG)
            this.ShowFPS = true;
#else
            this.ShowFPS = false;
#endif
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
