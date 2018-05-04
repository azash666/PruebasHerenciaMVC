using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class hit : NetworkBehaviour {
	public GameObject creador;
	void OnCollisionEnter(Collision colision){
		GameObject hit = colision.gameObject;
		if (hit != creador) {
			if (hit.tag == "Player" || hit.tag == "CPUTank") {
				var vida = hit.GetComponent<vida> ();
				if (vida != null) {
					vida.golpeador = creador;
					vida.recibirDanno (20);
					creador.GetComponent<puntuacion> ().sumapuntos (2);

				}
			}
			//if(isServer)
				Destroy (gameObject);
		}
	}
}
