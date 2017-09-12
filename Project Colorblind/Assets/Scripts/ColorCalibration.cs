using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 9/3/2017
/// Purpose: Calibrates the correct value for each material used in-game.
/// </summary>
public class ColorCalibration : MonoBehaviour
{
    #region ColorCalibration Members
    public Renderer m_cubeRenderer;
    public Material[] m_materials;
    private Material m_current;
    private Color m_temp;           // Saves memory
    private int m_index;

    #region Default Color Values
    private const float RedOpacity = 0.9f;
    private const float GreenOpacity = 0.125f;
    private const float BlueOpacity = 0.25f;
    #endregion
    #endregion

    #region ColorCalibration Methods
    void Start()
    {
        m_index = 0;
        m_current = m_materials[0];

        SetOpacity(m_materials[0], RedOpacity);
        SetOpacity(m_materials[1], GreenOpacity);
        SetOpacity(m_materials[2], BlueOpacity);
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
            m_temp.a -= 0.01f;
            m_current.color = m_temp;
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_temp = m_current.color;
            m_temp.a += 0.01f;
            m_current.color = m_temp;
            return;
        }
    }

    private bool MaterialSet()
    {
        return Input.GetKeyDown(KeyCode.Return) && m_index < m_materials.Length;
    }

    private void NextMaterial()
    {
        m_index++;
        m_current = m_materials[m_index];
        m_cubeRenderer.material = m_current;
    }

    private void SetOpacity(Material material, float opacity)
    {
        m_temp = material.color;
        m_temp.a = opacity;
        material.color = m_temp;
    }
    #endregion
}
