using UnityEngine;
using UnityEngine.Networking;

public class movimiento : NetworkBehaviour
{
	Rigidbody rb;
	public GameObject camara;
	public GameObject huecoCamara;
	public GameObject torreta;
	public Vector3 vel;

	[SyncVar]
	private float anterior;
	[SyncVar]
	private float tiempo;
	[SyncVar]
	private float tiempoAnterior;
	private float tiempodisparo;


	public GameObject huecoBala;
	private float fuerza;
	public float fuerzaAngular;
	[SyncVar]
	private float rotacionVertical;

	private float velocidadAngular;

	[SyncVar]
	public float rotacion;




	void Start()
	{
		//Cursor.visible = false;
		fuerza = 10000f;
		if(isLocalPlayer)
			Instantiate (camara, huecoCamara.transform.position, huecoCamara.transform.rotation).transform.parent=huecoCamara.transform;
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		
		if (!isLocalPlayer) {
			if (!isServer) {
				velocidadAngular = (rotacion - anterior) / (tiempo-tiempoAnterior);
			}
			rotacion += velocidadAngular * Time.deltaTime;
			torreta.transform.rotation = Quaternion.Euler (0, rotacion, 0);
			float auxy = huecoCamara.transform.rotation.eulerAngles.y;
			huecoBala.transform.rotation = Quaternion.Euler (rotacionVertical, auxy, 0);
		}
		else {
			var x = Input.GetAxis ("Horizontal") * Time.deltaTime *60f;
			var z = Input.GetAxis ("Vertical") *Time.deltaTime * fuerza;
			var rotacionTorreta = Input.GetAxis ("Mouse X") * Time.deltaTime * 60f;
			float aux = -Input.GetAxis ("Mouse Y") * Time.deltaTime * 50f;
			float aux2 = huecoCamara.transform.rotation.eulerAngles.x;
			float auxy = huecoCamara.transform.rotation.eulerAngles.y;
			if (aux2 < 340f && aux2 >= 180f) {
				rotacionVertical = 0;
				huecoCamara.transform.rotation = Quaternion.Euler (340f, auxy, 0);
				huecoBala.transform.rotation = Quaternion.Euler (340f, auxy, 0);
			} else {
				if (aux2 > 20f && aux2<180f) {
					rotacionVertical = 0;
					huecoCamara.transform.rotation = Quaternion.Euler (20f, auxy, 0);
					huecoBala.transform.rotation = Quaternion.Euler (20f, auxy, 0);
				} else {
					rotacionVertical = aux;
					if ((aux2 < 345f && aux2 > 180f && aux > 0) || (aux2 > 15f && aux2 < 180f && aux < 0) || aux2 >= 345f || aux2 <= 15f) {
						huecoCamara.transform.Rotate (aux, 0, 0);
						huecoBala.transform.Rotate (aux, 0, 0);
					}
				}
			}
			float inclinacionBalas = huecoBala.transform.rotation.eulerAngles.x;
			CmdCambiarVertical (inclinacionBalas);
			Debug.Log (x);
			//rb.AddTorque (0, x, 0);
			transform.Rotate (0, x, 0);
			torreta.transform.Rotate (0, rotacionTorreta, 0);
			rotacion = torreta.transform.rotation.eulerAngles.y;
			CmdCambiar(rotacion);
			//CmdVelocidad (rb.velocity);
			if (rb.velocity.magnitude <= 10)
				rb.AddForce (transform.forward * z);
			
		}
	}



	[Command]
	void CmdCambiar(float rot){
		anterior = rotacion;
		rotacion = rot;
		tiempoAnterior = tiempo;
		tiempo = Time.time;
		velocidadAngular = (rotacion - anterior) / tiempo;
	}

	[Command]
	void CmdCambiarVertical(float vert){
		rotacionVertical = vert;
	}

}