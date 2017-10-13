using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    // Use this for initialization

    public Transform target;
    public float distance = .2f;
    public float height = .50f;
    public float heightDamping = .5f;
    public float positionDamping = .5f;
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        // Early out if we don't have a target
        if (!target)
            return;
        // float wantedHeight = target.position.y;
        //float currentHeight = transform.position.y;

        //currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        Vector3 temp = new Vector3(target.position.x, target.position.y, -10);
        Debug.Log("Pre z is:" + transform.position.z);
        transform.position = Vector3.Lerp(transform.position, temp, Time.deltaTime * positionDamping);
        Debug.Log("Post z is:" + transform.position.z);
        transform.position.Set(transform.position.x, transform.position.y, -10);

        
    }
}
