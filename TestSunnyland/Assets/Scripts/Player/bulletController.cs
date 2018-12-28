using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour {

    public float speed = 5f;
    public GameObject dyingParticuleSystem;

    void Start()
    {
        if (transform.rotation.x < 0)
            speed *= -1;
    
        
    }

    // Update is called once per frame
    void Update () {
        
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        die(0.7f);


     
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.tag != "Checkpoint" )
        {
            die(0f);
        }
    }

    
    



    private void die(float time)
    {
        if (time == 0)
        {
            Instantiate(dyingParticuleSystem, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, time);
    }
}
