using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour{
	
	public Vector3 NewAxisX { get; set;}
	public Vector3 NewAxisY { get; set;}

	void Update(){
		
		float deltaXMouse = Input.GetAxis ("Mouse X");
		float deltaYMouse = Input.GetAxis ("Mouse Y");
		
		transform.Rotate (NewAxisX, deltaXMouse);
		//transform.Rotate (NewAxisY, deltaYMouse);
		//transform.Rotate (new Vector3 (NewAxis.x + deltaXMouse, NewAxis.y + deltaYMouse, 0f));
	}

}
