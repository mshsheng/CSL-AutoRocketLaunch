namespace CSL_AutoRocketLaunch.Config
{
    public class ConfigMethods
    {
        private static Configs config;

        public static bool enabled;
        public static int timeInterval;
        public static bool autoFocus;
        public static int modMode;
        public static int targetVisitorNum;

        public static void Load()
        {
            if (!LoadingExtension.InGame)
            {
                return;
            }

            config = Configuration<Configs>.Load();

            enabled = config.enabled;
            timeInterval = config.timeInterval;
            autoFocus = config.autoFocus;
            modMode = config.modMode;
            targetVisitorNum = config.targetVisitorNum;
        }

        public static void Save()
        {
            Configuration<Configs>.Save();
        }
    }
}
