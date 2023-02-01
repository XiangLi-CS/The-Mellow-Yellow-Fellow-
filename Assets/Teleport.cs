using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    public Transform target = null;
    public Transform target2 = null;
    bool bJump = false;
    bool bJump2 = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Teleport" && bJump == false && bJump2 == false)
        {
            this.transform.position = target.position;
            bJump = true;
        }

        if (other.gameObject.tag == "Teleport2" && bJump == false && bJump2 == false)
        {
            this.transform.position = target2.position;
            bJump2 = true;
        }
    }

    void onTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Teleport")
        {
            bJump2 = false;
        }

        if (other.gameObject.tag == "Teleport2")
        {
            bJump = false;
        }
    }
}
