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

    private GameObject[] pauses;
    private GameObject[] calibrates;
    private GameObject[] colors;


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
        current = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Escape)&& current.name != "main_menu")
        {
            Pause();
        }

        pauses = GameObject.FindGameObjectsWithTag("PauseMenu");
        calibrates = GameObject.FindGameObjectsWithTag("Calibrate");
        colors = GameObject.FindGameObjectsWithTag("ColorManager");
        if(pauses.Length > 1)
        {
            for(int i =0; i < pauses.Length; i++)
            {
                pauses[i] = null;
                pauseCanvas = pauses[0];
            }
        }
        if (calibrates.Length > 1)
        {
            for (int i = 0; i < calibrates.Length; i++)
            {
                calibrates[i] = null;
                calibrateCanvas = calibrates[0];
            }
        }
        if (colors.Length > 1)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = null;
                colorManagerGame = colors[0];
            }
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

    public void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(pauseCanvas);
        DontDestroyOnLoad(calibrateCanvas);
        DontDestroyOnLoad(colorManagerGame);

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
        Debug.Log("Quitting");
        Application.Quit();

    }

    public void MainMenu()
    {
        Debug.Log("MAIN LOADING");
        pauseCanvas.SetActive(false);
        SceneManager.LoadScene("main_menu", LoadSceneMode.Single);
    }

}
