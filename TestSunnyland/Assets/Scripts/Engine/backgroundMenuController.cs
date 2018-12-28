using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMenuController : MonoBehaviour {

    public float speed = 2.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //GetComponent<SpriteRenderer>().material.mainTextureOffset = ;
	}
}
