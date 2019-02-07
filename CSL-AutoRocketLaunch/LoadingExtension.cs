using ICities;
using CSL_AutoRocketLaunch.Config;

namespace CSL_AutoRocketLaunch
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public static bool InGame = false;

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            InGame = true;
            ConfigMethods.Load();
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            InGame = false;
        }
    }
}
