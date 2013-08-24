using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spiridios.SnapEncounters.Encounters
{
    public class Encounter
    {
        private Encounter nextEncounter;

        public Encounter NextEncounter
        {
            get { return nextEncounter; }
            set { this.nextEncounter = value; }
        }
    }
}
