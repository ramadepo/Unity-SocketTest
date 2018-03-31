using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using UnityEngine.UI;

public class Client : MonoBehaviour {

	private bool socketReady;
	private TcpClient socket;
	private NetworkStream stream;
	private StreamWriter writer;
	private StreamReader reader;

	public void ConnectToServer(){	//connect the server
		if (socketReady) {
			return;
		}

		string host = "127.0.0.1";
		int port = 8765;
		
		try {
			socket = new TcpClient(host,port);
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;
		} catch (Exception ex) {
			Debug.Log ("Socket error : " + ex.Message);
		}

	}

	private void Update(){	//keep tracking if there is message from server
		if(socketReady){
			if (stream.DataAvailable) {
				string data = reader.ReadLine();
				if (data !=null) {
					OnIncomingData(data);
				}
			}
		}
	}

	
	private void OnIncomingData(string data){	//receive the server's message (no use in this project)
		Debug.Log ("Server : " + data);
	}

	public void OnSpawnButton(){	//Spawn button event
		string data = GameObject.Find ("SpawnNumberInput").GetComponent<InputField> ().text;
		GameObject.Find ("SpawnNumberInput").GetComponent<InputField> ().text = "";

		SpawnSend (data);
	}


	private void SpawnSend(string data){	//send the message to server for spawnning
		if (!socketReady) {
			return;
		}

		writer.WriteLine (data);
		writer.Flush ();
	}



}
