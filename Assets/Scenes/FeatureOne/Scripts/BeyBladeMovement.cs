using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyBladeMovement : MonoBehaviour
{

    Rigidbody rb;

    int SilaOdpalu = 100;

    

    Vector3 screenPosition;
    Vector3 worldPosition;


    Vector3 centreDest = new Vector3(0, 0, 1);

    bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0.05f; 
        rb.angularDrag = 0.1f;
        rb.centerOfMass = new Vector3(0, 0.25f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            screenPosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                worldPosition = hit.point;
            }

            Move(worldPosition);
            isMoving = true;
        }

        if (isMoving)
            AddContinuousForce();
        
    }

    private void FixedUpdate()
    {
        Rotate();
    }
    private void AddContinuousForce()
    {
        //rb.AddForceAtPosition();
        // add continuous force with ForceMode.Force, to keep beyblade spinning in circle
    }

    private void Move(Vector3 pos)
    {
        
        Vector3 dir = (pos - transform.position);
        rb.AddForceAtPosition(dir.normalized * SilaOdpalu,transform.position,ForceMode.Impulse);
    }

    private void Rotate()
    {

        Vector3 antiGravityTorque = -Physics.gravity * rb.mass;

        Vector3 angularMomentum = new Vector3(
        rb.inertiaTensor.x * rb.angularVelocity.x,
        rb.inertiaTensor.y * (rb.angularVelocity.y * SilaOdpalu),
        rb.inertiaTensor.z * rb.angularVelocity.z
        );

        
        rb.AddRelativeTorque(angularMomentum, ForceMode.Force);
        rb.AddTorque(antiGravityTorque, ForceMode.VelocityChange);

    }
}
