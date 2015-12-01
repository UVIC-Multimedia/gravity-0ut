using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	//PLAYER CONFIG
	public float skinOffset = 0.2f;
	public float jumpForce = 500f;

	//SOUNDS
	public AudioClip jump;
	public AudioClip emptyJetpack;

	//USER INTERFACE
	private Jetpack jetpack;

	//COMPONENTS
	private Rigidbody rb;
	private AudioSource audioSource;
	private PlayerCamera playerCamera;
	
	//VARIABLES

	
	void Start () {
		InitializeComponents ();
		InitializeVariables ();
		InitializeUserInterface ();
	}
	
	void Update () {
		Propulse ();
		if (Input.GetKeyDown (KeyCode.K)) {
			gameObject.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
		}
	}

	void OnCollisionEnter(Collision collision) {
		rb.isKinematic = true;
		Vector3 pos = gameObject.transform.position;
		ContactPoint _contact  = collision.contacts[0];
		Vector3 dir = _contact.normal.normalized;
		pos = _contact.point + (dir * (GetComponent<SphereCollider>().radius + skinOffset));
		gameObject.transform.position = pos;
		jetpack.ResetCharge();

		//if (collision.gameObject.tag == "wall")
		//Camera.main.transform.localRotation = Quaternion.Euler(collision.transform.up);
		//Camera.main.transform.up = collision.transform.up;
		playerCamera.NewAxisX = collision.transform.up;
	}

	void Propulse(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(jetpack.GetRemainingCharge() > 0){
				audioSource.PlayOneShot(jump);
				rb.isKinematic = false;
				rb.AddForce(Camera.main.transform.forward * jumpForce);

				jetpack.ConsumeCharge();

			}else{
				audioSource.PlayOneShot(emptyJetpack);
			}
		}
	}

	void InitializeComponents(){
		rb = gameObject.GetComponent<Rigidbody> ();
		audioSource = gameObject.GetComponent<AudioSource> ();
		playerCamera = gameObject.GetComponent<PlayerCamera> ();
	}

	void InitializeVariables(){
		playerCamera.NewAxisX = new Vector3 (0, 1, 0);
		playerCamera.NewAxisY = new Vector3 (0, 0, 1);
	}

	void InitializeUserInterface(){
		Cursor.visible = false;
		jetpack = gameObject.GetComponent<Jetpack> ();
	}
}
