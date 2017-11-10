using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Player player;

    private float velocity;
    private Animator fillControl;
    private Animator outControl;
    private float lastlastVelocity;

    private bool justWonLevel = false;
    public bool wonLevel = false;
    private bool nextLevel = false;
    public float moveDelay = 2.0f;
    private float currentWinTime;
    private Vector3 doorPos;
    private Vector3 prevPos;
    public float rotDelay = 1.0f;
    private float rotTimer;

    public bool NextLevel
    {
        get { return nextLevel; }
    }

    // Use this for initialization
    void Start () {

        if(gameObject.tag == "Player")
        {
            player = gameObject.GetComponent<Player>();
            fillControl = transform.Find("Fill").GetComponent<Animator>();
            outControl = transform.Find("Outline").GetComponent<Animator>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.tag == "Player")
        {
            velocity = player.GetDirectionalInput().x;
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

        if(wonLevel)
        {
            LevelWin();
        }
	}

    void LevelWin()
    {
        if (!justWonLevel)
        {
            justWonLevel = true;
            doorPos = GameObject.Find("LevelManager").transform.position;
            prevPos = transform.position;
            if(gameObject.tag == "Player")
            {
                GetComponent<PlayerInput>().enabled = false;
                GetComponent<Player>().enabled = false;
                GetComponent<Controller2D>().enabled = false;
            }
            else
            {
                GetComponent<GhostPlayerInput>().enabled = false;
                GetComponent<GhostPlayer>().enabled = false;
            }
        }
        
        currentWinTime += Time.deltaTime;

        if(currentWinTime >= moveDelay)
        {
            transform.Rotate(new Vector3(0, 0, 1), currentWinTime * 5);
            transform.localScale.Scale(new Vector3(currentWinTime * .5f, currentWinTime * .5f, currentWinTime * .5f));
        }

        if (currentWinTime >= moveDelay + rotDelay)
        {
            nextLevel = true;
        }

        float currentMove = currentWinTime / moveDelay;

        transform.position = Vector3.Lerp(prevPos, doorPos, currentMove);
    }
}