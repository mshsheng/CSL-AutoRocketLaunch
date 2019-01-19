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

        public bool CheckLaunchSite()
        {
            BuildingManager _buildingManager = Singleton<BuildingManager>.instance;
            FastList<ushort> monumentBuildings = _buildingManager.GetServiceBuildings(ItemClass.Service.Monument);

            var buffer = _buildingManager.m_buildings.m_buffer;

            foreach (ushort serviceMonument in monumentBuildings)
            {
                var _buildingInfoTooltipThumbnail = buffer[serviceMonument].Info.m_InfoTooltipThumbnail;
                if (_buildingInfoTooltipThumbnail == "ChirpXLaunchSite")
                {
                    return true;
                }
            }

            return false;

        }

        public override void OnAfterSimulationTick()
        {
            if (threadingManager.simulationTick % 1024 == 0 && !threadingManager.simulationPaused)
            {
                if (!CheckLaunchSite())
                {
                    Debug.Log("No Launch Site Found. Exit.");
                    return;
                }
                Debug.Log("Found Launch Site.");
            }
            base.OnAfterSimulationTick();
        }
    }
}
