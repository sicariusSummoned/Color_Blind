//based on Scripting is Fun's Youtube Tutorial - "Unity 2D Game Basics - Enemy AI - Patrolling"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Transform[] patrolPoints;
    public float speed;
    Transform currentPatrolPoint;
    int currentPatrolIndex;


	// Use this for initialization
	void Start () {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
	}
	
	// Update is called once per frame
	void Update () {
       transform.Translate(Vector3.up * Time.deltaTime * speed);
        //check to see if enemy has reached patrol point. If so switch to next patrol point.
        if(Vector3.Distance (transform.position, currentPatrolPoint.position) < .1f)
        {
            if(currentPatrolIndex +1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        //turn to face current control patrol point
        //Finding the direction Vector that points to the patrol point
        Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;
        //figure out rotation in degrees to turn enemy
        float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg - 90f;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);
	}
}