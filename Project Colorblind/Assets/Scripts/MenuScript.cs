using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public Canvas warningMenu;
    public Canvas mainMenu;
    public Canvas selectMenu;

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
        selectMenu.gameObject.SetActive(false);
    }

    public void play()
    {
        SceneManager.LoadScene("1_1", LoadSceneMode.Single);
    }

    public void level2()
    {
        SceneManager.LoadScene("1_2", LoadSceneMode.Single);
    }
    public void level3()
    {
        SceneManager.LoadScene("1_3", LoadSceneMode.Single);
    }
    public void level4()
    {
        SceneManager.LoadScene("1_4", LoadSceneMode.Single);
    }
    public void level5()
    {
        SceneManager.LoadScene("1_5", LoadSceneMode.Single);
    }
    public void level6()
    {
        SceneManager.LoadScene("1_6", LoadSceneMode.Single);
    }
    public void level7()
    {
        SceneManager.LoadScene("1_7", LoadSceneMode.Single);
    }
    public void level8()
    {
        SceneManager.LoadScene("1_8", LoadSceneMode.Single);
    }
    public void level9()
    {
        SceneManager.LoadScene("1_9", LoadSceneMode.Single);
    }
    public void quitting()
    {
        Application.Quit();
    }

    public void levelSelect()
    {
        warningMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        selectMenu.gameObject.SetActive(true);
    }
}
