using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centroid : MonoBehaviour {

    // Use this for initialization
    private Vector3 centroid;

    public Camera mainCamera;

    public Vector3 Centroids
    {
        get { return centroid; }
    }
    public int numPlayers;

    private GameObject[] players;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        players = GameObject.FindGameObjectsWithTag("Player");
        CalcCentroid();
        this.transform.position = centroid;
    
	}


    private void CalcCentroid()
    {
        float x = 0;
        float y = 0;

        for(int i=0; i < players.Length; i++ )
        {
            x += players[i].transform.position.x;
            y += players[i].transform.position.y;
        }
        numPlayers = players.Length;
        x = x / numPlayers;
        y = y / numPlayers;

        centroid = new Vector3(x, y, -10);

    }



}
