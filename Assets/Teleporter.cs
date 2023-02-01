using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    public Teleporter target = null;
    public bool BlockTeleport { get; set; }

    void OnTriggerEnter(Collider other)
    {
        if (target == null)
            // If no target is assigned the teleporter will not teleport. So you could use this as destination if you want.
            return;

        if (BlockTeleport)
            return;

        target.BlockTeleport = true;
        other.transform.position = target.transform.position;

    }

    void OnTriggerExit(Collider other)
    {
        if (BlockTeleport)
            BlockTeleport = false;
    }
}
