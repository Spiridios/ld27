using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Spiridios.SpiridiEngine;
using Microsoft.Xna.Framework.Graphics;

namespace Spiridios.SnapEncounters
{
    public class Adventurer : Drawable, Updatable
    {
        private Actor actor;
        private Actor neutralPC;
        private Actor femalePC;
        private Actor malePC;

        private readonly Vector2 defaultPlayerActorPosition = new Vector2(320, 300);
        internal Vector2 DefaultPlayerActorPosition
        {
            get { return defaultPlayerActorPosition; }
        }

        public enum GenderType { Male, Female, NotSet };
        private GenderType gender;

        public enum WeaponType { Ranged, Mele, NotSet };

        public Adventurer()
        {
            Gender = GenderType.NotSet;

            neutralPC = new Actor(new AnimatedImage("NeutralPC.xml"));
            neutralPC.Position = defaultPlayerActorPosition;
            actor = neutralPC;
            femalePC = new Actor(new AnimatedImage("FemalePC.xml"));
            femalePC.Position = defaultPlayerActorPosition;
            malePC = new Actor(new AnimatedImage("MalePC.xml"));
            malePC.Position = defaultPlayerActorPosition;
            
        }

        public Actor Actor
        {
            get { return actor; }
        }

        public GenderType Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                switch (value)
                {
                    case(GenderType.Female):
                        actor = femalePC;
                        break;
                    case (GenderType.Male):
                        actor = malePC;
                        break;
                    default:
                        actor = neutralPC;
                        break;
                }
            }
        }

        public WeaponType Weapon { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            actor.Draw(spriteBatch);
        }

        public void Update(System.TimeSpan elapsedTime)
        {
            actor.Update(elapsedTime);
        }
    }
}
