using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : MonoBehaviour
{
    int life = 15;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    float distance;

    [SerializeField] Transform interactions;
    [SerializeField] float radius;
    [SerializeField] LayerMask interactable;
    Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <=0)
        {
            anim.SetTrigger("die");
        }
        distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance < 10)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public void TakeDamage()
    {
        life--;
        anim.SetTrigger("hurt");
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void BossInteract()
    {
        Collider2D coll = Physics2D.OverlapCircle(interactions.position, radius, interactable);
        if (coll != null)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                anim.SetTrigger("attack");
                playerScript.TakeDamage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(interactions.position, radius);
    }
}
