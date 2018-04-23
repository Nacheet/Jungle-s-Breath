using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingScript : MonoBehaviour {

    public GameObject shootingTarget;
    public GameObject shot;
    public Transform shotSpawn;
    public Vector2 targetEnemyVector;
    public Vector2 auxVector;
    public float detectionDistance = 10;
    public float fireRate = 1.0f;
    private float nextShot = 0.0f;
    public float modulo;

    private 


    // Use this for initialization
    void Start() {
        shootingTarget = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update() {

        // Detección jugador
        // Calculamos el vector entre el enemigo y el jugador
        auxVector.x = shootingTarget.transform.position.x - transform.position.x;
        auxVector.y = shootingTarget.transform.position.y - transform.position.y;

        modulo = Mathf.Sqrt(Mathf.Pow(auxVector.x, 2) + Mathf.Pow(auxVector.y, 2));

        targetEnemyVector.x = auxVector.x / modulo;
        targetEnemyVector.y = auxVector.y / modulo;

        // Actuamos si el jugador entra en el rango de visión del enemigo
        if (modulo <= detectionDistance)
        {
            // Disparo
            if (Time.time>nextShot) // Disparamos si ha pasado el tiempo suficiente entre disparos
            {
                nextShot = Time.time + fireRate; // Ponemos el valor del tiempo del siguiente disparo
                GameObject newShot = Instantiate<GameObject>(shot, shotSpawn.position, shotSpawn.rotation); // Instanciamos el disparo

                ShotBehaviour shotControl = newShot.GetComponent<ShotBehaviour>();
                shotControl.vector = targetEnemyVector;
            }


        }

    }
}