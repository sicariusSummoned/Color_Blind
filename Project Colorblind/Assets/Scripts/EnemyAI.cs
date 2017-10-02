//based on Scripting is Fun's Youtube Tutorial - "Unity 2D Game Basics - Enemy AI - Patrolling"

using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/1/2017
/// Purpose: Moves an enemy between patrol points.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    #region EnemyAI Members
    public Transform[] patrolPoints;
    public float speed;
    Transform currentPatrolPoint;
    int currentPatrolIndex;
    #endregion

    #region EnemyAI Methods
    void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    void Update()
    {
        // Move towards patrol point
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        // Switch to next patrol point if passed current
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
        {
            if (currentPatrolIndex + 1 < patrolPoints.Length)
                currentPatrolIndex++;
            else
                currentPatrolIndex = 0;

            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        // Turn to face current control patrol point
        // Finding the direction Vector that points to the patrol point
        Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;

        // Figure out rotation in degrees to turn enemy
        float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);
    }
    #endregion
}