using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    #region EnemyPatrol Members
    public Vector3[] m_waypoints;
    private Vector3 m_nextPosition;
    #endregion

    #region EnemyPatrol Methods
    private void Update()
    {
        m_nextPosition = Vector3.Lerp(m_waypoints[0], m_waypoints[1], 1);
        transform.position = transform.position - m_nextPosition;
    }
    #endregion
}
