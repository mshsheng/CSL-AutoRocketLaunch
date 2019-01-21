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
    class VisitorMethods
    {
        private ushort m_currentEventID;

        public int[] GetVisitorNum(ushort serviceLaunchSite)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            m_currentEventID = instance.m_buildings.m_buffer[serviceLaunchSite].m_eventIndex;
            EventInfo info = Singleton<EventManager>.instance.m_events.m_buffer[m_currentEventID].Info;
            RocketLaunchAI rocketLaunchAI = info.m_eventAI as RocketLaunchAI;
            rocketLaunchAI.CountVisitors(m_currentEventID, ref Singleton<EventManager>.instance.m_events.m_buffer[m_currentEventID], out int arrivedVisitors, out int _, out int maxVisitors);
            Debug.Log(arrivedVisitors.ToString());
            int[] visitorNum = { arrivedVisitors, maxVisitors };
            return visitorNum;
        }
    }
}
