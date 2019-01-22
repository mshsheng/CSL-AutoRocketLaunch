using ICities;
using UnityEngine;
using CSL_AutoRocketLaunch.TranslationFramework;

namespace CSL_AutoRocketLaunch
{
    public class AutoRocketLaunchMod : IUserMod
    {
        public static Translation translation = new Translation();

        public string Name
        {
            get { return "Auto Rocket Launch"; }
        }

        public string Description
        {
            get { return translation.GetTranslation("AUTO-ROCKET-LAUNCH-MODDESCRIPTION");  }
        }

        // Sets up a settings user interface
        public void OnSettingsUI(UIHelperBase helper)
        {
            // Load the configuration
            AutoRocketLaunchConfiguration config = Configuration<AutoRocketLaunchConfiguration>.Load();

            // Add Setting Group
            UIHelperBase group = helper.AddGroup(translation.GetTranslation("AUTO-ROCKET-LAUNCH-MODNAME"));

            // Set Target Tourist Number
            group.AddTextfield(translation.GetTranslation("AUTO-ROCKET-LAUNCH-targetVisitorNum"), config.targetVisitorNum.ToString(), (value) =>
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
