using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class SuccubusEncounter : Encounter
    {
        private Encounter expiredEncounter;
        private Encounter successLoveFightEncounter;
        private Encounter successFleeEncounter;
        private SEActor enemy = new SEActor("Succubus.xml");

        public SuccubusEncounter()
            : base(EncounterType.Choice)
        {
        }

        public override void Activate()
        {
            base.Activate();
            this.Actor = enemy;
            this.rightImage = new TextureImage("Flee");

            if (((SnapEncounters)game).Adventurer.Gender == Adventurer.GenderType.Male)
            {
                this.expiredEncounter = new Encounter(
                      "\nThe succubus mesmerized you. Since"
                    + "\nyou didn't fight her or submit, she"
                    + "\nlost interest in you, but not before"
                    + "\nripping your heart out. It was the"
                    + "\nmost exotic sensation you've ever"
                    + "\nfelt before hitting the ground."
                    );
                this.expiredEncounter.Actor = enemy;

                this.leftImage = new TextureImage("Heart");

                this.successLoveFightEncounter = new Encounter(
                      "\nYou run to the succubus and embrace"
                    + "\nher. She returns the hug, then beckons"
                    + "\nyou to follow. You are never seen"
                    + "\nagain, but it can be presumed you"
                    + "\ndied happy."
                    );
                this.successLoveFightEncounter.Actor = null;

                this.successFleeEncounter = new Encounter(
                      "\nThe succubus calls after you. She"
                    + "\npromises you the world and more"
                    + "\npleasure than you can imagine. But"
                    + "\nbeing a seasoned adventurer you"
                    + "\nknow better. That, and she was blond."
                    );
                this.successFleeEncounter.Actor = null;
            }
            else
            {
                this.expiredEncounter = new Encounter(
                      "\nThe succubus is disgusted by your"
                    + "\nlack of action. Her eyes glow deep"
                    + "\nred as she tears the flesh from"
                    + "\nyour bones."
                    );
                this.expiredEncounter.Actor = enemy;

                this.leftImage = ((SnapEncounters)SpiridiGame.Instance).Adventurer.AttackImage;

                this.successLoveFightEncounter = new Encounter(
                      "\nYou attack the succubus. She really"
                    + "\nwasn't expecting that. She usually"
                    + "\nuses her innate feminine charms to"
                    + "\nget out of situations like this. She"
                    + "\nflees, but not before gaining a few"
                    + "\nless than attractive scars."
                    );
                this.successLoveFightEncounter.Actor = null;

                this.successFleeEncounter = new Encounter(
                      "\nThe succubus yells after you. She"
                    + "\ncalls you names we'd rather not"
                    + "\nrepeat here. But being a seasoned"
                    + "\adventurer you let it slide."
                    + "\nBesides, you could totally see"
                    + "\nher roots showing."
                    );
                this.successFleeEncounter.Actor = null;
            }
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
                    InsertEncounter(successLoveFightEncounter);
                    if (((SnapEncounters)game).Adventurer.Gender == Adventurer.GenderType.Female)
                    {
                        ((SnapEncounters)game).Adventurer.GainXP(3);
                        enemy.Kill();
                    }
                    else
                    {
                        successLoveFightEncounter.NextEncounter = null;
                        ((SnapEncounters)game).Adventurer.Actor.lifeStage = Spiridios.SpiridiEngine.Actor.LifeStage.DEAD;
                        ((SnapEncounters)game).Adventurer.Actor.DrawDead = false;
                    }
                    break;
                case (Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.GainXP(1);
                    InsertEncounter(successFleeEncounter);
                    break;
            }
        }

    }
}
