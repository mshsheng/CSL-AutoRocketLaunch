using ICities;
using CSL_AutoRocketLaunch.Config;
using CSL_AutoRocketLaunch.Launch;

namespace CSL_AutoRocketLaunch
{
    public class ThreadingExtension : ThreadingExtensionBase
    {
        public override void OnAfterSimulationTick()
        {
            base.OnAfterSimulationTick();

            if (threadingManager.simulationTick % 60 != 0 || threadingManager.simulationPaused)
            {
                return;
            }

            if (!ConfigMethods.enabled)
            {
                return;
            }

            int tickInterval = ConfigMethods.timeInterval * 60;
            if (threadingManager.simulationTick % tickInterval != 0)
            {
                return;
            }

            ushort serviceLaunchSite = new LaunchSite().GetLaunchSite();
            if (serviceLaunchSite == 0)
            {
                return;
            }

            LaunchMethods launchMethods = new LaunchMethods(serviceLaunchSite);
            if (!launchMethods.CheckLaunchState())
            {
                return;
            }

            bool autoFocus = ConfigMethods.autoFocus;
            switch (ConfigMethods.modMode)
            {
                case 1:
                    int arrivedVisitors = launchMethods.GetVisitorNum();
                    int targetVisitorNum = ConfigMethods.targetVisitorNum;
                    if (arrivedVisitors >= targetVisitorNum)
                    {
                        launchMethods.LaunchRocket(autoFocus);
                    }
                    break;

                default:
                    launchMethods.LaunchRocket(autoFocus);
                    break;
            }

        }
    }
}
