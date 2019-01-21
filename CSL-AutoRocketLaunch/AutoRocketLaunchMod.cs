using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;
using UnityEngine;

namespace CSL_AutoRocketLaunch
{
    public class AutoRocketLaunchMod : IUserMod
    {
        public string Name
        {
            get { return "Auto Rocket Launch"; }
        }

        public string Description
        {
            get { return "Launching rockets automatically"; }
        }

        // Sets up a settings user interface
        public void OnSettingsUI(UIHelperBase helper)
        {
            // Load the configuration
            AutoRocketLaunchConfiguration config = Configuration<AutoRocketLaunchConfiguration>.Load();

            // Add Setting Group
            UIHelperBase group = helper.AddGroup("Auto Rocket Launch");

            // Set Target Visitor Number
            group.AddTextfield("Target Visitor Number", config.targetVisitorNum.ToString(), (value) =>
            {
                try
                {
                    config.targetVisitorNum = ushort.Parse(value);
                    Configuration<AutoRocketLaunchConfiguration>.Save();
                }
                catch
                {
                    Debug.Log("AutoRocketLaunch: TypeError");
                }
            });
            
        }
    }
}
