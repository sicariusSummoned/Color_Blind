using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 9/28/2017
/// Purpose: A singleton that holds all of the global color information.
/// </summary>
public class ColorManager : MonoBehaviour
{
    #region ColorManager Members
    public static ColorManager Instance;

    private Color m_defaultRed =   new Color(1,    0, 0,    0.5f);
    private Color m_defaultGreen = new Color(0.1f, 1, 0.1f, 0.3f);
    private Color m_defaultBlue =  new Color(0.2f, 0, 1,    0.3f);

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

    #region ColorManager Methods
    private void Awake()
    {
        // Destroy if there are multiple instances
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        // Stay persistent between levels
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        m_masterRed = m_defaultRed;
        m_masterGreen = m_defaultGreen;
        m_masterBlue = m_defaultBlue;
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
    #endregion
}
