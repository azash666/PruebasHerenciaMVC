using UnityEngine;
using UnityEngine.Networking;

public class disparo : NetworkBehaviour
{
	Rigidbody rb;


	private float tiempodisparo;

	public GameObject bala;
	public GameObject huecoBala;





	void Start()
	{


		tiempodisparo = 0;
	}

	void FixedUpdate()
	{
		
		if (isLocalPlayer) {
			if (Input.GetKey(KeyCode.Mouse0)){
				if (tiempodisparo  < Time.time) {
					CmdFire ();
					tiempodisparo = Time.time+ .5f;
				}
			}
		}
	}

	[Command]
	void CmdFire(){
		GameObject bullet = Instantiate (bala, huecoBala.transform.position, huecoBala.transform.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 80;
		Destroy(bullet, 2f);
		bullet.GetComponent<hit> ().creador = gameObject;
		NetworkServer.Spawn (bullet);
	}



}