using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/4/2017
/// Purpose: Respawns a player at its position if one dies.
/// </summary>
public class CheckPointSystem : MonoBehaviour
{
    #region CheckPointSystem Members
    public Player m_player1;
    public Player m_player2;
    public Player m_player3;
    #endregion

    #region CheckPointSystem Methods
    void Update()
    {
        // Janky Solution
        // If a player is dead then they respawn at this checkpoints position
        RespawnIfDead();
    }

    public void RespawnIfDead()
    {
        if (m_player1.Dead)
            m_player1.Respawn(transform.position);
        if (m_player2.Dead)
            m_player2.Respawn(transform.position);
        if (m_player3.Dead)
            m_player3.Respawn(transform.position);
    }
    #endregion
}
