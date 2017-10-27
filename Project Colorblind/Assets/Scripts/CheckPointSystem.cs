using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/4/2017
/// Purpose: Respawns a player at its position if one dies.
/// </summary>
public class CheckPointSystem : MonoBehaviour
{
    #region CheckPointSystem Members
    public Player[] m_players;
    public GhostPlayer[] m_ghostPlayers;

    [Header("FILL IN ORDER OF CHECKPOINT PROGRESSION")]
    public Waypoint[] m_waypoints;
    private int m_waypointIndex;
    #endregion

    #region CheckPointSystem Methods
    void Update()
    {
        // Update most recent waypoint if we passed it
        UpdateWaypoints();

        // Janky Solution
        // If a player is dead then they respawn at the most recent waypoint
        RespawnIfDead();
    }

    private void UpdateWaypoints()
    {
        if (m_waypointIndex + 1 < m_waypoints.Length &&
            m_waypoints[m_waypointIndex + 1].Passed)
            m_waypointIndex++;
    }

    private void RespawnIfDead()
    {
        foreach (var player in m_players)
            if (player.Dead)
                RespawnPlayers();

        foreach (var ghostPlayer in m_ghostPlayers)
            if (ghostPlayer.Dead)
                RespawnPlayers();
    }

    private void RespawnPlayers()
    {
        foreach (var player in m_players)
            player.Respawn(m_waypoints[m_waypointIndex].transform.position);

        foreach (var ghostPlayer in m_ghostPlayers)
            ghostPlayer.Respawn(m_waypoints[m_waypointIndex].transform.position);
    }
    #endregion
}
