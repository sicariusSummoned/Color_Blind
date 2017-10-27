using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/24/2017
/// Purpose: Moves player from one level to the next.
/// </summary>
public class LevelManager : MonoBehaviour
{
    #region LevelManager Members
    public string m_nextLevel;
    public float m_minDist;
    public Player[] m_players;
    #endregion

    #region LevelManager Methods
    void Update()
    {
        // Janky Solution
        // Sends player to next level
        if (PlayerNear())
        {
            SceneManager.LoadScene(m_nextLevel);
        }
    }

    public bool PlayerNear()
    {
        foreach (var player in m_players)
            if (Vector3.Distance(transform.position, player.transform.position) < m_minDist)
                return true;

        return false;
    }
    #endregion
}
