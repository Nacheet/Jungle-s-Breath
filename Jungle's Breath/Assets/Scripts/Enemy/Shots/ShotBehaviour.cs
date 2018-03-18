using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour {

    public float speed;
    public Vector2 vector;
    public float deathTime = 0.5f;
    private float initTime;
    private float modulo;


    // Use this for initialization
    void Start () {
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
        if (collision.gameObject.tag=="Player")
        {

        }
        else if (collision.gameObject.tag == "Enemy")
        {

        }
    }
}
