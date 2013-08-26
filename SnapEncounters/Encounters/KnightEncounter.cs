using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class KnightEncounter : Encounter
    {
        private Encounter expiredEncounter;
        private Encounter successLoveFleeEncounter;
        private Encounter successFightEncounter;
        private SEActor enemy = new SEActor("Knight.xml");

        public KnightEncounter()
            : base(EncounterType.Choice)
        {
        }

        public override void Activate()
        {
            base.Activate();
            this.Actor = enemy;

            this.expiredEncounter = new Encounter(
                  "\nThe knight attempts to restrain"
                + "\nyou. But as a seasoned adventurer"
                + "\nyou wriggle free and attempt to"
                + "\nattack. But your blows just bounce"
                + "\noff his armor. Angry, the knight"
                + "\nslays you with his helmet."
                );
            this.expiredEncounter.Actor = enemy;

            if (((SnapEncounters)game).Adventurer.Gender == Adventurer.GenderType.Female)
            {
                this.leftImage = new TextureImage("Heart");
                this.successLoveFleeEncounter = new Encounter(
                      "\nBat your eyes and compliment the"
                    + "\nknight at how shiny he really is."
                    + "\nThe knight doesn't take kindly to this"
                    + "\nmentioning his vow of celebacy. He then"
                    + "\ndrags you off to the dungeon. Hey,"
                    + "\nmaybe that was your goal all along."
                    );
                this.successLoveFleeEncounter.Actor = enemy;
            }
            else
            {
                this.leftImage = new TextureImage("Flee");
                this.successLoveFleeEncounter = new Encounter(
                      "\nYou take one look at the knight"
                    + "\nclad head to toe in plate armor."
                    + "\nYou decide that discretion is the"
                    + "\nbetter part of valor and turn tail."
                    + "\nYou hear the knight laughing at"
                    + "\nyour retreat. But he's not a"
                    + "\nseasoned adventurer like you, he"
                    + "\ndoesn't understand."
                    );
                this.successLoveFleeEncounter.Actor = enemy;
            }

            this.rightImage = ((SnapEncounters)game).Adventurer.AttackImage;
            if (((SnapEncounters)game).Adventurer.Weapon == Adventurer.WeaponType.Mele)
            {
                this.successFightEncounter = new Encounter(
                      "\nYour first few stabs and swipes do"
                    + "\nnothing but glance off the knight's"
                    + "\narmor. The knight mistakes this"
                    + "\nfumbling as a sign you are innept."
                    + "\nHe doesn't realize you are an"
                    + "\nexperienced adventurer and lets his"
                    + "\nguard down. You easily slip your sword"
                    + "\nbetween his armor and defeat him."
                    );
                this.successFightEncounter.Actor = enemy;
            }
            else
            {
                this.successFightEncounter = new Encounter(
                      "\nYou draw your bow and and aim steadily"
                    + "\nThe knight stands, eying you, trusting "
                    + "\nin his plate armor. You let fly and the"
                    + "\narrow pierces the armor effortlessly."
                    + "\nArmor really is better against stabby"
                    + "\nand slicy things."
                    );
                this.successFightEncounter.Actor = enemy;
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
                    InsertEncounter(successLoveFleeEncounter);
                    if (((SnapEncounters)game).Adventurer.Gender == Adventurer.GenderType.Female)
                    {
                        successLoveFleeEncounter.NextEncounter = null;
                        ((SnapEncounters)game).Adventurer.Actor.lifeStage = Spiridios.SpiridiEngine.Actor.LifeStage.DEAD;
                        ((SnapEncounters)game).Adventurer.Actor.DrawDead = false;
                    }
                    break;
                case (Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.GainXP(3);
                    enemy.Kill();
                    InsertEncounter(successFightEncounter);
                    break;
            }
        }

    }
}
