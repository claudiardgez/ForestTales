using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float h;
    int actualJumps;
    int maxJumps = 2;
    Rigidbody2D rb;
    Animator anim;

    [Header("Movement")]
    [SerializeField]float force;
    [SerializeField]float jumpForce;
    [SerializeField]Transform spawn;

    [Header("Overlap")]
    [SerializeField] Transform feet;
    [SerializeField] Transform interactions;
    [SerializeField] float radius;
    [SerializeField] float intRadius;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask interactable;

    [Header("Attacks")]
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform character;

    [Header("Attack timing")]
    [SerializeField] float timeLeft;
    [SerializeField] float timeBetween;

    int health;
    int maxHearts = 5;
    int coins;

    [SerializeField] GameManager gM;
    LastBoss lastBoss;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHearts;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        if (h>0)
        {
            transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
            anim.SetBool("running", true);
        }
        else if (h < 0)
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
            
        }


        if (Input.GetKeyDown(KeyCode.Space)&&actualJumps<=maxJumps && actualJumps>0)
        {
            rb.AddForce(new Vector3(0, 1, 0).normalized * jumpForce, ForceMode2D.Impulse);
            actualJumps--;
            anim.SetBool("running", false);
            anim.SetBool("falling", false);
            anim.SetTrigger("jump");
        }
        if(rb.velocity.y < 0)
        {
            anim.SetBool("falling", true);
        }
        else
        {
            anim.SetBool("falling", false);
        }
        OnFloor();
        miscInteractions();

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0) && timeLeft <= 0)
        {
            anim.SetTrigger("melee");
            Interactions();
            timeLeft = timeBetween;   
        }
        else if (Input.GetMouseButtonDown(1) && timeLeft <= 0)
        {
            if (transform.localScale==new Vector3(-1.3f, 1.3f, 1.3f))
            {
                Instantiate(arrowPrefab, character.position, Quaternion.Euler(0, 0, -90));

            }
            else if (transform.localScale == new Vector3(1.3f, 1.3f, 1.3f))
            {
                Instantiate(arrowPrefab, character.position, Quaternion.Euler(0, 0, 90));
            }
            timeLeft = timeBetween;
            anim.SetTrigger("arrow");
        }
        if (health<=0)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(h, 0, 0) * force, ForceMode2D.Force);
    }

    bool OnFloor()
    {
        Collider2D coll = Physics2D.OverlapCircle(feet.position, radius, ground);
        if (coll != null)
        {
            if (rb.velocity.y <= 0)
            {
                actualJumps = maxJumps;
                anim.SetBool("falling", false);
            }
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(feet.position, radius);
        Gizmos.DrawSphere(interactions.position, intRadius);
    }

    bool Interactions()
    {
        Collider2D coll = Physics2D.OverlapCircle(interactions.position, intRadius, interactable);
        if (coll != null)
        {
            if (coll.gameObject.CompareTag("enemy"))
            {
                Animator enemyAnim = coll.GetComponent<Animator>();
                CapsuleCollider2D enemyColl = coll.GetComponent<CapsuleCollider2D>();
                enemyAnim.SetTrigger("die");
                enemyColl.enabled = false;
            }

            else if (coll.gameObject.CompareTag("Boss"))
            {
                lastBoss.TakeDamage();
            }
            return true;
        }
        return false;
    }

    bool miscInteractions()
    {
        Collider2D coll = Physics2D.OverlapCircle(interactions.position, intRadius, interactable);
        if (coll != null)
        {
        if (coll.gameObject.CompareTag("coins"))
        {
            coins++;
                GameManager.gM.SaveData(health, coins);
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("potion"))
        {
                if (health <5)
                {
                   health++;
                    GameManager.gM.SaveData(health, coins);
                }
            Destroy(coll.gameObject);
        }
            else if (coll.gameObject.CompareTag("portal1"))
            {
                SceneManager.LoadScene(1);
            }
            else if (coll.gameObject.CompareTag("portal2"))
            {
                SceneManager.LoadScene(2);
            }
            return true;
        }
        return false;
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
       if (coll.gameObject.CompareTag("enemy"))
       {
            TakeDamage();
            Debug.Log("daño");
       }
       else if (coll.gameObject.CompareTag("spikes"))
       {
            TakeDamage();
            transform.position = spawn.position;
       }
    }

    public void TakeDamage()
    {
        health--;
        GameManager.gM.SaveData(health, coins);
    }
}
