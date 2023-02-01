using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
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
            //Hiding from player
            if(!hiding || agent.remainingDistance < 0.5f)
            {
                hiding = true;
                agent.destination = PickHidingPlace();
                GetComponent<Renderer>().material = scaredMaterial;
          
            }
        }
        else
        {
            //Debug.Log("Chasing Player!");
            if (hiding)
            {
                GetComponent<Renderer>().material = normalMaterial;
                hiding = false;
            }

            //Chasing player 
            if (CanSeePlayer())
            {
                agent.destination = player.transform.position;
            }
            else
            {
                if (agent.remainingDistance < 0.5f)
                {
                    agent.destination = PickRandomPosition();
                    hiding = false;
                    GetComponent<Renderer>().material = normalMaterial;
                }
            }
            
        }
        
        
    }

    [SerializeField]
    Fellow player;

    bool CanSeePlayer()
    {
        Vector3 rayPos = transform.position;
        Vector3 rayDir = (player.transform.position - rayPos).normalized;

        RaycastHit info;
        if (Physics.Raycast(rayPos, rayDir, out info))
        {
            if (info.transform.CompareTag("Fellow"))
            {
                return true;
            }
        }
        return false;

    }

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
