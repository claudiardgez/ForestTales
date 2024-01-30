using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushy : MonoBehaviour
{
    [SerializeField] Transform pointA, pointB;
    [SerializeField] float speed;
    Vector3 destiny;
    Rigidbody2D rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        destiny = pointB.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(MoveAndWait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveAndWait()
    {
        //Debug.Log("Tengo que llegar a mi destino");
        while (true)
        {
            while (transform.position != destiny)
            {
                anim.SetBool("running", true);
                transform.position = Vector3.MoveTowards(transform.position, destiny, speed * Time.deltaTime);
                //Debug.Log("me muevo a mi destino");
                yield return null;
            }
            anim.SetBool("running", false);
            //Debug.Log("He llegado, espero");
            yield return new WaitForSeconds(2f);
            transform.eulerAngles += new Vector3(0, 180, 0);

            if (destiny == pointB.position)
            {
                //Debug.Log("Cambio de destino");
                destiny = pointA.position;
            }
            else
            {
                destiny = pointB.position;
            }

            }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }


}
