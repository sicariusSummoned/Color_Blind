using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/31/2017
/// Purpose: Super terrible script that skips to a level for testing.
/// </summary>
public class LevelSkipper : MonoBehaviour
{
    #region ColorCalibration Members
    private string m_level;
    #endregion

    #region ColorCalibration Methods
    void Update()
    {
        if (NumPressed())
            GoToLevel();
    }

    private bool NumPressed()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_level = "1_1";
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_level = "1_2";
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_level = "1_3";
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_level = "1_4";
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_level = "1_5";
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            m_level = "1_6";
            return true;
        }

        return false;
    }

    private void GoToLevel()
    {
        SceneManager.LoadScene(m_level);
    }
    #endregion
}
