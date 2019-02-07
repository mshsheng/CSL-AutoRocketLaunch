using ColossalFramework;

namespace CSL_AutoRocketLaunch.Launch
{
    class LaunchSite
    {
        private BuildingManager buildingManager;
        private FastList<ushort> monumentBuildings;

        public ushort GetLaunchSite()
        {
            buildingManager = Singleton<BuildingManager>.instance;
            monumentBuildings = buildingManager.GetServiceBuildings(ItemClass.Service.Monument);

            var buffer = buildingManager.m_buildings.m_buffer;
            ushort serviceLaunchSite = 0;

            foreach (ushort serviceMonument in monumentBuildings)
            {
                var _buildingInfoTooltipThumbnail = buffer[serviceMonument].Info.m_InfoTooltipThumbnail;
                if (_buildingInfoTooltipThumbnail == "ChirpXLaunchSite")
                {
                    serviceLaunchSite = serviceMonument;
                    break;
                }
            }

            return serviceLaunchSite;
        }
    }
}
