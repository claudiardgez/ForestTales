using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    float speed = 1;
    float amp = 0;
    float des = 5;
    [SerializeField]Vector3 direction;
    Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float sin = amp + Mathf.Sin(speed * Time.time + des);
        transform.position = initialPos + new Vector3(sin, 0, 0)*4+ direction; 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
