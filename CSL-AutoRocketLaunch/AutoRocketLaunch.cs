using ICities;

namespace CSL_AutoRocketLaunch
{
    public class AutoRocketLaunch : ThreadingExtensionBase
    {
        public override void OnAfterSimulationTick()
        {
            base.OnAfterSimulationTick();

            if (threadingManager.simulationTick % 60 != 0 || threadingManager.simulationPaused)
            {
                return;
            }

            AutoRocketLaunchConfiguration config = Configuration<AutoRocketLaunchConfiguration>.Load();
            if (!config.enabled)
            {
                return;
            }

            int timeInterval = config.timeInterval * 60;
            if (threadingManager.simulationTick % timeInterval != 0)
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

            bool autoFocus = config.autoFocus;
            switch (config.mode)
            {
                case 1:
                    int arrivedVisitors = launchMethods.GetVisitorNum();
                    int targetVisitorNum = config.targetVisitorNum;
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
