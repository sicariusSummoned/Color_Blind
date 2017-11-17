using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
	public Transform canvas;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {

			Pause ();
		}

	}


    public void Pause()
    {
        Debug.Log("PAUSE HAS BEEN USED");

            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;

                Debug.Log("Here");
            }
            else
            {
                Debug.Log("There");
                Time.timeScale = 1;
                canvas.gameObject.SetActive(false);
                
            }      
    }
		
	public void returnMain(){
		SceneManager.LoadScene ("main_menu", LoadSceneMode.Single);
	}
		
	public void quitting()
	{
		Application.Quit();
	}

}
