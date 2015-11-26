using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	//PLAYER CONFIG
	public int jetpackMaxCharge = 3;
	public float skinOffset = 0.2f;
	public float jumpForce = 500f;

	//SOUNDS
	public AudioClip jump;
	public AudioClip emptyJetpack;

	//USER INTERFACE
	public GameObject panelJetpack;
	public GameObject imageJetpack;
	public GameObject imageCharge;
	public Text textEmptyJetpack;

	//COMPONENTS
	private Rigidbody rb;
	private AudioSource audioSource;
	private PlayerCamera playerCamera;
	private Image panelJetpackImage;
	public Image panelChargeImage;
	
	//VARIABLES
	private int jetpackCurrentCharge;
	
	void Start () {
		CheckEditorConfig ();
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
		jetpackCurrentCharge = jetpackMaxCharge;
		SetJetpackState ();

		//if (collision.gameObject.tag == "wall")
		//Camera.main.transform.localRotation = Quaternion.Euler(collision.transform.up);
		//Camera.main.transform.up = collision.transform.up;
		playerCamera.NewAxisX = collision.transform.up;
	}

	void Propulse(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(jetpackCurrentCharge > 0){
				audioSource.PlayOneShot(jump);
				rb.isKinematic = false;
				rb.AddForce(Camera.main.transform.forward * jumpForce);
				jetpackCurrentCharge--;
				SetJetpackState();
			}else{
				audioSource.PlayOneShot(emptyJetpack);
			}
		}
	}

	void SetJetpackState(){
		switch(jetpackCurrentCharge){
		case 2:

			break;
		case 1:

			break;
		case 0:
			imageJetpack.SetActive(false);
			panelJetpackImage.color = Color.red;
			textEmptyJetpack.enabled = true;
			break;
		default:
			panelJetpackImage.color = Color.white;
			imageJetpack.SetActive(true);
			textEmptyJetpack.enabled = false;
			break;
		}
	}

	void CheckEditorConfig(){
		jetpackMaxCharge = (jetpackMaxCharge < 3) ? 3 : jetpackMaxCharge;
	}

	void InitializeComponents(){
		rb = gameObject.GetComponent<Rigidbody> ();
		audioSource = gameObject.GetComponent<AudioSource> ();
		playerCamera = gameObject.GetComponent<PlayerCamera> ();
		panelJetpackImage = panelJetpack.GetComponent<Image> ();
		panelChargeImage = panelChargeImage.GetComponent<Image> ();
	}

	void InitializeVariables(){
		jetpackCurrentCharge = jetpackMaxCharge;
		playerCamera.NewAxisX = new Vector3 (0, 1, 0);
		playerCamera.NewAxisY = new Vector3 (0, 0, 1);
	}

	void InitializeUserInterface(){
		Cursor.visible = false;

		panelJetpack.GetComponent<RectTransform> ().sizeDelta = new Vector2 (jetpackMaxCharge * 60, 60);
		 
		SetJetpackState ();
	}
}
