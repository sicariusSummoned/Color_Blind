using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillatemovementtest : MonoBehaviour {
    float movement = .1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0,movement,0);

        if (transform.position.y > 5)
        {
            movement = -.1f;
        }
        if (transform.position.y < -5)
        {
            movement = .1f;
        }
	}
}
