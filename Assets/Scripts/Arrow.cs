using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float speed = 10;
    [SerializeField] Transform tip;
    [SerializeField] float radius;
    [SerializeField] LayerMask interactable;
    LastBoss lastBoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localRotation==Quaternion.Euler(0,0,90))
        {
            transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        }
        else if (transform.localRotation == Quaternion.Euler(0, 0, -90))
        {
            transform.Translate(new Vector3(0, 1, 0) * speed* Time.deltaTime);
        }
        Hit();
    }

    bool Hit()
    {
        Collider2D coll = Physics2D.OverlapCircle(tip.position, radius, interactable);
        if (coll != null)
        {
            if (coll.gameObject.CompareTag("enemy"))
            {
               Animator enemyAnim = coll.GetComponent<Animator>();
               CapsuleCollider2D enemyColl = coll.GetComponent<CapsuleCollider2D>();
               enemyAnim.SetTrigger("die");
               enemyColl.enabled = false;
                Destroy(gameObject);
            }
            else if (coll.gameObject.CompareTag("Boss"))
            {
                lastBoss.TakeDamage();
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(tip.position, radius);
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            Destroy(gameObject);
        }
    }
}
