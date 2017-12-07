using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public GameObject warningMenu;
    public GameObject mainMenu;
    public GameObject selectMenu;
    public GameObject creditMenu;

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
        creditMenu.gameObject.SetActive(false);
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
    public void level21()
    {
        SceneManager.LoadScene("2_1", LoadSceneMode.Single);
    }

    public void level22()
    {
        SceneManager.LoadScene("2_2", LoadSceneMode.Single);
    }
    public void level23()
    {
        SceneManager.LoadScene("2_3", LoadSceneMode.Single);
    }
    public void level24()
    {
        SceneManager.LoadScene("2_4", LoadSceneMode.Single);
    }
    public void level25()
    {
        SceneManager.LoadScene("2_5", LoadSceneMode.Single);
    }
    public void level26()
    {
        SceneManager.LoadScene("2_6", LoadSceneMode.Single);
    }
    public void level31()
    {
        SceneManager.LoadScene("3_1", LoadSceneMode.Single);
    }
    public void level32()
    {
        SceneManager.LoadScene("3_2", LoadSceneMode.Single);
    }
    public void level33()
    {
        SceneManager.LoadScene("3_3", LoadSceneMode.Single);
    }
    public void level34()
    {
       // SceneManager.LoadScene("3_4", LoadSceneMode.Single);
    }
    public void level35()
    {
        //SceneManager.LoadScene("3_5", LoadSceneMode.Single);
    }
    public void level36()
    {
      //  SceneManager.LoadScene("3_6", LoadSceneMode.Single);
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
        creditMenu.gameObject.SetActive(false);
    }

    public void toCredits()
    {
        warningMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        selectMenu.gameObject.SetActive(false);
        creditMenu.gameObject.SetActive(true);
    }

}
