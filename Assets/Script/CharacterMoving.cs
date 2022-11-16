using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    public Transform capsulecenter;
    public Transform headposition;
    
    private CharacterController controller;
    private Animator animator;

    public float accelerationSpeedX = 7.0f;
    public float accelerationSpeedY = 7.0f;

    float x = 0.0f;
    float y = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        x = Mathf.Lerp(x,Input.GetAxis("Horizontal"),Time.deltaTime * accelerationSpeedX);
        y = Mathf.Lerp(y,Input.GetAxis("Vertical"),Time.deltaTime * accelerationSpeedY);

        animator.SetFloat("XInput", x);
        animator.SetFloat("YInput", y);
    }
}
