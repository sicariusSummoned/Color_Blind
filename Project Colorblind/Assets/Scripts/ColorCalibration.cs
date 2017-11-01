using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/31/2017
/// Purpose: Calibrates the correct value for each color used in-game.
/// </summary>
public class ColorCalibration : MonoBehaviour
{
    #region ColorCalibration Members
    public string m_firstLevel;
    public SpriteRenderer m_sprite;
    public Color[] m_colors;
    public Text m_text;
    private Color m_temp;
    private int m_index;
    #endregion

    #region ColorCalibration Methods
    void Start()
    {
        m_index = 0;

        m_colors[0] = ColorManager.Instance.MasterRed;
        m_colors[1] = ColorManager.Instance.MasterGreen;
        m_colors[2] = ColorManager.Instance.MasterBlue;
        m_sprite.color = m_colors[0];
        m_text.text = "\nGreen Player and Blue Player should not be able to see this." +
                      "\nRed player should be able to see this." +
                      "\nPress left and right arrow to update opacity. Press enter to confirm";
    }

    void Update()
    {
        UpdateMaterial();
        if (MaterialSet())
            NextMaterial();
    }

    private void UpdateMaterial()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_temp = m_sprite.color;
            m_temp.a -= 0.005f;
            m_sprite.color = m_temp;
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_temp = m_sprite.color;
            m_temp.a += 0.005f;
            m_sprite.color = m_temp;
            return;
        }
    }

    private bool MaterialSet()
    {
        return Input.GetKeyDown(KeyCode.Return);
    }

    private void NextMaterial()
    {
        // Update master color
        switch (m_index)
        {
            case 0:
                ColorManager.Instance.UpdateMasterRed(m_sprite.color);
                m_text.text = "\nRed Player and Blue Player should not be able to see this." +
                              "\nGreen player should be able to see this." +
                              "\nPress left and right arrow to update opacity.";
                m_sprite.color = ColorManager.Instance.MasterGreen;
                break;
            case 1:
                ColorManager.Instance.UpdateMasterGreen(m_sprite.color);
                m_text.text = "\nRed Player and Green Player should not be able to see this. " +
                              "\nBlue player should be able to see this." +
                              "\nPress left and right arrow to update opacity.";
                m_sprite.color = ColorManager.Instance.MasterBlue;
                break;
            case 2:
                ColorManager.Instance.UpdateMasterBlue(m_sprite.color);
                ColorManager.Instance.EndCalibration();
                SceneManager.LoadScene(m_firstLevel);
                break;
        }
        m_index++;
    }
    #endregion
}
