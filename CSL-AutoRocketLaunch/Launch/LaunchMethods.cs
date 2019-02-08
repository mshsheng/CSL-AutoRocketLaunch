using ColossalFramework;
using CSL_AutoRocketLaunch.Detour;

namespace CSL_AutoRocketLaunch.Launch
{
    class LaunchMethods
    {
        private BuildingManager instance;
        private ushort m_currentEventID;
        private EventInfo info;
        private RocketLaunchAI rocketLaunchAI;

        // Initialize
        public LaunchMethods(ushort serviceLaunchSite)
        {
            instance = Singleton<BuildingManager>.instance;
            m_currentEventID = instance.m_buildings.m_buffer[serviceLaunchSite].m_eventIndex;
            info = Singleton<EventManager>.instance.m_events.m_buffer[m_currentEventID].Info;
            rocketLaunchAI = info.m_eventAI as RocketLaunchAI;
        }

        // Use the same method as the ChirpX Panel
        public bool CheckLaunchState()
        {
            EventData data = Singleton<EventManager>.instance.m_events.m_buffer[m_currentEventID];
            int state = rocketLaunchAI.GetProductionState(m_currentEventID, ref data);
            bool flag = (data.m_flags & EventData.Flags.Ready) != EventData.Flags.None;
            return flag;
        }

        public int GetVisitorNum()
        {
            rocketLaunchAI.CountVisitors(m_currentEventID, ref Singleton<EventManager>.instance.m_events.m_buffer[m_currentEventID], out int arrivedVisitors, out int _, out int maxVisitors);
            return arrivedVisitors;
        }

        public void LaunchRocket(bool autoFocus)
        {
            ushort eventID = m_currentEventID;
            Singleton<SimulationManager>.instance.AddAction(delegate
            {
                // The original code contains the method for auto focusing, so the detour is needed only when "AutoFocus" is not checked.
                if (!autoFocus)
                {
                    RocketLaunchAIDetour.Deploy();
                }

                rocketLaunchAI.Activate(eventID, ref Singleton<EventManager>.instance.m_events.m_buffer[eventID]);

                if (!autoFocus)
                {
                    RocketLaunchAIDetour.Revert();
                }
            });
        }
    }
}
