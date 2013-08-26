using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class SkeletonEncounter : Encounter
    {
        private Encounter expiredEncounter;
        private Encounter successFightEncounter;
        private Encounter successLoveEncounter;
        private SEActor enemy = new SEActor("Skeleton.xml");

        public SkeletonEncounter()
            : base(null, new TextureImage("Heart"))
        {
        }

        public override void Activate()
        {
            base.Activate();
            this.Actor = enemy;
            this.leftImage = ((SnapEncounters)SpiridiGame.Instance).Adventurer.AttackImage;

            this.expiredEncounter = new Encounter(
                  "\nThe skeleton shambles toward you. You"
                + "\ntry to run, but skeleton arms rise"
                + "\nout of the ground and hold you fast."
                + "\nSurprisingly, the pain from the bites"
                + "\nas the skelton \"devours\" you isn't"
                + "\nas painful as you thought."
                );
            this.expiredEncounter.Actor = enemy;

            if (((SnapEncounters)game).Adventurer.Weapon == Adventurer.WeaponType.Mele)
            {
                this.successFightEncounter = new Encounter(
                      "\nYou take a swing at the skeleton"
                    + "\nwith your sword, striking true."
                    + "\nBones fall off from places you"
                    + "\ndidn't even realize had bones."
                    + "\nYou continue the barage until"
                    + "\nthere's nothing but bonemeal left."
                    + "\nThe plants here are bound to thrive."
                    );
                this.successFightEncounter.Actor = enemy;
            }
            else
            {
                this.successFightEncounter = new Encounter(
                      "\nYou shoot arrow after arrow at the"
                    + "\nskeleton. Many whiz right between"
                    + "\nbones, but with each successful hit"
                    + "\nthe skeleton has fewer and fewer"
                    + "\nbones left, until there's only a"
                    + "\ntoe bone left. It flees in retreat."
                    );
                this.successFightEncounter.Actor = enemy;
            }

            this.successLoveEncounter = new Encounter(
                  "\nYou embrace the skeleton in the"
                + "\nheartiest of bear hugs. If the"
                + "\nskeleton had a face, it would"
                + "\nclearly show confusion. You hear"
                + "\na ghostly wheezy voice say"
                + "\n\"thank you\" as the skeleton"
                + "\ndrops, bone by bone, to the ground."
                );
            this.successLoveEncounter.Actor = enemy;

        }

        public override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            switch (this.choice)
            {
                case (Choice.Expired):
                    NextEncounter = expiredEncounter;
                    ((SnapEncounters)game).Adventurer.Actor.Kill();
                    break;
                case (Choice.LeftChoice):
                    ((SnapEncounters)game).Adventurer.GainXP();
                    InsertEncounter(successFightEncounter);
                    enemy.Kill();
                    break;
                case (Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.GainXP(3);
                    InsertEncounter(successLoveEncounter);
                    enemy.Kill();
                    break;
            }
        }

    }
}
