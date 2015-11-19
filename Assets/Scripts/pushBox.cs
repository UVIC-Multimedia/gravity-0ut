using UnityEngine;
using System.Collections;

public class pushBox : MonoBehaviour {

	public float force = 1000.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.P)) {
			Vector3 dir = Vector3.Cross(gameObject.transform.up, gameObject.transform.forward);

			gameObject.GetComponent<Rigidbody>().AddForce(dir*force);
		}
	}
}
