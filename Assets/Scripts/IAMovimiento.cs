using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IAMovimiento : NetworkBehaviour {
	public GameObject A, B, C, D, target;
	public GameObject[] players;
	public GameObject[] IA;
	private Rigidbody rb;
	private detecta DA, DB, DC, DD;
	public bool izquierda, derecha;
	public GameObject torreta;
	[SyncVar]
	public Vector3 velocidad;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		DA = A.GetComponent<detecta> ();
		DB = B.GetComponent<detecta> ();
		DC = C.GetComponent<detecta> ();
		DD = D.GetComponent<detecta> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isServer) {
			players = GameObject.FindGameObjectsWithTag ("Player");
			IA = GameObject.FindGameObjectsWithTag ("CPUTank");
			GameObject max;
			max = players [0];
			for (int i = 1; i < players.Length; i++) {
				if (players [i].GetComponent<puntuacion> ().puntos > max.GetComponent<puntuacion> ().puntos) {
					max = players [i];
				}
			}

			for (int i = 0; i < IA.Length; i++) {
				if (IA [i].GetComponent<puntuacion> ().puntos > max.GetComponent<puntuacion> ().puntos) {
					max = IA [i];
				}
			}
			target = max;
			Vector3 toVector = target.transform.position - transform.position;
			float angulo = Vector3.Angle (transform.forward, toVector);
			if (Vector3.Angle (transform.right, target.transform.position - transform.position) > 90f) {
				angulo = 360f - angulo;
			}
			if (angulo > 30 && angulo < 180) {
				izquierda = false;
				derecha = true;
			} else {
				derecha = false;
				if (angulo < 330 && angulo >= 180) {
					izquierda = true;
				} else {
					izquierda = false;
				}
			}
			//if (!DD.toca) {
			izda ();
			dcha ();
			avanza ();
			//} else {
			//	rb.AddForce (transform.forward * -150f);
			//}
			velocidad = rb.velocity;
			torreta.transform.localEulerAngles = new Vector3 (0, angulo, 0);
		} else {
			rb.velocity = velocidad;
		}

	}

	void avanza(){
		//if (!DD.toca) {
			if (!DA.toca) {
				if (rb.velocity.magnitude <= 10)
					rb.AddForce (transform.forward * 150f);
			}
	//	} else {
	//		rb.AddForce (transform.forward * -150f);
	//	}
	}

	void izda(){

		if ((!DB.toca && izquierda) || (DA.toca && DC.toca && !derecha) || (DA.toca&&!DB.toca) || (!izquierda && DA.toca && DC.toca)) {
			transform.Rotate (0, -80f*Time.deltaTime, 0);
			Debug.Log("Izda");
		}
	}

	void dcha(){
		if ((DA.toca && DB.toca && !DC.toca )||( derecha && DB.toca && !DC.toca )||( derecha && !DB.toca && !DC.toca)) {
			transform.Rotate (0, 80f*Time.deltaTime, 0);
			Debug.Log("Dcha");
		}
	}
}
