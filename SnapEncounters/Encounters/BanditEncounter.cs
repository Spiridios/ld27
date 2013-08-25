using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class BanditEncounter : Encounter
    {
        private Encounter expiredEncounter;
        private Encounter successFleeEncounter;
        private Encounter successFightEncounter;
        private SEActor enemy = new SEActor("Bandit.xml");

        public BanditEncounter()
            : base(new TextureImage("Flee"), null)
        {
            this.Actor = enemy;

            String preamble =
                  "\nThis must be a bad part of town. Another"
                + "\nhoodlum jumps you, a bandit this time.";

            String expired =
                    "Unfortunately when fight or flight kicked"
                + "\nin, you froze! The bandit walked up, took"
                + "\nall your possessions, stared at you, then"
                + "\ndecided to put you out of your missery.";

            this.expiredEncounter = new Encounter(preamble).AddLine(expired);
            this.expiredEncounter.Actor = enemy;

            String successFight =
                    "The bandit sees your weapon, and runs!"
                + "\nHowever, as a seasoned adventurer you"
                + "\nknow better than to let a bandit get"
                + "\naway. They have tasty loot and all."
                + "\nYou successfully slay the bandit.";

            this.successFightEncounter = new Encounter(preamble).AddLine(successFight);
            this.successFightEncounter.Actor = enemy;

            String successFlee =
                    "That bandit looked really scary. We"
                + "\ndon't blame you for running. He did"
                + "\nmanage to nick you as you fled, but"
                + "\nit's nothing a brave adventurer like"
                + "\nyou can't handle.";
            this.successFleeEncounter = new Encounter(preamble)
            .AddLine(successFlee);
            this.successFleeEncounter.Actor = null;
        }

        public override void Activate()
        {
            base.Activate();
            this.rightImage = ((SnapEncounters)SpiridiGame.Instance).Adventurer.AttackImage;
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
                    ((SnapEncounters)game).Adventurer.TakeHit();
                    NextEncounter = successFleeEncounter;
                    break;
                case (Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.GainXP();
                    enemy.Kill();
                    NextEncounter = successFightEncounter;
                    break;
            }
        }

    }
}
