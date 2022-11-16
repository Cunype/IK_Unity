using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingSystem : MonoBehaviour
{
    private CharacterController controller;
    
    private Animator animator;
    
    public float forwardDistance;

    public Transform normalHeight;

    public float heightDiff;

    public float heightDetection;

    private FootIK Ik;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Ik = GetComponent<FootIK>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CeilingRaycast();
        
        animator.SetFloat("CeilingHeight", heightDiff );

        //controller.center = Vector3.Lerp(new Vector3(0.0f,1.0f, 0.0f),new Vector3(0.0f,1.49f, 0.0f),heightDiff);
        //controller.height = Mathf.Lerp(1.0f,2.57f,heightDiff);
    }
    
    void CeilingRaycast(){
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        
        if (Physics.Raycast(transform.position + Vector3.up, (Vector3.up + (Ik.direction)  * forwardDistance).normalized, out hit, heightDetection))
        {
            if(normalHeight && hit.transform.gameObject.tag != "floor")
                heightDiff = Mathf.Lerp(heightDiff,(normalHeight.transform.position.y - hit.transform.position.y) - 1.0f,5.0f * Time.deltaTime);
        }
        else
        {
            heightDiff = Mathf.Lerp(heightDiff, 0.0f, 5.0f * Time.deltaTime);
        }
        Debug.DrawRay(transform.position + Vector3.up, (Vector3.up + (Ik.direction) * forwardDistance).normalized * (heightDetection), Color.green);
    }
}
