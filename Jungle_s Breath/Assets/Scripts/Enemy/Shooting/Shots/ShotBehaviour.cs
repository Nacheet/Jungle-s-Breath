using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour {

    public float speed;
    public Vector2 vector;
    public float deathTime = 0.5f;
    public float deadlyTime = 0.5f;
    private GameObject player;
    private GameObject shield;
    private Vector3 initSize;
    private float initTime;
    private float modulo, size;
    public bool rebote = false;
    bool summoning,summoned;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        shield = GameObject.Find("Shield"); 

        vector.x = (vector.x)*speed; // Hacemos que el vector sea unitario para quedarnos sólo con la dirección en la que queremos disparar
        vector.y = (vector.y)*speed; // Lo multiplicamos por speed para añadirle la velocidad deseada

        summoning = true;
        summoned = true;
        initSize = this.GetComponent<Transform>().localScale;
        this.GetComponent<Transform>().localScale = new Vector3(0,0,0);
        size = 0;
	}

    private void Update()
    {
        if (summoning)
        {
            if (size<initSize.x)
            {
                size += 0.5f;
                this.GetComponent<Transform>().localScale = new Vector3 (size,size,1);
            }
            else
                summoning = false;
        }
        else
        {
            if (summoned)
            {
                initTime = Time.time;
                GetComponent<Rigidbody2D>().velocity = vector; // Aplicamos el vector resultante a la velocidad del objeto
                summoned = false;
            }

            if (Time.time-initTime>deathTime)
            {
  
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!summoning)
        {
            if (collision.collider == player.GetComponent<Collider2D>()){

                Destroy(gameObject);
            }

            if (collision.gameObject.layer == 9) // quan choca amb el terra
            {
                Destroy(gameObject);
            }


            if (collision.collider == shield.GetComponent<Collider2D>())
            {
                rebote = true;
            }

            if (collision.gameObject.tag == "Enemy" && Time.time-initTime>deadlyTime)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }

            if (collision.gameObject.tag == "Boss1" && rebote && gameObject.tag=="ShotBoss")
            {
                if (collision.gameObject.GetComponent<Boss1Behaviour>().bossHP>0)
                {
                    collision.gameObject.GetComponent<Boss1Behaviour>().bossHP--;
                }
                else
                {
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
                    collision.gameObject.GetComponent<Boss1Behaviour>().nextDieTime = Time.time;
                    collision.gameObject.GetComponent<Boss1Behaviour>().dead = true;
                }
                Destroy(gameObject);
            }
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
