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

    private bool levelJustWon = true;
    private bool levelWon = false;
    private PlayerAnimation[] playerAnims;
    private float winDelay;
    private float elapsedTime = 0;
    #endregion

    #region LevelManager Methods

    public void Start()
    {
        Transform playersTrans = GameObject.Find("Players").transform;

        playerAnims = playersTrans.GetComponentsInChildren<PlayerAnimation>();

        winDelay = playerAnims[0].moveDelay + playerAnims[0].rotDelay + .1f;
    }

    void Update()
    {
        // Janky Solution
        // Sends player to next level
        if (!levelWon)
        {
            if (PlayerNear())
            {
                levelWon = true;
            }
        }
        else
        {
            if (levelJustWon)
            {
                for (int i = 0; i < 3; i++)
                {
                    playerAnims[i].wonLevel = true;
                }
                levelJustWon = false;
            }

            if (playerAnims[0].NextLevel)
            {
                SceneManager.LoadScene(m_nextLevel);
            }
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
