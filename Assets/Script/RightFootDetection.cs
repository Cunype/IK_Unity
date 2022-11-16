using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFootDetection : MonoBehaviour
{
    public GameObject go;
    int count = 0;
    void OnCollisionEnter(Collision collision)
    {
        count ++;
        ContactPoint contact = collision.contacts[0];
        if(count > 0){
            Debug.Log("Derecha");
            count = 0;
            go.transform.position = contact.point;
        }
    }
}
