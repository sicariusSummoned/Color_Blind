using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject calibrationCanvas;
    
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
        calibrationCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void DectivateCalibration()
    {
        calibrationCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void Quitting()
    {
        Debug.Log("Quitting");
        Application.Quit();

    }

    public void MainMenu()
    {
        Debug.Log("MAIN LOADING");
        SceneManager.LoadScene("main_menu", LoadSceneMode.Single);
    }

}
