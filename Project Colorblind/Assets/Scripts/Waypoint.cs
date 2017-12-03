using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 12/2/2017
/// Purpose: Stores a single position for players to respawn at.
/// </summary>
public class Waypoint : MonoBehaviour
{
    #region Waypoint Members
    public Player m_player;
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
        return Vector3.Distance(transform.position, m_player.transform.position) < m_minDist;
    }
    #endregion
}
