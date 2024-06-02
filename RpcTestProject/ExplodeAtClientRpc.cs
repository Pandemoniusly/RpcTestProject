using System;
using hivebombnetcode;
using Unity.Netcode;
using UnityEngine;
using Object = UnityEngine.Object;

namespace hivebombnetcode;

public class ExampleNetworkHandler : NetworkBehaviour
{
    public class MyEventArgs : EventArgs
    {
        public MyEventArgs(params float[] args)
        {
            Args = args;
        }

        public float[] Args { get; set; }
    }
    public override void OnNetworkSpawn()
    {
        LevelEvent = null;

        if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
            Instance?.gameObject.GetComponent<NetworkObject>().Despawn();
        Instance = this;

        base.OnNetworkSpawn();
    }

    [ClientRpc]
    public void EventClientRpc(Transform __transform, int rand, bool knockback, bool visible, int dmg, float rad)
    {
        LevelEvent?.Invoke(this,new MyEventArgs(__transform.position.x, __transform.position.y, __transform.position.z, rand,(knockback ? 1 : 0), (visible ? 1 : 0), dmg,rad));  // If the event has subscribers (does not equal null), invoke the event
    }

    public event EventHandler<MyEventArgs> LevelEvent;

    public static ExampleNetworkHandler Instance { get; private set; }

}