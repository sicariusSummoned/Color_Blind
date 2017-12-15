using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Player player;
    private SpriteRenderer sprite;
    private GameObject[] playersGO;
    private Player[] players;

    private float velocity;
    private Animator fillControl;
    private Animator Control;
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

    private bool dying;
    private bool dead;
    private bool justDied;
    private float deathTimer = 0f;
    private float deathTimeLimit;

    public bool NextLevel
    {
        get { return nextLevel; }
    }

    // Use this for initialization
    void Start () {

        if(gameObject.tag == "Player")
        {
            player = gameObject.GetComponent<Player>();
            Control = transform.Find("Graphics").GetComponent<Animator>();
            deathTimeLimit = gameObject.GetComponent<Player>().deathTime;
            sprite = transform.Find("Graphics").GetComponent<SpriteRenderer>();
            playersGO = GameObject.FindGameObjectsWithTag("Player");
            //for(int i = 0; i < playersGO.Length; i++)
            //{
            //    players[i] = playersGO[i].GetComponent<Player>();
            //}
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.tag == "Player")
        {
            dying = gameObject.GetComponent<Player>().Dying;
            dead = gameObject.GetComponent<Player>().Dead;
            if (!dying)
            {
                velocity = player.GetDirectionalInput().x;
                if (velocity != 0)
                {
                    Control.SetBool("Moving", true);
                }
                else if (velocity == 0)
                {
                    Control.SetBool("Moving", false);
                }
            }
            else
            {
                //if (!justdied)
                //{
                //    for (int i = 0; i < players.length; i++)
                //    {
                //        players[i].dying = true;
                //    }
                //    justdied = true;
                //}

                Control.SetBool("Moving", false);
                Control.SetBool("Dead", true);
                deathTimer += Time.deltaTime;
                if(deathTimer <= deathTimeLimit)
                {
                    float spriteLerp = Mathf.Lerp(0f, 1f, deathTimer);
                    sprite.color = new Color(1f, 1f, 1f, spriteLerp);

                }
                Debug.Log(deathTimer);
            }
            if(dead)
            {
                Control.SetBool("Dead", false);
                justDied = false;

                deathTimer = 0f;
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