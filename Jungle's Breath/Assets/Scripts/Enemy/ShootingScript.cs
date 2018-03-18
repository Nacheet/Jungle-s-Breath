using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingScript : MonoBehaviour {

    public GameObject shootingTarget;
    public GameObject shot;
    public Transform shotSpawn;
    public Vector2 targetEnemyVector;
    public float detectionDistance = 10;
    public float fireRate = 1.0f;
    private float nextShot = 0.0f;


    // Use this for initialization
    void Start() {

    }


    // Update is called once per frame
    void Update() {

        // Detección jugador
        // Calculamos el vector entre el enemigo y el jugador
        targetEnemyVector.x = shootingTarget.transform.position.x - transform.position.x;
        targetEnemyVector.y = shootingTarget.transform.position.y - transform.position.y;

        // Actuamos si el jugador entra en el rango de visión del enemigo
        if (Mathf.Sqrt(Mathf.Pow(targetEnemyVector.x, 2) + Mathf.Pow(targetEnemyVector.y, 2)) <= detectionDistance)
        {
            // Disparo
            if (Time.time>nextShot) // Disparamos si ha pasado el tiempo suficiente entre disparos
            {
                nextShot = Time.time + fireRate; // Ponemos el valor del tiempo del siguiente disparo
                Instantiate<GameObject>(shot, shotSpawn.position, shotSpawn.rotation); // Instanciamos el disparo
            }


        }

    }
}