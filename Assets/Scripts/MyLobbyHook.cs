using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class MyLobbyHook : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        SetupLocalPlayer player = gamePlayer.GetComponent<SetupLocalPlayer>();
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        player.charID = lobby.CharID;
    }
}
