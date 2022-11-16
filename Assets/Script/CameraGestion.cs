using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGestion : MonoBehaviour
{
    private Animator animator;

    private CrouchingSystem cySystem;
    //
    public Transform camera;
    public Transform cameraTarget;
    public float pLerp = 0.02f;
    public float tLerp = 0.02f;
    //
    private Vector2 turn;
    public float sensitivity = 0.5f;

    public Transform targetRotation;

    public bool close;


    public Transform dir;
    // Start is called before the first frame update
    void Start()
    {
        close = false;
        animator = GetComponent<Animator>();
        cySystem = GetComponent<CrouchingSystem>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        
        camera.transform.position = Vector3.Lerp(camera.transform.position, cameraTarget.transform.position, pLerp);

        targetRotation.rotation = Quaternion.Euler(Mathf.Clamp(-turn.y,-35.0f,35.0f),turn.x,0.0f);
        camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, targetRotation.rotation, tLerp);

        float z = Mathf.Lerp(0.0f ,-4.77f, 0.75f - cySystem.heightDiff);
        float y = Mathf.Lerp(0.0f ,2.76f, 0.75f - cySystem.heightDiff);
        cameraTarget.transform.localPosition = new Vector3(cameraTarget.transform.localPosition.x,y,z);
        
        transform.localRotation = Quaternion.Euler(0.0f, turn.x, 0.0f);

        if (Input.GetMouseButton(0))
        {
            animator.SetFloat("Firering",1.0f);
        }
        else
        {
            animator.SetFloat("Firering",0.0f);
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        animator.SetBoneLocalRotation(HumanBodyBones.Chest,   Quaternion.Euler(Mathf.Clamp(-turn.y,-35.0f,35.0f),0.0f,0.0f));
    }
}
