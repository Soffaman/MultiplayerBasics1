using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using System.Net;

public class MyNetworkManager : NetworkManager
{
    

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
        player.SetDisplayName($"Player {numPlayers}");

        Color32 displayColor = new Color32(
            (byte)Random.Range(1, 255),
            (byte)Random.Range(1, 255),
            (byte)Random.Range(1, 255), 255);

        player.SetDisplayColor(displayColor);

    }
}
