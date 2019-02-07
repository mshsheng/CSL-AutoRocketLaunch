using System;
using System.Reflection;
using ColossalFramework;
using UnityEngine;

namespace CSL_AutoRocketLaunch
{
    public class RocketLaunchAIDetour : EventAI
    {
        private static RedirectCallsState _state;
        private static readonly MethodInfo BeginEventMethod = typeof(RocketLaunchAI).GetMethod("BeginEvent", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo BeginEventDetour = typeof(RocketLaunchAIDetour).GetMethod("BeginEvent", BindingFlags.Instance | BindingFlags.NonPublic);
        private static bool _deployed;

        private static MethodInfo FindVehiclesMethod = typeof(RocketLaunchAI).GetMethod("FindVehicles", BindingFlags.Instance | BindingFlags.NonPublic);

        public VehicleInfo m_crawlerVehicle => (VehicleInfo)typeof(RocketLaunchAI).GetField("m_crawlerVehicle", BindingFlags.Instance | BindingFlags.Public).GetValue(this);
        public VehicleInfo m_rocketVehicle => (VehicleInfo)typeof(RocketLaunchAI).GetField("m_rocketVehicle", BindingFlags.Instance | BindingFlags.Public).GetValue(this);

        public EffectInfo m_launchAlarmEffect => (EffectInfo)typeof(RocketLaunchAI).GetField("m_launchAlarmEffect", BindingFlags.Instance | BindingFlags.Public).GetValue(this);
        public Vector3 m_rocketLaunchPosition => (Vector3)typeof(RocketLaunchAI).GetField("m_rocketLaunchPosition", BindingFlags.Instance | BindingFlags.Public).GetValue(this);

        public static void Deploy()
        {
            if (!_deployed)
            {
                try
                {
                    _state = RedirectionHelper.RedirectCalls(BeginEventMethod, BeginEventDetour);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
                _deployed = true;
            }
        }

        public static void Revert()
        {
            if (_deployed)
            {
                try
                {
                    RedirectionHelper.RevertRedirect(BeginEventMethod, _state);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
                _deployed = false;
            }
        }

        private void FindVehicles(ushort eventID, ref EventData data, out ushort crawler, out ushort rocket)
        {
            crawler = 0;
            rocket = 0;

            object[] content = { eventID, data, crawler, rocket };
            FindVehiclesMethod.Invoke(this, content);

            data = (EventData)content[1];
            crawler = (ushort)content[2];
            rocket = (ushort)content[3];
        }

        protected override void BeginEvent(ushort eventID, ref EventData data)
        {
            if ((data.m_flags & EventData.Flags.Ready) == EventData.Flags.None)
            {
                data.m_flags = ((data.m_flags & ~EventData.Flags.Preparing) | EventData.Flags.Ready);
                return;
            }
            data.m_flags |= EventData.Flags.Success;
            base.BeginEvent(eventID, ref data);
            data.m_startFrame = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            data.m_expireFrame = CalculateExpireFrame(data.m_startFrame);
            VehicleManager instance = Singleton<VehicleManager>.instance;
            FindVehicles(eventID, ref data, out ushort crawler, out ushort rocket);
            if (crawler != 0)
            {
                VehicleInfo info = instance.m_vehicles.m_buffer[crawler].Info;
                info.m_vehicleAI.SetTarget(crawler, ref instance.m_vehicles.m_buffer[crawler], 0);
            }
            if (rocket != 0)
            {
                ushort leadingVehicle = instance.m_vehicles.m_buffer[rocket].m_leadingVehicle;
                if (leadingVehicle != 0)
                {
                    instance.m_vehicles.m_buffer[leadingVehicle].m_trailingVehicle = 0;
                    instance.m_vehicles.m_buffer[rocket].m_leadingVehicle = 0;
                }
                VehicleInfo info2 = instance.m_vehicles.m_buffer[rocket].Info;
                info2.m_vehicleAI.SetTarget(rocket, ref instance.m_vehicles.m_buffer[rocket], 0);
            }
            if ((object)m_launchAlarmEffect != null && data.m_building != 0)
            {
                InstanceID instance2 = default(InstanceID);
                instance2.Event = eventID;
                Vector3 position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_building].CalculatePosition(m_rocketLaunchPosition);
                EffectInfo.SpawnArea spawnArea = new EffectInfo.SpawnArea(position, Vector3.up, 10f);
                Singleton<EffectManager>.instance.DispatchEffect(m_launchAlarmEffect, instance2, spawnArea, Vector3.zero, 0f, 1f, Singleton<AudioManager>.instance.DefaultGroup);
            }
        }
    }
}
