using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFromOnePointToTheOther : MonoBehaviour
{
    public Transform p1;

    public Transform p2;

    private float timePassed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime * 0.09f;
        transform.position = Vector3.Lerp(p1.transform.position, p2.transform.position,
            Mathf.PingPong(timePassed, 1));
    }
}
