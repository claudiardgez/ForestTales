using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position , player.transform.position);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance <6)
        {
           transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position,speed*Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
