namespace CSL_AutoRocketLaunch
{
    [ConfigurationPath("AutoRocketLaunch.xml")]
    public class AutoRocketLaunchConfiguration
    {
        public bool enabled { get; set; } = true;
        public int mode { get; set; } = 0;
        public bool autoFocus { get; set; } = false;
        public int timeInterval { get; set; } = 20;
        public int targetVisitorNum { get; set; } = 350;
    }
}
