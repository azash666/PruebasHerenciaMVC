using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class puntuacion : NetworkBehaviour {
	[SyncVar]
	public int puntos;
	// Use this for initialization
	void Start () {
		puntos = 0;
	}

	public void sumapuntos(int puntos){
		CmdSumapuntos (puntos);
	}
	
	[Command]
	private void CmdSumapuntos(int punt){
		puntos += punt;
	}
}
