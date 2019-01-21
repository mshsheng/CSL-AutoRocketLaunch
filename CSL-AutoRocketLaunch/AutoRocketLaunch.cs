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
                if (serviceLaunchSite == 0)
                {
                    Debug.Log("No Launch Site Found. Exit.");
                }
                else
                {
                    int[] visitorNum = (new VisitorMethods()).GetVisitorNum(serviceLaunchSite);
                    string logStr = "Found Launch Site. Current Visitor Number: " + visitorNum[0].ToString() + " Max Visitor Number: " + visitorNum[1].ToString();
                    Debug.Log(logStr);
                }
            }
            base.OnAfterSimulationTick();
        }
    }
}
