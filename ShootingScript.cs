using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingScript : MonoBehaviour {

    public GameObject shootingTarget;
    private Vector2 targetEnemyVector;
    public float detectionDistance = 10;


    // Use this for initialization
    void Start() {
        shootingTarget = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update() {

        // Detección jugador
        // Calculamos el vector entre el enemigo y el jugador
        targetEnemyVector.x = shootingTarget.transform.position.x - gameObject.transform.position.x;
        targetEnemyVector.y = shootingTarget.transform.position.y - gameObject.transform.position.y;

        // Actuamos si el jugador entra en el rango de visión del enemigo
        if (Mathf.Sqrt(Mathf.Pow(targetEnemyVector.x, 2) + Mathf.Pow(targetEnemyVector.y, 2)) <= detectionDistance)
        {
            // Disparo


        }

    }
}