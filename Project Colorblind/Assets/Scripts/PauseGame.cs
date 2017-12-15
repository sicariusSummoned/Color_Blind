using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    //public static GameObject colorManagerGame;
    private Scene current;
   // public static GameObject pauseCanvas;
    //public static GameObject calibrateCanvas;
    public GameObject pauseCanvas;
    public GameObject calibrateCanvas;
    public GameObject colorManagerGame;
	public GameObject controlls;
	public GameObject pauseMenu;



    public void Start()
    {
        // Destroy if there are multiple instances
       // if (pauseCanvas != null && pauseCanvas != this)
           // Destroy(pauseCanvas.gameObject);
       // if (calibrateCanvas != null && calibrateCanvas != this)
            //Destroy(calibrateCanvas.gameObject);


    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
			
    }


    public void Pause()
    {
        if (pauseCanvas.activeInHierarchy == false)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
    }
		

    public void ActivateCalibration()
    {
        calibrateCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void DectivateCalibration()
    {
        calibrateCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void Quitting()
    {
        
        Application.Quit();

    }

    public void MainMenu()
    {
       
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("main_menu", LoadSceneMode.Single);
    }

	public void ControllMenu()
	{
		controlls.SetActive (true);
		pauseMenu.SetActive (false);
	}

	public void returnControlls(){
		controlls.SetActive (false);
		pauseMenu.SetActive (true);
	}

}
