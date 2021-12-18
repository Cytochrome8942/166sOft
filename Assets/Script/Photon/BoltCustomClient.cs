using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit.Platform.Photon;
using UdpKit;
using System;

public class BoltCustomClient : GlobalEventListener
{
    public static BoltCustomClient instance;
    public static void StartClient()
    {
        if (instance == null)
        {
            if (BoltCustomServer.instance != null){
                BoltLauncher.Shutdown();
            }

            instance = new GameObject().AddComponent<BoltCustomClient>();
            instance.name = "BoltCustomClient";
            DontDestroyOnLoad(instance);

            BoltLauncher.StartClient();
        }
    }
    
    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
    {
        if(BoltNetwork.IsClient)
        Destroy(gameObject);
    }
}
