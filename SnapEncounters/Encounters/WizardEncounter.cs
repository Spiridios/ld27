using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class WizardEncounter : Encounter
    {
        private Encounter expiredEncounter;
        private Encounter successFightEncounter;
        private Encounter successFleeEncounter;
        private SEActor enemy = new SEActor("Zombie.xml");

        public WizardEncounter()
            : base(null, new TextureImage("Flee"))
        {
        }

        public override void Activate()
        {
            base.Activate();
            this.Actor = enemy;
            this.leftImage = ((SnapEncounters)SpiridiGame.Instance).Adventurer.AttackImage;

            this.expiredEncounter = new Encounter(
                  "\nYou stand in awe of the wizards power"
                + "\nand do nothing. The wizard fires off"
                + "\nthe simplest of spells and fells you"
                + "\nwithout effort. So sad that a seasoned"
                + "\nadventurer died so close to the end"
                + "\nof their adventure."
                );
            this.expiredEncounter.Actor = enemy;

            if (((SnapEncounters)game).Adventurer.Weapon == Adventurer.WeaponType.Mele)
            {

                this.successFightEncounter = new Encounter(
                      "\nYou calmly walk up to the wizard"
                    + "\nwith your sword held in surrender."
                    + "\nThe wizard allows you to approach"
                    + "\nbut when he reaches for your sword"
                    + "\nyou grab his arm and stab him with"
                    + "\na small dagger you had concealed."
                    + "\nYou have defeated the powerful wizard!"
                    );
                this.successFightEncounter.Actor = enemy;
            }
            else
            {
                this.successFightEncounter = new Encounter(
                      "\nYou draw your bow and aim while"
                    + "\nyou see the wizard incanting something"
                    + "\nyou let fly, but the arrow turns into"
                    + "\na harmless fly. The wizard laughs as"
                    + "\nyou pull out another arrow and fire."
                    + "\nthis arrow was turned into a bat."
                    + "\nFortunately the wizard didn't see the"
                    + "\nsecond arrow you let fly. The powerful"
                    + "\nwizard was struck down!"
                    );
                this.successFightEncounter.Actor = enemy;
            }

            this.successFleeEncounter = new Encounter(
                  "\nYou try to run, but the wizard casts"
                + "\na flesh-to-stone spell on you. It's"
                + "\nactually quite fitting, the only"
                + "\nstatue of a famous adventurer to"
                + "\nhave so many lifelike details."
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
                    InsertEncounter(successFightEncounter);
                    enemy.Kill();
                    break;
                case (Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.GainXP(-1);
                    NextEncounter = successFleeEncounter;
                    ((SnapEncounters)game).Adventurer.Actor.Kill();
                    break;
            }
        }

    }
}
