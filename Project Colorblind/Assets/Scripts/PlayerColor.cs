using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {

    //tells which color it is
    //0 = red
    //1 = g
    //2 = b
    public int rgbSwitch;

    //tells if the player is on the see saw
    private bool onSeesaw;
	// Use this for initialization
	void Start () {
        onSeesaw = false;
	}
	
    public bool OnSeeSaw
    {
        get { return onSeesaw; }
        set { onSeesaw = value; }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
