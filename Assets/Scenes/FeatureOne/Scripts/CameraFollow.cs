using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public Transform enemyBeyBlade;

    Vector3 offset;

    Vector2 rot;
   

    private void Start()
    {
        offset = transform.position  - Target.position;
        
    }



    private void LateUpdate()
    {
        transform.position = Target.position + offset;

        transform.LookAt(enemyBeyBlade);
    }
}
