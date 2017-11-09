using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public Canvas warningMenu;
    public Canvas mainMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //clicking the understand button to go to main menu
    public void ToMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        warningMenu.gameObject.SetActive(false);
    }

    public void play()
    {
        SceneManager.LoadScene("1_1", LoadSceneMode.Single);
    }

    public void quitting()
    {
        Application.Quit();
    }
}
