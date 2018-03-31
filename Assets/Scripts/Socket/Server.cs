using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.IO;

public class Server : MonoBehaviour {

	public int port = 8765;
	public SpawningCreature spawnManager;
	private List<ServerClient> clients;
	private List<ServerClient> disconnectList;
	private TcpListener server;
	private bool serverStarted;
	private int sensorCount = 0;

	// Use this for initialization
	private void Start () {
		clients = new List<ServerClient> ();	//all clients are in this list
		disconnectList = new List<ServerClient> ();	//all disconnect clients are in this list

		//make sure that server is started successfully
		try {
			server = new TcpListener(IPAddress.Any,port);
			server.Start();

			StartListening();	//start waiting client connection
			serverStarted = true;
			Debug.Log("Server has been started on port " + port);
		} catch (Exception ex) {
			Debug.Log ("Socket Error: " + ex.Message);
		}
	}
	
	// keep listenint client message
	private void Update () {
		if (!serverStarted) {
			return;
		}

		foreach (ServerClient c in clients) {
			if (!IsConnected (c.tcp)) {
				c.tcp.Close ();
				disconnectList.Add (c);
				continue;
			} else {
				NetworkStream s = c.tcp.GetStream ();	//get the client's stream
				if (s.DataAvailable) {
					StreamReader reader = new StreamReader (s, true);
					string data = reader.ReadLine ();
					
					if (data != null) {
						OnIncomingData (c, data);
					}
				}
			}
		}
	}

	//waiting client connect
	private void StartListening(){
		server.BeginAcceptTcpClient (AcceptTcpClient, server);
	}

	//use asynchornous signal to built connection and add to the connection list "clients"
	private void AcceptTcpClient(IAsyncResult ar){
		TcpListener listener = (TcpListener)ar.AsyncState;

		clients.Add (new ServerClient (listener.EndAcceptTcpClient (ar),sensorCount++));
		StartListening ();	//continue waiting client connect

		Debug.Log ("Sensor " + clients [clients.Count - 1].clientNumber + " has connected");
	}

	//deal with the client message
	private void OnIncomingData(ServerClient c,string data){
		Debug.Log ("Sensor(" + c.clientNumber + ") has send the message: " + data);

		for (int i = 0; i < int.Parse(data); i++) {	//call SpawnManager to spawn
			spawnManager.Spawn ();
		}

	}

	//transfer the message to every client (no use in this project)
	private void Broadcast(string data, List<ServerClient> cl){
		foreach (ServerClient c in cl) {
			try {
				StreamWriter writer = new StreamWriter(c.tcp.GetStream());
				writer.WriteLine(data);
				writer.Flush();
			} catch (Exception ex) {
				Debug.Log ("Write error : " + ex.Message + "to sensor " + c.clientNumber);

			}
		}
	}

	//judge the client is still in connection or not
	private bool IsConnected(TcpClient c){
		try {
			if (c != null && c.Client != null && c.Client.Connected) {
				return true;
			}
			else{
				return false;
			}
		} catch{
			return false;
		}
	}
}

//the class about the client for server management
public class ServerClient{
	public TcpClient tcp;
	public int clientNumber;

	public ServerClient(TcpClient clientSocket, int sensorNumber){
		clientNumber = sensorNumber;
		tcp = clientSocket;
	}
}