using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Ghostthree : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    Material scaredMaterial;
    Material normalMaterial;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = PickRandomPosition();

        normalMaterial = GetComponent<Renderer>().material;

        startPos = transform.position;
    }

    Vector3 PickRandomPosition()
    {
        Vector3 destination = transform.position;
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle * 8.0f;
        destination.x += randomDirection.x;
        destination.z += randomDirection.y;

        NavMeshHit navHit;
        NavMesh.SamplePosition(destination, out navHit, 8.0f, NavMesh.AllAreas);

        return navHit.position;

        startPos = navHit.position;
    }

    Vector3 PickHidingPlace()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position - (directionToPlayer * 2.0f), out navHit, 2.0f, NavMesh.AllAreas);

        return navHit.position;
    }

    bool hiding = false;
    // Update is called once per frame
    void Update()
    {
        if (player.PowerupActive())
        {
            //Debug.Log("Hiding from Player!");
            if (!hiding || agent.remainingDistance < 0.5f)
            {
                hiding = true;
                agent.destination = PickHidingPlace();
                GetComponent<Renderer>().material = scaredMaterial;
            }
        }
        else
        {
            if (hiding)
            {
                GetComponent<Renderer>().material = normalMaterial;
                hiding = false;
            }

            
            if (agent.remainingDistance < 0.5f)
            {
                agent.destination = PickRandomPosition();
                hiding = false;
                GetComponent<Renderer>().material = normalMaterial;
            }
            

        }

    }

    [SerializeField]
    Fellow player;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Fellow"))
        {
            if (player.PowerupActive())
            {
                Debug.Log("88");
                //gameObject.SetActive(false);
                transform.position = startPos;

            }

        }
    }
}
