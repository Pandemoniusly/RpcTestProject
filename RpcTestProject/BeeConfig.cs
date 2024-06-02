using BepInEx.Configuration;
using GameNetcodeStuff;
using HarmonyLib;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine.InputSystem;
using static Unity.Netcode.CustomMessagingManager;

namespace hivebombnetcode
{
    public sealed class Config
    {
        #region Properties

        public new ConfigEntry<bool> Enabled { get; set; }
        public new ConfigEntry<bool> KnockbackEnabled { get; set; }
        public new ConfigEntry<bool> VisibleExplosions { get; set; }
        public ConfigEntry<float> Radius { get; set; }
        public ConfigEntry<int> MaxPlayerDamage { get; set; }

        private static Config instance = null;
        public static Config Instance
        {
            get
            {
                if (instance == null)
                    instance = new Config();

                return instance;
            }
        }
        #endregion

        public void Setup()
        {
            Enabled = hivebombnetcode.Plugin.BepInExConfig().Bind("General", "Enabled", true, "Enables access to track your social security, banking info, home address and DNA samples, Default: On");
            KnockbackEnabled = hivebombnetcode.Plugin.BepInExConfig().Bind("General", "Masochist Mode", true, "Enables Knockback, Default: On");
            VisibleExplosions = hivebombnetcode.Plugin.BepInExConfig().Bind("General", "Explosion Visibility", true, "Turns the explosion particles On or Off, Default: On");
            MaxPlayerDamage = hivebombnetcode.Plugin.BepInExConfig().Bind("General", "Player Damage", 0, new ConfigDescription("Explosion radius, Default is 2.4 and is as big as a lightning strikes kill range", new AcceptableValueRange<int>(0, 100)));
            Radius = hivebombnetcode.Plugin.BepInExConfig().Bind("General", "Radius", 2.4f, new ConfigDescription("Explosion radius, Default is 2.4 and is as big as a lightning strikes kill range", new AcceptableValueRange<float>(0.1f, 10f)));
        }
    }
}