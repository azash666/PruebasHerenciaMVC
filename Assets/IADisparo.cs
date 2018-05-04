using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IADisparo : MonoBehaviour {
	private float tiempodisparo;

	public GameObject bala;
	public GameObject huecoBala;





	void Start()
	{
		tiempodisparo = 0;
	}

	void FixedUpdate()
	{



			if (tiempodisparo  < Time.time) {
				CmdFire ();
				tiempodisparo = Time.time + 0.5f + (float)Random.Range (0, 100)/100f;
			}


	}

	//[Command]
	void CmdFire(){
		GameObject bullet = Instantiate (bala, huecoBala.transform.position, huecoBala.transform.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 60;
		Destroy(bullet, 2f);
		bullet.GetComponent<hit> ().creador = gameObject;
		//NetworkServer.Spawn (bullet);
	}

}
