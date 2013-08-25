﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spiridios.SnapEncounters
{
    public class Adventurer
    {
        public enum GenderType { Male, Female, NotSet };

        public enum WeaponType { Ranged, Mele, NotSet };

        public Adventurer()
        {
            Gender = GenderType.NotSet;
        }

        public GenderType Gender { get; set; }

        public WeaponType Weapon { get; set; }
    }
}
