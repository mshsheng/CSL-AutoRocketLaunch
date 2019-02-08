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
            monumentBuildings = buildingManager.GetServiceBuildings(ItemClass.Service.Monument); // ChirpX Launch Site: Monument

            Building[] buffer = buildingManager.m_buildings.m_buffer;
            ushort serviceLaunchSite = 0;

            foreach (ushort serviceMonument in monumentBuildings)
            {
                string _buildingObjectName = buffer[serviceMonument].Info.gameObject.name;
                if (_buildingObjectName == "ChirpX Launch Control Center") // Main Building Name: ChirpX Launch Control Center
                {
                    serviceLaunchSite = serviceMonument;
                    break;
                }
            }

            return serviceLaunchSite;
        }
    }
}
