using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour {

    public float speed;
    public Vector2 vector;
    public float deathTime = 0.5f;
    private GameObject player;
    private GameObject shield;
    private float initTime;
    private float modulo;
    public bool rebote = false;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        shield = GameObject.Find("Shield");
  

        vector = GameObject.Find("Enemy").GetComponent<ShootingScript>().targetEnemyVector;
        modulo = Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2));

        vector.x = (vector.x/modulo)*speed; // Hacemos que el vector sea unitario para quedarnos sólo con la dirección en la que queremos disparar
        vector.y = (vector.y/modulo)*speed; // Lo multiplicamos por speed para añadirle la velocidad deseada

        GetComponent<Rigidbody2D>().velocity=vector; // Aplicamos el vector resultante a la velocidad del objeto
        initTime = Time.time;
	}

    private void Update()
    {
        if (Time.time-initTime>deathTime)
        {
  
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == player.GetComponent<Collider2D>()){

            Destroy(gameObject);
        }

        if (collision.collider == shield.GetComponent<Collider2D>())
        {
            rebote = true;
        }

        if (collision.gameObject.tag == "Enemy" && rebote)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
