using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Jetpack : MonoBehaviour {

	public GameObject panel;
	private Image panelImage;
	private RectTransform panelRect;

	public GameObject icon;
	private Image iconImage;

	public GameObject charge;
	private Image chargeImage;
	private RectTransform chargeRect;

	public Text textEmpty;

	public int maxCharge = 3;
	private int remainingCharge;

	void Start(){

		maxCharge = (maxCharge < 3) ? 3 : maxCharge;

		panel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (maxCharge * 60, 60);
		panelImage = panel.GetComponent<Image> ();

		chargeImage = charge.GetComponent<Image> ();
		chargeRect = charge.GetComponent<RectTransform> ();

		ResetCharge ();
	}

	public void ResetCharge(){
		remainingCharge = maxCharge;
		panelImage.color = Color.white;
		chargeImage.color = Color.green;
		charge.SetActive (true);
		icon.SetActive(true);
		textEmpty.enabled = false;
		UpdateJetpack ();
	}

	public void ConsumeCharge(){
		remainingCharge--;
		UpdateJetpack ();
	}

	public void UpdateJetpack(){
		if (remainingCharge > 0) {
			chargeRect.sizeDelta = new Vector2(remainingCharge * 60, 60);
		} else {
			icon.SetActive(false);
			panelImage.color = Color.red;
			charge.SetActive(false);
			textEmpty.enabled = true;
		}
	}

	public int GetRemainingCharge(){
		return remainingCharge;
	}
}
