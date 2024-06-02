using HarmonyLib;
using hivebombnetcode;
using Unity;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static hivebombnetcode.ExampleNetworkHandler;
using Object = UnityEngine.Object;
namespace hivebombnetcode
{
    internal class networkreal
    {
        [HarmonyPostfix, HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.Awake))]
        static void SubscribeToHandler()
        {
            ExampleNetworkHandler.Instance.LevelEvent += ReceivedEventFromServer;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RoundManager), nameof(RoundManager.DespawnPropsAtEndOfRound))]
        static void UnsubscribeFromHandler()
        {
            ExampleNetworkHandler.Instance.LevelEvent -= ReceivedEventFromServer;
        }

        static void ReceivedEventFromServer(object who, MyEventArgs args)
        {
            // Event Code Here
            ExplosionHandler.ExplodeAt(args.Args[1], args.Args[2], args.Args[3], args.Args[4], args.Args[5], args.Args[6], args.Args[7], args.Args[8]);
        }

        public static void SendEventToClients(Transform __transform,int rand)
        {
            if (!(Unity.Netcode.NetworkManager.Singleton.IsHost || Unity.Netcode.NetworkManager.Singleton.IsServer))
                return;

            ExampleNetworkHandler.Instance.EventClientRpc(__transform, rand, Config.Instance.KnockbackEnabled.Value, Config.Instance.VisibleExplosions.Value, Config.Instance.MaxPlayerDamage.Value, Config.Instance.Radius.Value);
        }
    }
}