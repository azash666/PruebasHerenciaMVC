using UnityEngine;
using UnityEngine.Networking;

public class vida : NetworkBehaviour
{
	Rigidbody rb;
	public bool hitt;
	public GameObject golpeador;
	public float vidaMaxima;
	public float anterior;
	public float velocidad;
	[SyncVar]
	public float vidaActual;
	public NetworkStartPosition[] spawns;


	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		vidaActual = vidaMaxima;
		hitt = false;
		//generador.GetComponent<Generacion> ().players.Add (this);
	}

	void FixedUpdate(){
		transform.localScale = new Vector3 (0.4f+vidaActual/350f, 0.4f+vidaActual/350f, 0.4f+vidaActual/350f);
		if (hitt) {
			if (anterior+0.8<=Time.time)
				hitt = false;
		}
		float aux = rb.velocity.magnitude;
		CmdVelocidad (aux);
	}

	void OnCollisionEnter(Collision colision){
		GameObject hit = colision.gameObject;
		if(isServer)
			if (hit.tag == "Player") {
				var vidaa = hit.GetComponent<vida> ();
				if (vidaa != null && velocidad >= 3f && !hitt) {
				vidaa.golpeador = gameObject;
					vidaa.recibirDanno (50);
					hitt = true;
					anterior = Time.time;
				}
			}else{
				if (/*rb.velocity.magnitude >= 4f && */hit.tag != "Suelo" && hit.tag != "Bala" && !hitt) {
					int valor = (int) (velocidad-2)*5;
					hitt = true;
					anterior = Time.time;
					golpeador = null;
					recibirDanno (5);
				}
			}
	}

	public void recibirDanno(int danno){
		if (isServer) {
			CmdQuitarvida(danno);
			if (vidaActual <= 0) {
				if (golpeador!=null)
					golpeador.GetComponent<puntuacion> ().sumapuntos (50);
				this.GetComponent<puntuacion> ().sumapuntos (-10);
				respawn ();
			}
		}
	}

	[ClientRpc]
	void RpcRespawn(){
		if(isLocalPlayer){
			Vector3 PInicial = Vector3.zero;
			Quaternion RInicial = Quaternion.Euler(0,0,0);
			spawns = FindObjectsOfType<NetworkStartPosition> ();
			if(spawns !=null && spawns.Length>0){
				int value = Random.Range (0, spawns.Length);
				PInicial = spawns [value].transform.position;
				RInicial = spawns [value].transform.rotation;
			}
			transform.position = PInicial;
			transform.rotation = RInicial;
		}
	}

	[Command]
	void CmdQuitarvida(int valor){
		vidaActual -= valor;
	}

	void CmdVelocidad(float vel){
		velocidad = vel;
	}

	public void respawn(){
		RpcRespawn ();
		vidaActual = vidaMaxima;
	}

}