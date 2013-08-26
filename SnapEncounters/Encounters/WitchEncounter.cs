using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class WitchEncounter : Encounter
    {
        private Encounter expiredEncounter;
        private Encounter successLoveEncounter;
        private Encounter successFleeEncounter;
        private SEActor enemy = new SEActor("Witch.xml");

        public WitchEncounter()
            : base(new TextureImage("Heart"), new TextureImage("Flee"))
        {
        }

        public override void Activate()
        {
            base.Activate();
            this.Actor = enemy;

            this.expiredEncounter = new Encounter(
                  "\nThe witch says \"hello\" but as a"
                + "\nseasoned adventurer you know better."
                + "\nYou attack her to try and keep her"
                + "\nfrom getting a spell off. She falls"
                + "\nover dead, but still manages to say"
                + "\nsomething while casting a spell."
                + "\nAll she wanted was a friend."
                );
            this.expiredEncounter.Actor = enemy;

            if (((SnapEncounters)game).Adventurer.Gender == Adventurer.GenderType.Female)
            {
                this.successLoveEncounter = new Encounter(
                      "\nYou compliment the witch on how"
                    + "\nher robe fits so well. She responds"
                    + "\nby explaining how she made it herself."
                    + "\nSoon the conversation turns to"
                    + "\nadventure and you find you have a new"
                    + "\nally when you need to rest from"
                    + "\nfrom your day job."
                    );
            }
            else
            {
                this.successLoveEncounter = new Encounter(
                      "\nYou tell the witch that her eyes"
                    + "\nsparkle like the stars on a clear"
                    + "\nnight. She responds by laughing the"
                    + "\nmost stereotypical witch cackle you"
                    + "\nhave ever heard. While your advances"
                    + "\ndidn't lead to true love, you did"
                    + "\ngain a friend."
                    );
            }

            this.successFleeEncounter = new Encounter(
                  "\nThe witch yells something unintelligible"
                + "\nthen starts crying. The water from her"
                + "\ntears causes her to melt."
                );
            this.successFleeEncounter.Actor = enemy;
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
                    ((SnapEncounters)game).Adventurer.GainXP(3);
                    InsertEncounter(successLoveEncounter);
                    break;
                case (Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.GainXP(-1);
                    enemy.Kill();
                    InsertEncounter(successFleeEncounter);
                    break;
            }
        }

    }
}
