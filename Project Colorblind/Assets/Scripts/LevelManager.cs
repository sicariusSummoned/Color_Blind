using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/4/2017
/// Purpose: Moves player from one level to the next.
/// </summary>
public class LevelManager : MonoBehaviour
{
    #region LevelManager Members
    public string m_nextLevel;
    public float m_minDist;
    public Player m_player1;
    public Player m_player2;
    public Player m_player3;
    #endregion

    #region LevelManager Methods
    void Update()
    {
        // Janky Solution
        // Checks if a player is nearby during keypress
        // Sends player to next level
        if (Input.GetKey(KeyCode.Space) && PlayerNear())
        {
            SceneManager.LoadScene(m_nextLevel);
        }
    }

    public bool PlayerNear()
    {
        return (Vector3.Distance(transform.position, m_player1.transform.position) < m_minDist ||
                Vector3.Distance(transform.position, m_player2.transform.position) < m_minDist ||
                Vector3.Distance(transform.position, m_player3.transform.position) < m_minDist);
    }
    #endregion
}
