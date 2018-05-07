using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAppleScript : MonoBehaviour {


    public float speed;
    public Vector2 vector;
    public float deathTime = 0.5f;
    public float deadlyTime = 0.5f;
    private GameObject player;
    private GameObject shield;
    private float initTime;
    private float modulo;
    public bool rebote = false;


    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        shield = GameObject.Find("Shield");

        vector.x = (vector.x) * speed; 
        vector.y = (vector.y) * speed; // Lo multiplicamos por speed para añadirle la velocidad deseada

        GetComponent<Rigidbody2D>().velocity = vector; // Aplicamos el vector resultante a la velocidad del objeto
        initTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - initTime > deathTime)
        {

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == player.GetComponent<Collider2D>())
        {

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 9) // quan choca amb el terra
        {
            Destroy(gameObject);
        }


        if (collision.collider == shield.GetComponent<Collider2D>())
        {

            Destroy(gameObject);
        }
    }
}
