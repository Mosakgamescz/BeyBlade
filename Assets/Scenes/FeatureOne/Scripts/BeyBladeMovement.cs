using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BeyBladeMovement : MonoBehaviour
{

    Rigidbody rb;
    Collider col;

    float SilaOdpalu = 1000;

    public Transform enemyBeyBlade;

    Vector3 screenPosition;
    Vector3 worldPosition;

    Vector3 torqPom;
    float rad;


    Vector3 centreDest = new Vector3(0, 0, 1);

    private bool didClick = false;
    private bool ignoreCol = false;

    private float vertInput;
    private float horInput;

    private float torqueMomentum;
    

    public Transform direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.centerOfMass = new Vector3(0, 0.25f, 0);
        torqPom = new Vector3(-0.42f, 0.25f, -0.005f);
        rad = Vector3.Distance(rb.centerOfMass,torqPom);
        
    }

    // Update is called once per frame
    void Update()
    {
        vertInput = Input.GetAxisRaw("Vertical");
        horInput = Input.GetAxisRaw("Horizontal");

        torqueMomentum = rad * SilaOdpalu;

        Debug.Log(torqueMomentum);
        Rotate();

        
        if (Input.GetMouseButtonDown(0))
        {
            didClick = true;   
            
        }   
        else
        {
            
            MoveCenter();
        }

    }

    private void FixedUpdate()
    {
        if (didClick)
        {
            Move();
            didClick = false;
        }
       
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
     
        Vector3 movement = (forward * vertInput + right * horInput).normalized;
    
        rb.AddForce(movement * 100);
        
    }

    private void Move()
    {

        screenPosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            worldPosition = hit.point;
        }

        Vector3 dir = (worldPosition - transform.position);
        rb.AddForceAtPosition(dir.normalized * 150, transform.position,ForceMode.Impulse);
        
        
    }

    void MoveCenter()
    {

        Vector3 smer = ((new Vector3(0, -15,0 ) - transform.position));
       
        float vzdalenost = smer.magnitude;

       
        smer.Normalize();
        float sila = Mathf.Clamp01(2f / vzdalenost);
        transform.Translate(smer * 5 * sila * Time.deltaTime, Space.World);
    }

    private void Rotate()
    {

        Vector3 antiGravityTorque = -Physics.gravity * rb.mass;

        Vector3 angularMomentum = new Vector3(
        rb.inertiaTensor.x * rb.angularVelocity.x,
        rb.inertiaTensor.y * (rb.angularVelocity.y),
        rb.inertiaTensor.z * rb.angularVelocity.z
        );
        
        rb.AddRelativeTorque(angularMomentum, ForceMode.Force);
        rb.AddTorque(antiGravityTorque, ForceMode.VelocityChange);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("beyblade") && ignoreCol == false)
        {
            Debug.Log("funguje1");
            float bounce = 2;
            rb.AddForce(new Vector3(transform.position.x,0,transform.position.z) * -bounce,ForceMode.Impulse);
            ignoreCol = true;
            StartCoroutine(Disablecol());
            
        }
        
    }

   IEnumerator Disablecol()
    {
        yield return new WaitForSeconds(5);
        ignoreCol = false;
    }

    
}
