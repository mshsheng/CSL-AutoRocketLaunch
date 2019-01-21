using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using CSL_AutoRocketLaunch;

namespace CSL_AutoRocketLaunch
{
    public class AutoRocketLaunch : ThreadingExtensionBase
    {
        public override void OnAfterSimulationTick()
        {
            if (threadingManager.simulationTick % 1024 == 0 && !threadingManager.simulationPaused)
            {
                ushort serviceLaunchSite = (new LaunchSite()).GetLaunchSite();
                if (serviceLaunchSite != 0)
                {
                    LaunchMethods launchMethods = new LaunchMethods(serviceLaunchSite);
                    bool launchState = launchMethods.CheckLaunchState();
                    if (launchState)
                    {
                        int arrivedVisitors = launchMethods.GetVisitorNum();
                        AutoRocketLaunchConfiguration config = Configuration<AutoRocketLaunchConfiguration>.Load();
                        int targetVisitorNum = config.targetVisitorNum;
                        if (arrivedVisitors >= targetVisitorNum)
                        {
                            launchMethods.LaunchRocket();
                        }
                    }
                }
            }
            base.OnAfterSimulationTick();
        }
    }
}
