using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectaRueda : MonoBehaviour {
	public bool toca;
	public int cosas;
	public GameObject propietario;
	// Use this for initialization
	void Start(){
		cosas = 0;
	}
	void Update () {
		if (cosas <= 0) {
			toca = false;
			cosas = 0;
		} else {
			toca = true;
		}
	}
	void OnTriggerEnter(Collider colision){
		GameObject hit = colision.gameObject;
		if (hit != propietario && (hit.tag!="Player" || hit.tag!="CPUTank")) {
			cosas++;
		}
	}
	void OnTriggerExit(Collider colision){
		GameObject hit = colision.gameObject;
		if (hit != propietario && (hit.tag!="Player" || hit.tag!="CPUTank")) {
			cosas--;
		}
	}
}
