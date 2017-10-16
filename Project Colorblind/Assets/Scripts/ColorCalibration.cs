using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/15/2017
/// Purpose: Calibrates the correct value for each material used in-game.
/// </summary>
public class ColorCalibration : MonoBehaviour
{
    #region ColorCalibration Members
    public string m_firstLevel;
    public Renderer m_cubeRenderer;
    public Material[] m_materials;
    private Material m_current;
    private Color m_temp;
    private int m_index;
    #endregion

    #region ColorCalibration Methods
    void Start()
    {
        m_index = 0;
        m_current = m_materials[0];

        m_materials[0].color = ColorManager.Instance.MasterRed;
        m_materials[1].color = ColorManager.Instance.MasterGreen;
        m_materials[2].color = ColorManager.Instance.MasterBlue;
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
            m_temp = m_current.color;
            m_temp.a -= 0.005f;
            m_current.color = m_temp;
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_temp = m_current.color;
            m_temp.a += 0.005f;
            m_current.color = m_temp;
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
                ColorManager.Instance.UpdateMasterRed(m_current.color);
                break;
            case 1:
                ColorManager.Instance.UpdateMasterGreen(m_current.color);
                break;
            case 2:
                ColorManager.Instance.UpdateMasterBlue(m_current.color);
                break;
            default:
                ColorManager.Instance.EndCalibration();
                SceneManager.LoadScene(m_firstLevel);
                return;
        }

        m_index++;
        if (m_index >= m_materials.Length)
            return;

        m_current = m_materials[m_index];
        m_cubeRenderer.material = m_current;
    }
    #endregion
}
