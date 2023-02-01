using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fellow : MonoBehaviour
{
    public float volume;
    int score = 0;
    int pelletsEaten = 0;
    [SerializeField]
    int pointsPerPellet = 100;

    [SerializeField]
    float powerupDuration = 10.0f;
    float powerupTime = 0.0f;

    Vector3 dest = Vector3.zero;

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pellet"))
        {
            //Adding score by eating pellet
            pelletsEaten++;
            score += pointsPerPellet;
            Debug.Log("Score is" + score);
        }

        if (other.gameObject.CompareTag("Powerup"))
        {
            powerupTime = powerupDuration;
        }

    }

    public bool PowerupActive()
    {
        return powerupTime > 0.0f;
    }


    public int PelletsEaten()
    {
        return pelletsEaten;
    }

    public int Score()
    {
        return score;
    }
    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;

        lifeCounter = startingLives;

    }

    // Update is called once per frame
    [SerializeField]
    float speed = 0.05f;

    void Update()
    {
        //Vector3 pos = transform.position;

        //if (Input.GetKey(KeyCode.A))
        //{
        //    pos.x -= speed;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    pos.x += speed;
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    pos.z += speed;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    pos.z -= speed;
        //}
        //transform.position = pos;

        //how long the powerup item can remain
        powerupTime = Mathf.Max(0.0f, powerupTime - Time.deltaTime);


    }

    void FixedUpdate()
    {
        Rigidbody b = GetComponent<Rigidbody>();
        Vector3 velocity = b.velocity;

        if (Input.GetKey(KeyCode.A))
        {
            velocity.x = -speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x = speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            velocity.z = speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.z = -speed;
        }
        b.velocity = velocity;

    }

    int lifeCounter;
    [SerializeField]
    int startingLives = 3;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            //Cannot be ate by ghost when powerup
            if (PowerupActive())
            {
                gameObject.SetActive(true);

            }
            else
            {
                //decrease life 
                if(lifeCounter != 0)
                {
                    lifeCounter--;
                    Debug.Log("You have " + lifeCounter +" lives");
                    transform.position = dest;
                }               

            }
        }
    }

    public int LifeCounter()
    {
        return lifeCounter;
    }
}
