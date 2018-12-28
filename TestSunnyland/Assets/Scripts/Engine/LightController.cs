using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    // Public variables
    public float timeToAlive = 0.1f;
    public float maxIntens = 1.3f;


    // Private variables
    private float time = 2000;
    private bool alive = false;
    private Vector3 movement;
    private Vector3 lastPosition;
    private bool hasRb2d;


    //Referencies
    private Rigidbody2D rb2d;
    public Light lightOfTheObject;


    private void Start()
    {
        //If gameobject has a Rigidbody then we are wokring with it, if it doesn't then we will work with transform
        hasRb2d = HasComponent<Rigidbody2D>();
        if (hasRb2d) 
            rb2d = GetComponent<Rigidbody2D>();
        else
            lastPosition = transform.position;

        
    }


    // Update is called once per frame
    void Update() {
        updateTheLight();
    }




    private void updateTheLight()
    {
        // Working with the Rigidbody
        if (hasRb2d)
        {
            if (rb2d.velocity.magnitude > 0)
            {
                alive = true;
            }
        }

        // Working with the transofrm
        else
        {
            if(lastPosition != transform.position)
            {
                alive = true;
                lastPosition = transform.position;
            }
        }


        // Time to be alive 
        time += Time.deltaTime;
        if (time > timeToAlive){
            alive = false;
            time = 0;
        }

        // Smoothing the intensity over time
        if (alive)
            lightOfTheObject.intensity += 0.01f;
        else
            lightOfTheObject.intensity -= 0.01f;


        // Maximum intensity
        if (lightOfTheObject.intensity > maxIntens){
            lightOfTheObject.intensity = maxIntens;
        }
    }



    // Checking if gameObject has a component
    public bool HasComponent<T>() where T : Component
    {
        return GetComponent<T>() != null;
    }
}
