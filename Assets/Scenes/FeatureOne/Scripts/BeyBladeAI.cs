using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class BeyBladeAI : MonoBehaviour
{
    Rigidbody rb;
    Collider col;

    float Lives = 100;

    Vector3 screenPosition;
    Vector3 worldPosition; 
    Vector3 vv;


    Vector3 centreDest = new Vector3(0, 0, 1);

    public static bool isMoving = false;
    private bool ignoreCol = false;
    private bool canUse = true;
    private bool didHit = false;

    public Transform player;
    public GameObject plajer;
    private Rigidbody rbplayer;

    private float dampingFactor = 0.8f;

    public HealthBar healthBar;

    public GameDecider gd;

    //private NavMeshAgent navMeshAgent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rbplayer = plajer.GetComponent<Rigidbody>();
        healthBar.SetMaxHealth((int)Lives);
        //navMeshAgent = GetComponent<NavMeshAgent>();
        rb.centerOfMass = new Vector3(0, 0.25f, 0);
         vv = new Vector3(-45.257f, 4.37f, 30.119f);
    }

    // Update is called once per frame
    void Update()
    {
        
        gd.CheckLives((int)Lives, "enemy");
        if (Lives > 20)
        {
          
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            Vector3 direction = player.position - transform.position;

            if (distanceToPlayer > 10)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, 0.025f);

            }
            else if (distanceToPlayer < 10)
            {
                if (canUse)
                {

                    if (!didHit)
                    {
                        rb.AddForce(direction);
                    }
                    else if (didHit)
                    {
                        StartCoroutine(SetUse());
                        didHit = false;
                    }

                }
                else if (!canUse)
                {
                    direction.Normalize();


                    transform.position -= direction * 25 * Time.deltaTime;
                }


            }


            MoveCenter();
        }
            

      
       
    }

    private void FixedUpdate()
    {
        
        if(Lives > 20)
            Rotate();
    }

    private void Move(Vector3 pos)
    {

        Vector3 dir = (pos - transform.position);
        rb.AddForceAtPosition(dir.normalized * 225, transform.position, ForceMode.Impulse);
    }

    void MoveCenter()
    {

        Vector3 smer = ((new Vector3(0, -5, 0) - transform.position));



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
        rb.inertiaTensor.y * (rb.angularVelocity.y * Lives),
        rb.inertiaTensor.z * rb.angularVelocity.z
        );
        


        rb.AddTorque(new Vector3(0, angularMomentum.y, 0), ForceMode.Force);
        //rb.AddRelativeTorque(angularMomentum, ForceMode.Force);

        //rb.AddTorque(antiGravityTorque, ForceMode.VelocityChange);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("beyblade") && ignoreCol == false)
        {
            Lives -= Mathf.Round(rbplayer.velocity.magnitude);
            healthBar.SetHealth((int)Lives);
            rb.AddTorque(new Vector3(0, rb.inertiaTensor.y * rb.angularVelocity.y, 0) * -500, ForceMode.Impulse);
            didHit = true;   
            float bounce = 4;
            rb.AddForce(new Vector3(transform.position.x, 0, transform.position.z) * -bounce, ForceMode.Impulse);
            
            //StartCoroutine(Disablecol());

        }
    }

    IEnumerator Disablecol()
    {
        yield return new WaitForSeconds(5);
        ignoreCol = false;
    }


    IEnumerator SetUse()
    {
        canUse = false;
        yield return new WaitForSeconds(8);
        canUse = true;
    }

    private void GameEndBeyBlade()
    {
        //rb.AddRelativeTorque();
        //rb.AddTorque();
    }
}
