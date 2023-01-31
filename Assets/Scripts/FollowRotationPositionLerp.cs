using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotationPositionLerp : MonoBehaviour
{

    public Transform target;
    public float SmoothPosition = 6;
    public float SmoothRotation = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * SmoothPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * SmoothRotation);



    }
}
