using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public static GameObject colorManager;
    private Scene current;
    public static GameObject InstancePause;
    public static GameObject InstanceCalibrate;
    public GameObject pauseCanvas;
    public GameObject calibrateCanvas;
    public GameObject colorManagerGame;


    public void Start()
    {
        // Destroy if there are multiple instances
       // if (InstancePause != null && InstancePause != this)
           // Destroy(InstancePause.gameObject);
        if (InstanceCalibrate != null && InstanceCalibrate != this)
            Destroy(InstanceCalibrate.gameObject);

        colorManager = colorManagerGame;
        InstancePause = pauseCanvas;
        InstanceCalibrate = calibrateCanvas;
    }
    void Update()
    {
        current = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Escape)&& current.name != "main_menu")
        {
            Pause();
        }
    }


    public void Pause()
    {
        if (InstancePause.activeInHierarchy == false)
        {
            InstancePause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            InstancePause.SetActive(false);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
        //DontDestroyOnLoad(InstancePause);
        DontDestroyOnLoad(InstanceCalibrate);
        DontDestroyOnLoad(colorManager);

    }
    public void ActivateCalibration()
    {
        InstanceCalibrate.SetActive(true);
        InstancePause.SetActive(false);
    }

    public void DectivateCalibration()
    {
        InstanceCalibrate.SetActive(false);
        InstancePause.SetActive(true);
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
