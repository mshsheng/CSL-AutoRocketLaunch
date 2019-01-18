using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;
using UnityEngine;

namespace CSL_AutoRocketLaunch
{
    public class AutoRocketLaunchMod : IUserMod
    {
        public string Name
        {
            get { return "Auto Rocket Launch"; }
        }

        public string Description
        {
            get { return "Launching rockets automatically"; }
        }
    }
}
