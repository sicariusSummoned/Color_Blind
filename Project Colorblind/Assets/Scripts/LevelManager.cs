//Code based on Daniel Wood's Unity 2D Game Design Youtube tutorials

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public float respawnDelay;
    public Player gamePlayer;

	// Use this for initialization
	void Start () {
        gamePlayer = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Respawn()
    {
        gamePlayer.gameObject.SetActive(false);
        gamePlayer.transform.position = gamePlayer.respawnPoint;
        gamePlayer.gameObject.SetActive(true);

    }
}
