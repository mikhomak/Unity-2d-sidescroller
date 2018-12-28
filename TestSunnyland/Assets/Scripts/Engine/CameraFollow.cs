using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //REFERENCE TO THE TARGET
    public Transform target;


    public float smoothSpeed = 0.125f;
    public Vector3 offset;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 desPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desPosition, smoothSpeed);
        transform.position = smoothedPosition;

	}
}
