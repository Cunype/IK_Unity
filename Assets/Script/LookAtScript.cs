using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    private Animator animator;
    public GameObject lookAtObj;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
 
    void OnAnimatorIK(int layerIndex)
    {      
        if(animator)
        {
            if(lookAtObj){       
                animator.SetLookAtWeight(Vector3.Dot(transform.forward, (lookAtObj.transform.position - transform.position).normalized));
                animator.SetLookAtPosition(lookAtObj.transform.position);
            }              
        }
 
    }
}
