using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 12/2/2017
/// Purpose: Respawns a player at its position if one dies.
/// </summary>
public class CheckPointSystem : MonoBehaviour
{
    #region CheckPointSystem Members
    public Player m_redPlayer;
    public Player m_greenPlayer;
    public Player m_bluePlayer;

    // Ghosts all spawn in the same spot
    // Ghosts currently try to spawn at red, then green, then blue
    // They spawn at whichever one is possible in that order first
    public GhostPlayer[] m_ghostPlayers;
    private bool m_ghostsRespawned;

    [Header("NOTE: Spotlights respawn on red player by default right now")]
    [Header("Fill Waypoints In Order Of Progression")]
    public Waypoint[] m_waypointsRed;
    public Waypoint[] m_waypointsGreen;
    public Waypoint[] m_waypointsBlue;
    private int m_waypointIndexRed;
    private int m_waypointIndexGreen;
    private int m_waypointIndexBlue;
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
        if (m_waypointIndexRed + 1 < m_waypointsRed.Length &&
            m_waypointsRed[m_waypointIndexRed + 1].Passed)
            m_waypointIndexRed++;

        if (m_waypointIndexGreen + 1 < m_waypointsGreen.Length &&
            m_waypointsGreen[m_waypointIndexGreen + 1].Passed)
            m_waypointIndexGreen++;

        if (m_waypointIndexBlue + 1 < m_waypointsBlue.Length &&
            m_waypointsBlue[m_waypointIndexBlue + 1].Passed)
            m_waypointIndexBlue++;
    }

    private void RespawnIfDead()
    {
        if ((m_redPlayer   != null && m_redPlayer.Dead) ||
            (m_greenPlayer != null && m_greenPlayer.Dead) ||
            (m_bluePlayer  != null && m_bluePlayer.Dead))
            RespawnPlayers();

        foreach (var ghostPlayer in m_ghostPlayers)
            if (ghostPlayer.Dead)
                RespawnPlayers();
    }

    private void RespawnPlayers()
    {
        if (m_redPlayer != null)
        {
            m_redPlayer.Respawn(m_waypointsRed[m_waypointIndexRed].transform.position);

            foreach (var ghostPlayer in m_ghostPlayers)
                ghostPlayer.Respawn(m_waypointsRed[m_waypointIndexRed].transform.position);
            m_ghostsRespawned = true;
        }

        if (m_greenPlayer != null)
        {
            m_greenPlayer.Respawn(m_waypointsGreen[m_waypointIndexGreen].transform.position);

            if (!m_ghostsRespawned)
            {
                foreach (var ghostPlayer in m_ghostPlayers)
                    ghostPlayer.Respawn(m_waypointsGreen[m_waypointIndexGreen].transform.position);
                m_ghostsRespawned = true;
            }
        }

        if (m_bluePlayer != null)
        {
            m_bluePlayer.Respawn(m_waypointsBlue[m_waypointIndexBlue].transform.position);

            if (!m_ghostsRespawned)
            {
                foreach (var ghostPlayer in m_ghostPlayers)
                    ghostPlayer.Respawn(m_waypointsBlue[m_waypointIndexBlue].transform.position);
            }
        }

        m_ghostsRespawned = false;
    }
    #endregion
}
