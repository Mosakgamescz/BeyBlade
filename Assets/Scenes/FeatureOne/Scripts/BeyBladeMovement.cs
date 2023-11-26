using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyBladeMovement : MonoBehaviour
{

    Rigidbody rb;

    int SilaOdpalu = 50;

    Vector3 screenPosition;
    Vector3 worldPosition;


    Vector3 centreDest = new Vector3(0, 0, 1);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0.0f; // Adjust the drag as needed
        rb.angularDrag = 0.05f;
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
        }
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Move(Vector3 pos)
    {
        rb.AddForce((pos - transform.position).normalized * (SilaOdpalu), ForceMode.Impulse);
        // movement to be finnished
    }

    private void Rotate()
    {

        Vector3 antiGravityTorque = -Physics.gravity * rb.mass;

        Vector3 angularMomentum = new Vector3(
        rb.inertiaTensor.x * rb.angularVelocity.x,
        rb.inertiaTensor.y * (rb.angularVelocity.y * SilaOdpalu),
        rb.inertiaTensor.z * rb.angularVelocity.z
        );

        Debug.Log(angularMomentum);
        rb.AddRelativeTorque(angularMomentum, ForceMode.Force);
        rb.AddTorque(antiGravityTorque, ForceMode.VelocityChange);

    }
}
