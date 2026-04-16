using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace NoMoreJetsplosions
{
    public static class PluginInfo
    {
        public const string GUID = "Detologist.NoMoreJetsplosions";
        public const string NAME = "NoMoreJetsplosions";
        public const string VERSION = "1.0.0";
    }

    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource Log;

        void Awake()
        {
            Log = Logger;
            
            var harmony = new Harmony(PluginInfo.GUID);
            harmony.PatchAll();

            Log.LogInfo($"{PluginInfo.NAME} Loaded Successfully!");
        }
    }

    [HarmonyPatch(typeof(JetpackItem))]
    public class JetpackPatch
    {
        [HarmonyPatch("ExplodeJetpackServerRpc")]
        [HarmonyPrefix]
        static bool StopOverheat()
        {
            Plugin.Log.LogDebug("Preventing Jetpack Overheat.");
            return false;
        }

        [HarmonyPatch("OnTriggerEnter")]
        [HarmonyPrefix]
        static bool StopCollision() => false;
    }
}