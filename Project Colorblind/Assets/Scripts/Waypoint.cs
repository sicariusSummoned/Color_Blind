using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/27/2017
/// Purpose: Stores a single position for players to respawn at.
/// </summary>
public class Waypoint : MonoBehaviour
{
    #region Waypoint Members
    public Player[] m_players;
    public float m_minDist;
    [HideInInspector] private bool m_passed;
    [HideInInspector] public bool Passed
    {
        get { return m_passed; }
    }
    #endregion

    #region Waypoint Methods
    void Update()
    {
        // Janky Solution
        // Triggers Passed
        if (!Passed && PlayerNear())
            m_passed = true;
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
