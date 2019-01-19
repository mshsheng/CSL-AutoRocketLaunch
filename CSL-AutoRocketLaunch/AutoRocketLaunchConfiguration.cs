using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL_AutoRocketLaunch
{
    [ConfigurationPath("AutoRocketLaunch.xml")]
    public class AutoRocketLaunchConfiguration
    {
        public ushort targetTouristNum { get; set; } = 360;
    }
}
