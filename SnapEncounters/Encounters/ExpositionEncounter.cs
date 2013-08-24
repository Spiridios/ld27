using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spiridios.SnapEncounters.Encounters
{
    public class ExpositionEncounter : Encounter
    {
        List<String> exposition = new List<String>();

        public ExpositionEncounter()
            : base()
        {
        }

        public ExpositionEncounter(String expositionLine)
            : base()
        {
            this.AddLine(expositionLine);
        }

        public ExpositionEncounter AddLine(String expositionLine)
        {
            this.exposition.Add(expositionLine);
            return this;
        }

    }
}
