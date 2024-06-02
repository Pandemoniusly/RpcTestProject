using HarmonyLib;
using System;

namespace hivebombnetcode
{
    [HarmonyPatch(typeof(GrabbableObject))]
    internal class beeupdate
    {
        public static int Framecount = 0;
        public static readonly Random getrandom = new Random();

        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static void beehiveexplode(GrabbableObject __instance)
        {
            if (__instance.name == "RedLocustHive(Clone)")
            {
                //finalhivebomb.Logger.LogInfo("found");
                if (Config.Instance.Enabled.Value == true)
                {
                    if (Framecount <= 0)
                    {
                        Framecount = 10;
                        if ((getrandom.Next(800) <= getrandom.Next(10)))
                        {
                            hivebombnetcode.networkreal.SendEventToClients(__instance.transform, getrandom.Next(50));
                        }
                    }
                    else if (Framecount > 0)
                    {
                        Framecount -= 1;
                    }
                }
            }
        }
    }
}