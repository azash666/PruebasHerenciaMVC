using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class hit : NetworkBehaviour {
	public GameObject creador;
	void OnCollisionEnter(Collision colision){
		GameObject hit = colision.gameObject;
		if (hit != creador) {
			if (hit.tag == "Player") {
				var vida = hit.GetComponent<vida> ();
				if (vida != null) {
					vida.recibirDanno (20);
				}
			}
			if(isServer)
				Destroy (gameObject);
		}
	}
}
