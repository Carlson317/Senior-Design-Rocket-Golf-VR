using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class NetworkLobbyHook : LobbyHook {
	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager networkManager, GameObject lobbyPlayer, GameObject gamePlayer){

		LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer> ();
		SetUpLocalPlayer localPlayer = gamePlayer.GetComponent<SetUpLocalPlayer> ();
		MyPlayerController localPlayerConroller = gamePlayer.GetComponent<MyPlayerController> ();
		localPlayerConroller.pName = lobby.playerName;
		localPlayerConroller.kickCount = lobby.kickNumber;
		localPlayerConroller.gameEnded = lobby.finished;
		localPlayer.pName = lobby.playerName;
		localPlayer.pColor = lobby.playerColor;
		localPlayer.noise = lobby.noise;
		localPlayer.persistence = lobby.persistence;
		localPlayer.amplitude = lobby.amplitude;
		localPlayer.randX = lobby.randX;
		localPlayer.randY = lobby.randY;
		localPlayer.holeX = lobby.holeX;
		localPlayer.randTree = lobby.randTree;
	}
}
