using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLaser : MonoBehaviour {
    public enum PLAYER_COLOR { Red, Green, Blue };
    public PLAYER_COLOR myColor;

    private string laserButton = "_Laser";
    private string lookHorizAxis = "_Look_Horizontal";
    private string lookVertAxis = "_Look_Vertical";

    private Transform myTransform;
    private Vector2 targetVector;
    private Vector2 startVector = Vector2.up;

    private Quaternion myRotation;
    private Quaternion targetRotation;

    public float rotationSpeed = 150.0f;

    private bool wasDown = false;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
