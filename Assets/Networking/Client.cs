using UnityEngine;
using System.Collections;
using System;
using System.IO;


public class Client : MonoBehaviour
{

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("Alpha V1.000");
    }

    void Update()
    {
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joining Lobby");
        PhotonNetwork.CreateRoom("DevRoom");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joining Room");

    }
    // For dev, just join the open game
    void OnPhotonCreateRoomFailed()
    {
        PhotonNetwork.JoinRandomRoom();
        GameObject LoadingScreen = new GameObject("LoadingScreen");
        LoadingScreen.AddComponent<LoadingScreen>();
    }
    void OnCreatedRoom()
    	{
        // first player in the room
	        GameObject newShip = PhotonNetwork.Instantiate("LightCruiser", new Vector3(0, 0, 0),Quaternion.AngleAxis(0, Vector3.left), 0);
	        PhotonNetwork.Instantiate("LightCruiser_Phys", new Vector3(0, 0, 0), Quaternion.AngleAxis(0, Vector3.left), 0); // Ship Physics
		
		GameObject Map = new GameObject ("Map");
	        GameObject LoadingScreen = new GameObject("LoadingScreen");
	        LoadingScreen.AddComponent<LoadingScreen>();
	        LoadingScreen.GetComponent<LoadingScreen>().PlayerShipPhysical = newShip;
	        LoadingScreen.GetComponent<LoadingScreen>().PlayerShipVirtual = newShip;

		// Generate a new map seed.
		Map.AddComponent<WorldGenerator> ();
		Map.GetComponent<WorldGenerator> ().Seed = UnityEngine.Random.seed;
		Map.GetComponent<WorldGenerator> ().Ship = newShip;
	    }
}