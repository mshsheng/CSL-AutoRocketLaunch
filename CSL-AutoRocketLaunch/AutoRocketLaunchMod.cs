using ICities;
using UnityEngine;
using CSL_AutoRocketLaunch.TranslationFramework;
using ColossalFramework.UI;

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
            get { return translation.GetTranslation("AUTO-ROCKET-LAUNCH-modDescription"); }
        }

        // Sets up a settings user interface
        public void OnSettingsUI(UIHelperBase helper)
        {
            // Load the configuration
            AutoRocketLaunchConfiguration config = Configuration<AutoRocketLaunchConfiguration>.Load();

            // Add Setting Group
            UIHelperBase groupEnable = helper.AddGroup(translation.GetTranslation("AUTO-ROCKET-LAUNCH-modName"));
            UIHelperBase groupSettings = helper.AddGroup(translation.GetTranslation("AUTO-ROCKET-LAUNCH-settings"));

            bool enabled = config.enabled;

            groupEnable.AddCheckbox(translation.GetTranslation("AUTO-ROCKET-LAUNCH-enable"), enabled, sel =>
            {
                config.enabled = sel;
                Configuration<AutoRocketLaunchConfiguration>.Save();
            });

            string[] modeLabels =
            {
                translation.GetTranslation("AUTO-ROCKET-LAUNCH-immediateMode"),
                translation.GetTranslation("AUTO-ROCKET-LAUNCH-visitorMode")
            };
            int mode = config.mode;

            groupSettings.AddDropdown(translation.GetTranslation("AUTO-ROCKET-LAUNCH-modeSetting"), modeLabels, mode, sel =>
            {
                config.mode = sel;
                Configuration<AutoRocketLaunchConfiguration>.Save();
            });

            string modeDescription = translation.GetTranslation("AUTO-ROCKET-LAUNCH-immediateModeDescription") + "\n" + 
                translation.GetTranslation("AUTO-ROCKET-LAUNCH-visitorModeDescription") + "\n";

            UITextField txtModeDescription = (UITextField)groupSettings.AddTextfield(modeDescription," ", (s) => { }, (s) => { });
            txtModeDescription.Disable();

            groupSettings.AddSpace(20);

            // Set Target Tourist Number
            groupSettings.AddTextfield(translation.GetTranslation("AUTO-ROCKET-LAUNCH-targetVisitorNum"), config.targetVisitorNum.ToString(), (value) =>
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
