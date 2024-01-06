using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int direction = 1;
    public float speed = 5f;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.right * direction * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            enemyController.TakeDamage();
            Destroy(this.gameObject);
        }

        else if(other.gameObject.CompareTag("Destructible"))
        {
            Destructible destructible = other.gameObject.GetComponent<Destructible>();
            destructible.TakeDamage();
        }

        else
            Destroy(this.gameObject);

        
    }
}
