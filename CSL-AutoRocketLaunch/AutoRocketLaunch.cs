using ICities;

namespace CSL_AutoRocketLaunch
{
    public class AutoRocketLaunch : ThreadingExtensionBase
    {
        public override void OnAfterSimulationTick()
        {
            if (threadingManager.simulationTick % 1024 == 0 && !threadingManager.simulationPaused)
            {
                AutoRocketLaunchConfiguration config = Configuration<AutoRocketLaunchConfiguration>.Load();
                if (config.enabled)
                {
                    ushort serviceLaunchSite = (new LaunchSite()).GetLaunchSite();
                    if (serviceLaunchSite != 0)
                    {
                        LaunchMethods launchMethods = new LaunchMethods(serviceLaunchSite);
                        bool launchState = launchMethods.CheckLaunchState();
                        if (launchState)
                        {
                            switch (config.mode)
                            {
                                case 1:
                                    int arrivedVisitors = launchMethods.GetVisitorNum();
                                    int targetVisitorNum = config.targetVisitorNum;
                                    if (arrivedVisitors >= targetVisitorNum)
                                    {
                                        launchMethods.LaunchRocket();
                                    }
                                    break;

                                default:
                                    launchMethods.LaunchRocket();
                                    break;
                            }
                        }
                    }
                }
            }
            base.OnAfterSimulationTick();
        }
    }
}
