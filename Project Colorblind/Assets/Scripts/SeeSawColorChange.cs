using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is for changing the seeSaw block to change color
public class SeeSawColorChange : MonoBehaviour {

	//the starting color values for the swwSaw material.
	private float startRed;
	private float startBlue;
	private float startGreen;

	private Color changingColor;

	//the changing rate for the colors increasing or decreasing
	public float changeRate;

	//tells if the shade is being increased or decreased
	private bool increaseR;
	private bool increaseB;
	private bool increaseG;


    public PlayerColor p1;
    public PlayerColor p2;
    public PlayerColor p3;

    public SpriteRenderer spr_box;
	private Material spriteMaterial;
	//the bounding box collider for the specific obj
	private Collider2D boundingBox;
	// Use this for initialization
	void Start () {
		spriteMaterial = spr_box.material;
		startBlue = spriteMaterial.color.b;
		startGreen = spriteMaterial.color.g;
		startRed = spriteMaterial.color.r;
		boundingBox = this.gameObject.GetComponent<Collider2D>();

		increaseB = false;
		increaseG = false;
		increaseR = false;
		changingColor = spriteMaterial.color;


	}
	
	// Update is called once per frame
	void Update () {

        if(p1 != null)
        {
            addColor(p1.rgbSwitch, p1.OnSeeSaw);
        }
        if (p2 != null)
        {
            addColor(p2.rgbSwitch, p2.OnSeeSaw);
        }
        if (p3 != null)
        {
            addColor(p3.rgbSwitch, p3.OnSeeSaw);
        }

        spriteMaterial.color = changingColor;
    }

    void addColor(int color, bool addColor)
    {
      if(color == 0 && addColor == true && spriteMaterial.color.r < 255)
        {
            changingColor.r += changeRate;
            Debug.Log("red on");
        }
      else if(color==0 && addColor==false && spriteMaterial.color.r > startRed)
        {
            changingColor.r -= changeRate;
            Debug.Log("red off");
        }
      if (color == 1 && addColor == true && spriteMaterial.color.g < 255)
        {
            changingColor.g += changeRate;
            Debug.Log("green on");
        }
        else if (color == 1 && addColor == false && spriteMaterial.color.g > startGreen)
        {
            changingColor.g -= changeRate;
            Debug.Log("green off");
        }
    if (color == 2 && addColor == true && spriteMaterial.color.b < 255)
        {
            changingColor.b += changeRate;
            Debug.Log("blue on");
        }
        else if (color == 2 && addColor == false && spriteMaterial.color.b > startBlue)
        {
            changingColor.b -= changeRate;
            Debug.Log("blue off");
        }

        Debug.Log(changingColor);
    }

    
}
