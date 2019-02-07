﻿using ICities;
using UnityEngine;
using CSL_AutoRocketLaunch.Config;
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

        // Settings UI
        public void OnSettingsUI(UIHelperBase helper)
        {
            // Load the configuration
            Configs config = Configuration<Configs>.Load();

            // Add Setting Group
            UIHelperBase groupEnable = helper.AddGroup(translation.GetTranslation("AUTO-ROCKET-LAUNCH-modName"));
            UIHelperBase groupSettings = helper.AddGroup(translation.GetTranslation("AUTO-ROCKET-LAUNCH-settings"));

            groupEnable.AddCheckbox(translation.GetTranslation("AUTO-ROCKET-LAUNCH-enable"), config.enabled, sel =>
            {
                config.enabled = sel;
                Configuration<Configs>.Save();
            });

            groupSettings.AddTextfield(translation.GetTranslation("AUTO-ROCKET-LAUNCH-timeInterval"), config.timeInterval.ToString(), value =>
            {
                try
                {
                    config.timeInterval = int.Parse(value);
                    Configuration<Configs>.Save();
                }
                catch
                {
                    Debug.Log("AutoRocketLaunch: TypeError");
                }
            });

            groupSettings.AddCheckbox(translation.GetTranslation("AUTO-ROCKET-LAUNCH-autoFocus"), config.autoFocus, sel =>
            {
                config.autoFocus = sel;
                Configuration<Configs>.Save();
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
                Configuration<Configs>.Save();
            });

            string modeDescription = translation.GetTranslation("AUTO-ROCKET-LAUNCH-immediateModeDescription") + "\n" +
                translation.GetTranslation("AUTO-ROCKET-LAUNCH-visitorModeDescription") + "\n";

            UITextField txtModeDescription = (UITextField)groupSettings.AddTextfield(modeDescription, " ", (s) => { }, (s) => { });
            txtModeDescription.Disable();

            groupSettings.AddSpace(20);

            // Set Target Tourist Number
            groupSettings.AddTextfield(translation.GetTranslation("AUTO-ROCKET-LAUNCH-targetVisitorNum"), config.targetVisitorNum.ToString(), value =>
            {
                try
                {
                    config.targetVisitorNum = int.Parse(value);
                    Configuration<Configs>.Save();
                }
                catch
                {
                    Debug.Log("AutoRocketLaunch: TypeError");
                }
            });
        }
    }
}
