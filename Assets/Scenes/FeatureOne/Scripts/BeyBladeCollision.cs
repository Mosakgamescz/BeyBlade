using UnityEngine;

public class BeybladeCollision : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Beyblade"))
        {
            float collisionSpeed = other.GetComponent<Rigidbody>().velocity.magnitude;

            float maxCollisionSpeed = 10f; 

            int damage = Mathf.RoundToInt((collisionSpeed / maxCollisionSpeed) * 50);

            currentHealth -= damage;
        }
    }
}