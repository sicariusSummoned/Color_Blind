using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private float velocity;
    private Player player;
    private Animator fillControl;
    private Animator outControl;
    private float lastlastVelocity;

    // Use this for initialization
    void Start () {
        player = gameObject.GetComponent<Player>();
        fillControl = transform.Find("Fill").GetComponent<Animator>();
        outControl = transform.Find("Outline").GetComponent<Animator>();
        Debug.Log(fillControl);
        Debug.Log(outControl);
    }
	
	// Update is called once per frame
	void Update () {
        velocity = player.GetDirectionalInput().x;

        if(gameObject.name == "Red Player")
            Debug.Log(velocity);

        if (velocity != 0)
        {
            fillControl.SetBool("Moving", true);
            outControl.SetBool("Moving", true);
        }
        else if (velocity == 0)
        {
            fillControl.SetBool("Moving", false);
            outControl.SetBool("Moving", false);
        }
	}
}
