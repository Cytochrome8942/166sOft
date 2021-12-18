using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit.Platform.Photon;
using UdpKit;

public class BoltCustomServer : GlobalEventListener
{
    public static BoltCustomServer instance;
    public static void StartServer()
    {
        if (instance == null)
        {
            if (BoltCustomClient.instance != null){
                BoltLauncher.Shutdown();
            }
            instance = new GameObject().AddComponent<BoltCustomServer>();
            instance.name = "BoltCustomServer";
            DontDestroyOnLoad(instance);

            BoltConfig config = BoltRuntimeSettings.instance.GetConfigCopy();
            config.maxEntityPriority = 10;
            BoltLauncher.StartServer(new UdpEndPoint(UdpIPv4Address.Any, 27000), config);
        }
    }
    public override void BoltStartDone()
    {
        var customToken = new PhotonRoomProperties();
        customToken.IsOpen = true;
        customToken.IsVisible = true;
        BoltMatchmaking.CreateSession(sessionID: "test", sceneToLoad: "RoomLobbyScene", token: customToken);
    }
    public override void SessionCreationFailed(UdpSession session, UdpSessionError errorReason)
    {
        BoltCustomClient.StartClient();
    }
    public override void SessionConnected(UdpSession session, IProtocolToken token)
    {
        Debug.Log(((BoltTokens.RoomAccessToken)token).userName);
    }
    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
    {
        if(BoltNetwork.IsServer)
            Destroy(gameObject);
    }
}
