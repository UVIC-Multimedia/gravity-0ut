using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpForce = 500f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.isKinematic = false;
			rb.AddForce(Camera.main.transform.forward * jumpForce);
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		Debug.Log ("Hem col·lisionat amb: " + collision.gameObject.tag);
		rb.isKinematic = true;
		//if (collision.gameObject.tag == "wall")
		Camera.main.transform.localRotation = Quaternion.Euler(collision.transform.up);
	}
}
