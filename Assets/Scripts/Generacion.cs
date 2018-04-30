using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Generacion : NetworkBehaviour {
	public GameObject[] terreno;

	public GameObject[] valla;
	private float tiempo;
	private int[] tipo;
	private int[] rotacion;
	private GameObject[] todo;
	// Use this for initialization

	void Generate () {
		todo = new GameObject[39];
		tipo = new int[9];
		rotacion = new int[9];
		for (int i = 0; i < 9; i++) {
			tipo [i] = Random.Range (0, terreno.Length);
			rotacion [i] = Random.Range (0, 8);
		}
		for (int i = 0; i < 9; i++) {
			GameObject objeto = (GameObject)Instantiate (terreno [tipo [i]], new Vector3 (
				                    (float)((i % 3) * 50),
				                    0,
				                    (float)((i / 3) * 50)
			                    ), Quaternion.identity);
			objeto.transform.Rotate (0f, rotacion [i] * 45f, 0f);
			todo[i] = objeto;
			NetworkServer.Spawn (objeto);
		}
		for (int i = 0; i < 30; i++) {
			GameObject objeto = (GameObject)Instantiate (valla[Random.Range(0, valla.Length)], new Vector3 (
				                    Random.Range (0, 13) * 10f,
				                    0,
				                    Random.Range (0, 13) * 10f
			                    ), Quaternion.identity);
			objeto.transform.Rotate (0f, Random.Range(0, 8) * 45f, 0f);
			todo[i+9] = objeto;
			NetworkServer.Spawn (objeto);
		}
	}

	void Start(){
		tiempo = Time.time + 300f;
		Generate ();
	}

	// Update is called once per frame
	void Update () {
		if (tiempo <= Time.time) {
			for(int i=0; i<todo.Length; i++)
				Destroy (todo [i]);
			tiempo = Time.time + 300f;
			Generate ();
		}
	}
}
