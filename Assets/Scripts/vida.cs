using UnityEngine;
using UnityEngine.Networking;

public class vida : NetworkBehaviour
{
	Rigidbody rb;
	public bool hitt;

	public float vidaMaxima;
	public float anterior;
	public float velocidad;
	[SyncVar]
	public float vidaActual;


	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		vidaActual = vidaMaxima;
		hitt = false;
	}

	void FixedUpdate(){
		transform.localScale = new Vector3 (0.3f+vidaActual/200f, 0.3f+vidaActual/200f, 0.3f+vidaActual/200f);
		if (hitt) {
			if (anterior+0.8<=Time.time)
				hitt = false;
		}
		velocidad = rb.velocity.magnitude;
	}

	void OnCollisionEnter(Collision colision){
		GameObject hit = colision.gameObject;
		if(isServer)
			if (hit.tag == "Player") {
				var vidaa = hit.GetComponent<vida> ();
				if (vidaa != null && velocidad >= 3f && !hitt) {
					vidaa.recibirDanno (50);
					hitt = true;
					anterior = Time.time;
				}
			}else{
				if (/*rb.velocity.magnitude >= 4f && */hit.tag != "Suelo" && hit.tag != "Bala" && !hitt) {
					int valor = (int) (velocidad-2)*5;
					hitt = true;
					anterior = Time.time;
					if (valor>0)
						recibirDanno (valor);
				}
			}
	}

	public void recibirDanno(int danno){
		if (isServer) {
			CmdQuitarvida(danno);
			if (vidaActual <= 0) {
				RpcRespawn ();
				vidaActual = vidaMaxima;
			}
		}
	}

	[ClientRpc]
	void RpcRespawn(){
		if(isLocalPlayer){
			transform.position = Vector3.zero;

		}
	}

	[Command]
	void CmdQuitarvida(int valor){
		vidaActual -= valor;
	}

}