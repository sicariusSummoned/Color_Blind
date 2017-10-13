using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMultiplePlayers : MonoBehaviour {

    public Transform player1, player2, player3;
    public float minSizeY = 5f;
    public float maxSizeY = 15f;


   
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        SetCameraPos();
        SetCameraSize();
	}

    void SetCameraPos()
    {
        Vector3 middle = (player1.position + player2.position + player3.position) * 0.5f;

        Camera.main.transform.position = new Vector3(middle.x, middle.y, Camera.main.transform.position.z);
    }

    void SetCameraSize()
    {
        //horizontal size based on screen ratio
        float minSizeX = minSizeY * Screen.width / Screen.height;

        //multiplying by 0.5, because the orthographicSize is half the height
        float width = Mathf.Abs(player1.position.x - player2.position.x - player3.position.x) * 0.5f;
        float height = Mathf.Abs(player1.position.y - player2.position.y - player3.position.y) * 0.5f;

        float camSizeX = Mathf.Max(width, minSizeX);
        Camera.main.orthographicSize = Mathf.Max(height, camSizeX * Screen.height / Screen.width, minSizeY);
        Camera.main.orthographicSize = Mathf.Clamp(Mathf.Max(height, camSizeX * Screen.height / Screen.width, minSizeY), minSizeY, maxSizeY);
    }
}
