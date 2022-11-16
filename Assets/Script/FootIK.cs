using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIK : MonoBehaviour
{
    private Animator animator;

    public GameObject LeftFoot;
    public GameObject RightFoot;

    RaycastHit LeftFootCurrentPos;
    RaycastHit RightFootCurrentPos;

    Vector3 LeftFootLastPos;
    Vector3 RightFootLastPos;

    RaycastHit LeftFootNextPos;
    RaycastHit RightFootNextPos;

    public float speed;

    public float floorHeightDetection;

    public float footheight;

    public Vector3 direction;

    public GameObject orientationVisualizer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //FOOT RAYCASTS
        LeftFootRaycast();
        RightFootRaycast();

        //FOOT NEXT STEP CALCULATION
        LeftNextFootStep();
        RightNextFootStep();

          transform.localPosition = new Vector3(transform.position.x,
               (Mathf.Abs(LeftFootLastPos.y + RightFootLastPos.y) / 2.0f), transform.position.z);

          orientationVisualizer.transform.position = transform.position;
          orientationVisualizer.transform.rotation = Quaternion.LookRotation(direction);
          //
          //LeftFootSphereCollider.center = LeftFoot.transform.localPosition;
          //RightFootSphereCollider.center = RightFoot.transform.localPosition;
    }

    void OnAnimatorIK(int layerIndex)
    {
        LeftFootCurrentPos.point = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        RightFootCurrentPos.point = animator.GetIKPosition(AvatarIKGoal.RightFoot);

         Vector3 dir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

         dir.Normalize();

         Vector3 anim_dir = transform.forward * animator.GetFloat("XInput") +
                            transform.right * animator.GetFloat("YInput");
             
         anim_dir.Normalize();
         direction = anim_dir;

         if(dir != Vector3.zero)
         { 
             FootIkMoving();
         }else{
             FootIkIdle();
        }

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(LeftFootCurrentPos.point, 0.15f);
        Gizmos.DrawSphere(RightFootCurrentPos.point, 0.15f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(LeftFootLastPos, 0.15f);
        Gizmos.DrawSphere(RightFootLastPos, 0.15f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(LeftFootNextPos.point, 0.15f);
        Gizmos.DrawSphere(RightFootNextPos.point, 0.15f);
    }

    void FootIkIdle(){

        RaycastHit hit;
            if (Physics.Raycast(LeftFootLastPos + (Vector3.up * 1.5f), -Vector3.up, out hit, 1.68f))
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + (Vector3.up * footheight));
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(Vector3.up,hit.normal)*transform.rotation);
            }

            if (Physics.Raycast(RightFootLastPos + (Vector3.up * 1.5f), -Vector3.up, out hit, 1.68f))
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + (Vector3.up * footheight));
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1.0f);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(Vector3.up,hit.normal)*transform.rotation) ;
            }
    }
    
    void FootIkMoving(){

        RaycastHit hit;
        if (Physics.Raycast(LeftFootLastPos + (Vector3.up * 1.5f), -Vector3.up, out hit, 1.68f))
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("LeftFootIKWeight"));
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + (Vector3.up * footheight));
            // animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("LeftFootIKWeight"));
            //
            // animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(Vector3.up,hit.normal)*transform.rotation);
        }

        if (Physics.Raycast(RightFootLastPos + (Vector3.up * 1.5f), -Vector3.up, out hit, 1.68f))
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, animator.GetFloat("RightFootIKWeight"));
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + (Vector3.up * footheight));
                // animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat("RightFootIKWeight"));
                //
                // animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(Vector3.up,hit.normal)*transform.rotation) ;
        }
    }

    void LeftFootRaycast(){
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(LeftFootCurrentPos.point + Vector3.up, -Vector3.up, out hit, floorHeightDetection))
        {
            if(hit.transform.gameObject != this){
                
                LeftFootLastPos = hit.point;
                
            }
        }
        Debug.DrawRay(LeftFootCurrentPos.point + Vector3.up, -transform.TransformDirection(Vector3.up) * floorHeightDetection, Color.green);
    }

    void RightFootRaycast(){
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(RightFootCurrentPos.point + Vector3.up, -Vector3.up, out hit, floorHeightDetection))
        {
            if(hit.transform.gameObject != this){
                
                RightFootLastPos = hit.point;
                
            }
        }
        Debug.DrawRay(RightFootCurrentPos.point + Vector3.up, -transform.TransformDirection(Vector3.up) * floorHeightDetection, Color.green);
    }

    void LeftNextFootStep(){

        Vector3 dir = transform.forward * animator.GetFloat("YInput") + transform.right * animator.GetFloat("XInput");

        Vector3 res = LeftFootLastPos + dir * (speed * 1.0f);

        LeftFootNextPos.point = res;

        RaycastHit hit;
        if (Physics.Raycast(res + (Vector3.up * 2.0f), -Vector3.up, out hit, 4.0f))
        {
            if(hit.transform.gameObject != this && hit.transform.gameObject.tag != "Player"){
                Debug.DrawRay(res  + (Vector3.up * 2.0f), -transform.TransformDirection(Vector3.up) * 4.0f, Color.yellow);
                LeftFootNextPos = hit;
            }
        }
        
    }
    void RightNextFootStep(){
        Vector3 dir = transform.forward * animator.GetFloat("YInput") + transform.right * animator.GetFloat("XInput");
        //dir.Normalize();
        Vector3 res = RightFootLastPos + dir * (speed * 1.0f);
        RightFootNextPos.point = res;

        RaycastHit hit;
        if (Physics.Raycast(res + (Vector3.up * 2.0f), -Vector3.up, out hit, 4.0f))
        {
            if(hit.transform.gameObject != this && hit.transform.gameObject.tag != "Player"){
                Debug.DrawRay(res  + (Vector3.up * 2.0f), -transform.TransformDirection(Vector3.up) * 4.0f, Color.yellow);
                RightFootNextPos = hit;
            }
        }
    }
}
