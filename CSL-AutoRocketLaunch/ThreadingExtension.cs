using ICities;
using CSL_AutoRocketLaunch.Config;
using CSL_AutoRocketLaunch.Launch;

namespace CSL_AutoRocketLaunch
{
    public class ThreadingExtension : ThreadingExtensionBase
    {
        private ushort serviceLaunchSite = 0;

        public override void OnAfterSimulationTick()
        {
            base.OnAfterSimulationTick();

            // Check Pause
            if (threadingManager.simulationTick % 60 != 0 || threadingManager.simulationPaused)
            {
                return;
            }

            // Check Enabled
            if (!ConfigMethods.enabled)
            {
                return;
            }

            // Check Refresh Interval
            int tickInterval = ConfigMethods.timeInterval * 60; // Simulation tick: 60 times per second
            if (threadingManager.simulationTick % tickInterval != 0)
            {
                return;
            }

            // Get Launch Site
            if (serviceLaunchSite == 0)
            {
                serviceLaunchSite = new LaunchSite().GetLaunchSite();
            }
            // Recheck Launch Site
            if (serviceLaunchSite == 0)
            {
                return;
            }

            // Check Launch State
            LaunchMethods launchMethods = new LaunchMethods(serviceLaunchSite);
            if (!launchMethods.CheckLaunchState())
            {
                return;
            }

            bool autoFocus = ConfigMethods.autoFocus;

            switch (ConfigMethods.modMode)
            {
                /* 0: Immediate Mode
                   1: Visitor Mode */
                case 1:
                    int arrivedVisitors = launchMethods.GetVisitorNum();
                    int targetVisitorNum = ConfigMethods.targetVisitorNum;

                    // Check Visitor Number
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
