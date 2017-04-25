using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulShot : MonoBehaviour {

    public float speed;
    private Transform player;
    private int lifespan = 3;
    private Vector2 previousPos;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (player.transform.position - this.transform.position).normalized * speed;
        //rb.velocity = transform.up * speed;
        //this.transform.position = player.transform.position;
    }

    void Update()
    {
        Destroy(this.gameObject, lifespan);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Decreases the players health
            other.gameObject.GetComponent<PlayerManager>().Damage(1);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
