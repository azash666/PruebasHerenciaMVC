using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equilibrio : MonoBehaviour {
	public GameObject DelanteIzquierda, DelanteDerecha, DetrasIzquierda, DetrasDerecha;
	public bool AI, AD, DI, DD;
	public float fuerza;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		AI = DelanteIzquierda.GetComponent<detectaRueda> ().toca;
		AD = DelanteDerecha.GetComponent<detectaRueda> ().toca;
		DI = DetrasIzquierda.GetComponent<detectaRueda> ().toca;
		DD = DetrasDerecha.GetComponent<detectaRueda> ().toca;
		if (!DI && !AI && (DD || AD)) {
			rb.AddRelativeTorque (0, 0, fuerza);
			//transform.Rotate (0, 0, -300 * Time.deltaTime);
		}
		if (!DD && (AD || !AI) && (!AD || AI)) {
			rb.AddRelativeTorque(0, 0, -fuerza);
			//transform.Rotate (0, 0, 300 * Time.deltaTime);
		}
		if (!AD && !AI && (DI || DD)) {
			rb.AddRelativeTorque(30f, 0, 0);
			//transform.Rotate (-300 * Time.deltaTime, 0, 0);
		}
		if (!DD && !DI && (AI || AD)) {
			rb.AddRelativeTorque(-30f, 0, 0);
			//transform.Rotate (300 * Time.deltaTime, 0, 0);
		}
	}
}
