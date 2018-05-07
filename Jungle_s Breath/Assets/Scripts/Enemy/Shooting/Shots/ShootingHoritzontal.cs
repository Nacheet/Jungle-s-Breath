using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingHoritzontal : MonoBehaviour {

    public GameObject shootingTarget;
    public GameObject shot;
    public Transform shotSpawn;
    public Vector2 vector;
    public float minFireRate = 3.0f, maxFireRate = 7.0f;
    private float nextShot;

    private


    // Use this for initialization
    void Start()
    {
        shootingTarget = GameObject.Find("Player");
        nextShot = Time.time + Random.Range(minFireRate, maxFireRate);
    }


    // Update is called once per frame
    void Update()
    {
        if (shootingTarget.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x)
            vector.Set(1, 0);
        else
            vector.Set(-1, 0);

        // Disparo
        if (Time.time > nextShot) // Disparamos si ha pasado el tiempo suficiente entre disparos
        {
            nextShot = Time.time + Random.Range(minFireRate, maxFireRate); // Ponemos el valor del tiempo del siguiente disparo
            GameObject newShot = Instantiate<GameObject>(shot, shotSpawn.position, shotSpawn.rotation); // Instanciamos el disparo

            ShotBehaviour shotControl = newShot.GetComponent<ShotBehaviour>();
            shotControl.vector = vector;
        }

    }
}
