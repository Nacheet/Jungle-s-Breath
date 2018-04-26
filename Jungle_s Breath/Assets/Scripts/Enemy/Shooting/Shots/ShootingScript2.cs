using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingScript2 : MonoBehaviour
{

    public GameObject shot;
    public Transform shotSpawn;
    public Vector2 vector;
    public float minFireRate = 3.0f, maxFireRate=7.0f;
    private float nextShot;

    private


    // Use this for initialization
    void Start()
    {
        vector.Set(0, -1);
        nextShot = Time.time + Random.Range(minFireRate, maxFireRate);
    }


    // Update is called once per frame
    void Update()
    {

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
