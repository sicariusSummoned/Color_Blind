using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 11/2/2017
/// Purpose: Acts as a wall until a trigger is activated.
/// </summary>
public class MovingDoor : PlatformController
{
    #region MovingDoor Members
    public Trigger m_trigger;
    private bool m_moveComplete;
    #endregion

    #region MovingDoor Methods
    private new void Start()
    {
        m_moveComplete = false;
        base.Start();
    }

    private new void Update()
    {
        // Only move once trigger is activated
        if (m_trigger.Activated && !m_moveComplete)
        {
            UpdateRaycastOrigins();

            Vector3 velocity = CalculatePlatformMovement();

            //CalculatePassengerMovement(velocity);

            //MovePassengers(true);
            transform.Translate(velocity);
            //MovePassengers(false);
        }
    }

    private new Vector3 CalculatePlatformMovement()
    {
        if (Time.time < nextMoveTime)
        {
            return Vector3.zero;
        }

        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoints += Time.deltaTime * speed / distanceBetweenWaypoints;

        percentBetweenWaypoints = Mathf.Clamp01(percentBetweenWaypoints);
        float easedPercentBetweenWaypoints = Ease(percentBetweenWaypoints);

        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easedPercentBetweenWaypoints);

        // We have reached our destination so we just stop moving
        if (percentBetweenWaypoints >= 1)
            m_moveComplete = true;

        return newPos - transform.position;
    }
    #endregion
}
