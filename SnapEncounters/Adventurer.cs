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

        private Image attackImage;

        private int hitPoints = 10;
        private int xp = 0;

        private readonly Vector2 defaultPlayerActorPosition = new Vector2(240, 300);
        internal Vector2 DefaultPlayerActorPosition
        {
            get { return defaultPlayerActorPosition; }
        }

        public enum GenderType { Male, Female, NotSet };
        private GenderType gender = GenderType.NotSet;

        public enum WeaponType { Ranged, Mele, NotSet };
        private WeaponType weapon = WeaponType.NotSet;

        public Adventurer()
        {

            neutralPC = new SEActor("NeutralPC.xml");
            neutralPC.Position = defaultPlayerActorPosition;
            actor = neutralPC;
            femalePC = new SEActor("FemalePC.xml");
            femalePC.Position = defaultPlayerActorPosition;
            malePC = new SEActor("MalePC.xml");
            malePC.Position = defaultPlayerActorPosition;
            
        }

        public void TakeHit()
        {
            this.hitPoints--;
        }

        public int HitPoints
        {
            get { return this.hitPoints; }
            set { this.hitPoints = value; }
        }

        public void GainXP()
        {
            this.xp++;
        }

        public void GainXP(int amount)
        {
            this.xp += amount;
        }

        public int XP
        {
            get { return this.xp; }
            set { this.xp = value; }
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

        public Image AttackImage
        {
            get { return this.attackImage; }
        }

        public WeaponType Weapon
        {
            get { return this.weapon; }
            set
            {
                this.weapon = value;
                if (this.weapon == WeaponType.Ranged)
                {
                    this.attackImage = new TextureImage("Ranged");
                }
                else
                {
                    this.attackImage = new TextureImage("Mele");
                }

            }
        }

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
