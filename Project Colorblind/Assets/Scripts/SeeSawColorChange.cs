using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is for changing the seeSaw block to change color
public class SeeSawColorChange : MonoBehaviour {

	//the starting color values for the swwSaw material.
	private float startdRed;
	private float startBlue;
	private float startGreen;

	private Color changingColor;

	//the changing rate for the colors increasing or decreasing
	public float changeRate;

	//tells if the shade is being increased or decreased
	private bool increaseR;
	private bool increaseB;
	private bool increaseG;

	public SpriteRenderer spr_box;
	private Material spriteMaterial;
	//the bounding box collider for the specific obj
	private Collider2D boundingBox;
	// Use this for initialization
	void Start () {
		spriteMaterial = spr_box.material;
		startBlue = spriteMaterial.color.b;
		startGreen = spriteMaterial.color.g;
		startdRed = spriteMaterial.color.r;
		boundingBox = this.gameObject.GetComponent<Collider2D>();

		increaseB = false;
		increaseG = false;
		increaseR = false;
		changingColor = spriteMaterial.color;
	}
	
	// Update is called once per frame
	void Update () {

		if (increaseB == true && spriteMaterial.color.b < 255) {
			changingColor.b+= changeRate;
		}
		else if (increaseB == false && spriteMaterial.color.b > startBlue) {
			changingColor.b-= changeRate;
		}
		spriteMaterial.color = changingColor;
		
	}

	//if the collider stays on the platform
	void OnTriggerStay2D(Collider2D coll){
		Debug.Log (coll);
		if (coll.gameObject.GetComponent<PlayerColor>()!= null) {
			if (coll.gameObject.GetComponent<PlayerColor> ().red == true) {
				increaseR = true;
			}
			if (coll.gameObject.GetComponent<PlayerColor> ().blue == true) {
				increaseB = true;
				Debug.Log ("blue collided");
			}
			if (coll.gameObject.GetComponent<PlayerColor> ().green == true) {
				increaseG = true;
			}

		}

	}

	//if the collider stays on the platform
	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.GetComponent<PlayerColor>()!= null) {
			if (coll.gameObject.GetComponent<PlayerColor> ().red == true) {
				increaseR = false;
			}
			if (coll.gameObject.GetComponent<PlayerColor> ().blue == true) {
				increaseB = false;
				Debug.Log ("blue left");
			}
			if (coll.gameObject.GetComponent<PlayerColor> ().green == true) {
				increaseG = false;
			}

		}

	}







}
