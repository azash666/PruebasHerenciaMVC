using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour {
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

			Destroy (gameObject);
		}
	}
}
