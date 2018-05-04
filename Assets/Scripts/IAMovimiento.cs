using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IAMovimiento : NetworkBehaviour {
	public GameObject A;
	public GameObject B;
	public GameObject C;
	private Rigidbody rb;
	private detecta DA, DB, DC;
	[SyncVar]
	public Vector3 velocidad;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		DA = A.GetComponent<detecta> ();
		DB = B.GetComponent<detecta> ();
		DC = C.GetComponent<detecta> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		izda ();
		dcha ();
		avanza ();



	}

	void avanza(){
		if (!DA.toca) {
			if (rb.velocity.magnitude <= 10)
				rb.AddForce (transform.forward * 200f);
		} else {
			//rb.AddForce (transform.forward * -200f);
		}
	}

	void izda(){

		if ((!DB.toca && DC.toca) || (DA.toca && DC.toca) || (DA.toca&&!DB.toca)) {
			transform.Rotate (0, -50f*Time.deltaTime, 0);
		}
	}

	void dcha(){
		if (DB.toca && !DC.toca) {
			transform.Rotate (0, 50f*Time.deltaTime, 0);
		}
	}
}
