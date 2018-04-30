using UnityEngine;
using UnityEngine.Networking;

public class movimiento : NetworkBehaviour
{
	Rigidbody rb;
	public GameObject camara;
	public GameObject huecoCamara;
	public GameObject torreta;
	public Vector3 vel;


	private float anterior;
	private float tiempo;
	private float tiempodisparo;

	public GameObject bala;
	public GameObject huecoBala;

	[SyncVar]
	private float velocidadAngular;

	[SyncVar/*(hook = "cambiar")*/]
	public float rotacion;




	void Start()
	{
		if(isLocalPlayer)
			Instantiate (camara, huecoCamara.transform.position, huecoCamara.transform.localRotation).transform.parent=huecoCamara.transform;
		tiempodisparo = 0;
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		
		if (!isLocalPlayer) {
			rotacion += velocidadAngular * Time.deltaTime;
			torreta.transform.rotation = Quaternion.Euler (0, rotacion, 0);
		}
		else {
			var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 60.0f;
			var z = Input.GetAxis ("Vertical") * 50f;
			var rotacionTorreta = Input.GetAxis ("Mouse X") * Time.deltaTime * 60f;
			var rotacionVertical = -Input.GetAxis ("Mouse Y") * Time.deltaTime * 50f;

			transform.Rotate (0, x, 0);
			torreta.transform.Rotate (0, rotacionTorreta, 0);
			huecoBala.transform.Rotate (rotacionVertical, 0, 0);
			huecoCamara.transform.Rotate (rotacionVertical, 0, 0);
			rotacion = torreta.transform.rotation.eulerAngles.y;
			CmdCambiar(rotacion);
			//CmdVelocidad (rb.velocity);
			if (rb.velocity.magnitude <= 10)
				rb.AddForce (transform.forward * z);
			if (Input.GetKey(KeyCode.Mouse0)){
				if (tiempodisparo + .5f < Time.time) {
					CmdFire ();
					tiempodisparo = Time.time;
				}
			}
		}
	}

	[Command]
	void CmdFire(){
		GameObject bullet = Instantiate (bala, huecoBala.transform.position, huecoBala.transform.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 60;
		Destroy(bullet, 2f);
		bullet.GetComponent<hit> ().creador = gameObject;
		NetworkServer.Spawn (bullet);
	}


	[Command]
	void CmdCambiar(float rot){
		anterior = rotacion;
		rotacion = rot;
		tiempo = Time.time;
		velocidadAngular = (rotacion - anterior) / tiempo;
	}



}