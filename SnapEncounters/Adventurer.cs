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
        private SEActor actor;
        private SEActor neutralPC;
        private SEActor femalePC;
        private SEActor malePC;

        private readonly Vector2 defaultPlayerActorPosition = new Vector2(240, 300);
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

            neutralPC = new SEActor("NeutralPC.xml");
            neutralPC.Position = defaultPlayerActorPosition;
            actor = neutralPC;
            femalePC = new SEActor("FemalePC.xml");
            femalePC.Position = defaultPlayerActorPosition;
            malePC = new SEActor("MalePC.xml");
            malePC.Position = defaultPlayerActorPosition;
            
        }

        public SEActor Actor
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
