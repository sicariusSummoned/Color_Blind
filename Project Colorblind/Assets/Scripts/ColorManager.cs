using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/15/2017
/// Purpose: A singleton that holds all of the global color information.
/// </summary>
public class ColorManager : MonoBehaviour
{
    #region ColorManager Members
    #region Color Members
    public static ColorManager Instance;

    private Color m_defaultRed =   new Color(1,    0, 0,    0.3f);
    private Color m_defaultGreen = new Color(0.5f, 1, 0.1f, 0.2f);
    private Color m_defaultBlue =  new Color(0.1f, 0, 0.5f, 0.3f);

    private Color m_masterRed;
    private Color m_masterGreen;
    private Color m_masterBlue;

    public Color MasterRed
    {
        get { return m_masterRed; }
    }
    public Color MasterGreen
    {
        get { return m_masterGreen; }
    }
    public Color MasterBlue
    {
        get { return m_masterBlue; }
    }
    #endregion

    private List<SpriteRenderer> m_redSprites;
    private List<SpriteRenderer> m_greenSprites;
    private List<SpriteRenderer> m_blueSprites;
    #endregion

    #region ColorManager Methods
    private void Awake()
    {
        // Destroy if there are multiple instances
        if (Instance != null && Instance != this)
            //Destroy(gameObject);

        // Stay persistent between levels
        DontDestroyOnLoad(gameObject);
        Instance = this;

        m_redSprites = new List<SpriteRenderer>();
        m_greenSprites = new List<SpriteRenderer>();
        m_blueSprites = new List<SpriteRenderer>();

        // Get saved color values or just use defaults
        if (PlayerPrefs.HasKey("rr") == false)
            m_masterRed = m_defaultRed;
        else GetRed();

        if (PlayerPrefs.HasKey("gr") == false)
            m_masterGreen = m_defaultGreen;
        else GetGreen();

        if (PlayerPrefs.HasKey("br") == false)
            m_masterBlue = m_defaultBlue;
        else GetBlue();
    }

    private void Start()
    {
        // Red Objects
        foreach (var red in GameObject.FindGameObjectsWithTag("Red"))
        {
            m_redSprites.Add(red.GetComponent<SpriteRenderer>());
            m_redSprites[m_redSprites.Count - 1].color = MasterRed;
        }

        foreach (var red in GameObject.FindGameObjectsWithTag("RedKillsPlayer"))
        {
            m_redSprites.Add(red.GetComponent<SpriteRenderer>());
            m_redSprites[m_redSprites.Count - 1].color = MasterRed;
        }

        // Green Objects
        foreach (var green in GameObject.FindGameObjectsWithTag("Green"))
        {
            m_greenSprites.Add(green.GetComponent<SpriteRenderer>());
            m_greenSprites[m_greenSprites.Count - 1].color = MasterGreen;
        }

        foreach (var green in GameObject.FindGameObjectsWithTag("GreenKillsPlayer"))
        {
            m_greenSprites.Add(green.GetComponent<SpriteRenderer>());
            m_greenSprites[m_greenSprites.Count - 1].color = MasterGreen;
        }

        // Blue Objects
        foreach (var blue in GameObject.FindGameObjectsWithTag("Blue"))
        {
            m_blueSprites.Add(blue.GetComponent<SpriteRenderer>());
            m_blueSprites[m_blueSprites.Count - 1].color = MasterBlue;
        }

        foreach (var blue in GameObject.FindGameObjectsWithTag("BlueKillsPlayer"))
        {
            m_blueSprites.Add(blue.GetComponent<SpriteRenderer>());
            m_blueSprites[m_blueSprites.Count - 1].color = MasterBlue;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("rr");
        PlayerPrefs.DeleteKey("rg");
        PlayerPrefs.DeleteKey("rb");
        PlayerPrefs.DeleteKey("ra");

        PlayerPrefs.DeleteKey("gr");
        PlayerPrefs.DeleteKey("gg");
        PlayerPrefs.DeleteKey("gb");
        PlayerPrefs.DeleteKey("ga");

        PlayerPrefs.DeleteKey("gr");
        PlayerPrefs.DeleteKey("gg");
        PlayerPrefs.DeleteKey("gb");
        PlayerPrefs.DeleteKey("ga");
    }

    public void UpdateMasterRed(Color c)
    {
        m_masterRed = c;
    }

    public void UpdateMasterGreen(Color c)
    {
        m_masterGreen = c;
    }

    public void UpdateMasterBlue(Color c)
    {
        m_masterBlue = c;
    }

    public void GetRed()
    {
        m_masterRed = new Color(PlayerPrefs.GetFloat("rr"),
                                PlayerPrefs.GetFloat("rg"),
                                PlayerPrefs.GetFloat("rb"),
                                PlayerPrefs.GetFloat("ra"));
    }

    public void GetGreen()
    {
        m_masterGreen = new Color(PlayerPrefs.GetFloat("gr"),
                                  PlayerPrefs.GetFloat("gg"),
                                  PlayerPrefs.GetFloat("gb"),
                                  PlayerPrefs.GetFloat("ga"));
    }

    public void GetBlue()
    {
        m_masterBlue = new Color(PlayerPrefs.GetFloat("br"),
                                 PlayerPrefs.GetFloat("bg"),
                                 PlayerPrefs.GetFloat("bb"),
                                 PlayerPrefs.GetFloat("ba"));
    }

    public void EndCalibration()
    {
        // Set red preferences
        PlayerPrefs.SetFloat("rr", m_masterRed.r);
        PlayerPrefs.SetFloat("rg", m_masterRed.g);
        PlayerPrefs.SetFloat("rb", m_masterRed.b);
        PlayerPrefs.SetFloat("ra", m_masterRed.a);

        // Set green preferences
        PlayerPrefs.SetFloat("gr", m_masterGreen.r);
        PlayerPrefs.SetFloat("gg", m_masterGreen.g);
        PlayerPrefs.SetFloat("gb", m_masterGreen.b);
        PlayerPrefs.SetFloat("ga", m_masterGreen.a);

        // Set blue preferences
        PlayerPrefs.SetFloat("br", m_masterBlue.r);
        PlayerPrefs.SetFloat("bg", m_masterBlue.g);
        PlayerPrefs.SetFloat("bb", m_masterBlue.b);
        PlayerPrefs.SetFloat("ba", m_masterBlue.a);

        PlayerPrefs.Save();
    }
    #endregion
}
