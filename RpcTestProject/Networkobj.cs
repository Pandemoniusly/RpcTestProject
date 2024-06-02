using HarmonyLib;
using hivebombnetcode;
using Unity.Netcode;
using UnityEngine;

namespace hivebombnetcode
{
    [HarmonyPatch]
    public class NetworkObjectManager
    {

        static AssetBundle MainAssetBundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Plugin.instance.Info.Location), "ExampleModAssets"));

        [HarmonyPostfix, HarmonyPatch(typeof(GameNetworkManager), nameof(GameNetworkManager.Start))]
        public static void Init()
        {
            if (networkPrefab != null)
                return;
            // Info is an instance member field of your `BaseUnityPlugin` class.
            networkPrefab = (GameObject)MainAssetBundle.LoadAsset("ExampleNetworkHandler");
            networkPrefab.AddComponent<hivebombnetcode.ExampleNetworkHandler>();

            Unity.Netcode.NetworkManager.Singleton.AddNetworkPrefab(networkPrefab);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.Awake))]
        static void SpawnNetworkHandler()
        {
            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
            {
                var networkHandlerHost = Object.Instantiate(hivebombnetcode.NetworkObjectManager.networkPrefab, Vector3.zero, Quaternion.identity);
                networkHandlerHost.GetComponent<NetworkObject>().Spawn();
            }
        }

        static GameObject networkPrefab;
    }
}