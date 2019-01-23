﻿namespace CSL_AutoRocketLaunch
{
    [ConfigurationPath("AutoRocketLaunch.xml")]
    public class AutoRocketLaunchConfiguration
    {
        public bool enabled { get; set; } = true;
        public int mode { get; set; } = 0;
        public ushort targetVisitorNum { get; set; } = 350;
    }
}
