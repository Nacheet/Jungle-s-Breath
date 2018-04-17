using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behaviour : MonoBehaviour {


    public GameObject shot;
    public Transform shotSpawn;
    public float minFireTime = 5.0f, maxFireTime = 15.0f;
    public Vector2 targetEnemyVector;
    private float nextShot;

    private


    // Use this for initialization
    void Start()
    {
        nextShot = Random.Range(minFireTime, maxFireTime);
        targetEnemyVector.x = -1;
        targetEnemyVector.y = 0;
    }


    // Update is called once per frame
    void Update()
    {

            // Disparo
            if (Time.time > nextShot) // Disparamos si ha pasado el tiempo suficiente entre disparos
            {
                nextShot = Time.time + Random.Range(minFireTime, maxFireTime); // Ponemos el valor del tiempo del siguiente disparo
                Instantiate<GameObject>(shot, shotSpawn.position, shotSpawn.rotation); // Instanciamos el disparo
            }

    }
}
