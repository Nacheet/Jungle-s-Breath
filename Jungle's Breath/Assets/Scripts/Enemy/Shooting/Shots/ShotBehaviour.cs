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

        vector.x = (vector.x)*speed; // Hacemos que el vector sea unitario para quedarnos sólo con la dirección en la que queremos disparar
        vector.y = (vector.y)*speed; // Lo multiplicamos por speed para añadirle la velocidad deseada

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

        if (collision.gameObject.tag == "Boss1" && rebote)
        {
            if (collision.gameObject.GetComponent<Boss1Behaviour>().bossHP>0)
            {
                collision.gameObject.GetComponent<Boss1Behaviour>().bossHP--;
            }
            else
            {
                Destroy(collision.gameObject);
                GameObject[] aux = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < aux.Length; i++)
                {
                    Destroy(aux[i]);
                }
                GameObject[] aux2 = GameObject.FindGameObjectsWithTag("Shot");
                for (int i = 0; i < aux2.Length; i++)
                {
                    Destroy(aux2[i]);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && rebote)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
