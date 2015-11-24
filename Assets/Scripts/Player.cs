using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float skinOffset = 0.2f;
	public float jumpForce = 500f;
	//SOUNDS
	public AudioClip jump;

	//COMPONENTS
	private Rigidbody rb;
	private AudioSource audioSource;
	private PlayerCamera playerCamera;
	
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		audioSource = gameObject.GetComponent<AudioSource> ();
		playerCamera = gameObject.GetComponentInChildren<PlayerCamera> (); 
		playerCamera.NewAxisX = new Vector3 (0, 1, 0);
		playerCamera.NewAxisY = new Vector3 (0, 0, 1);
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			audioSource.PlayOneShot(jump);
			rb.isKinematic = false;
			rb.AddForce(Camera.main.transform.forward * jumpForce);
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			gameObject.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
			Debug.Log(contact.point);
			Debug.Log(contact.normal);
		}
		Debug.Log ("Hem col·lisionat amb: " + collision.gameObject.tag);
		rb.isKinematic = true;
		Vector3 pos = gameObject.transform.position;
		ContactPoint _contact  = collision.contacts[0];
		Vector3 dir = _contact.normal.normalized;
		pos = _contact.point + (dir * (GetComponent<SphereCollider>().radius + skinOffset));
		gameObject.transform.position = pos;


		//if (collision.gameObject.tag == "wall")
		//Camera.main.transform.localRotation = Quaternion.Euler(collision.transform.up);
		//Camera.main.transform.up = collision.transform.up;
		playerCamera.NewAxisX = collision.transform.up;
	}
}
