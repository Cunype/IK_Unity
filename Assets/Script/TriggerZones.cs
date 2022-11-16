using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZones : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        LookAtScript ls = collision.transform.gameObject.GetComponent<LookAtScript>();
        if (ls)
        {
            ls.lookAtObj = this.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        LookAtScript ls = collision.transform.gameObject.GetComponent<LookAtScript>();
        if (ls)
        {
            ls.lookAtObj = null;
        }
    }
}
