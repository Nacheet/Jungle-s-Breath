using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behaviour : MonoBehaviour {

    public int bossHP = 10;
    public int spawnNow;
    public GameObject enemyGenerate;
    public GameObject Enemy2;
    public GameObject shot;
    public Transform shotSpawn1, shotSpawn2, shotSpawn3, shotSpawnBoss;
    private Vector3 init1, init2,init3;
    public float minFireTime = 8.0f, maxFireTime = 10.0f;
    public float minGenerateTime = 5.0f, maxGenerateTime = 7.0f;
    public Vector2 targetVector;
    private float nextShot, nextEnemy;


    private Vector3 initialPosition;


    // Use this for initialization
    void Start()
    {
        nextEnemy = Time.time + Random.Range(minGenerateTime, maxGenerateTime);
        nextShot = Time.time + Random.Range(minFireTime, maxFireTime);
        initialPosition = gameObject.GetComponent<Transform>().localPosition;
        init1 = shotSpawn1.GetComponent<Transform>().position;
        init2 = shotSpawn2.GetComponent<Transform>().position;
        init3 = shotSpawn3.GetComponent<Transform>().position;
        Instantiate<GameObject>(Enemy2, shotSpawn1.position, shotSpawn1.rotation);
        Instantiate<GameObject>(Enemy2, shotSpawn2.position, shotSpawn2.rotation);
        Instantiate<GameObject>(Enemy2, shotSpawn3.position, shotSpawn3.rotation);
        targetVector.x = -1;
        targetVector.y = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (bossHP <= 10 && bossHP >= 7)
        {
            minGenerateTime = 6.0f;
            maxGenerateTime = 8.0f;

            minFireTime = 3.0f;
            maxFireTime = 5.0f;
        }
        else if (bossHP < 7 && bossHP > 3)
        {
            minGenerateTime = 4.0f;
            maxGenerateTime = 6.0f;

            minFireTime = 3.0f;
            maxFireTime = 5.0f;
        }
        else
        {
            minGenerateTime = 2.0f;
            maxGenerateTime = 3.0f;

            minFireTime = 3.0f;
            maxFireTime = 5.0f;

        }

        if (Time.time > nextEnemy)
        {
            spawnNow = Random.Range(1, 3);

            if (spawnNow == 1)
            {                              
                   Instantiate<GameObject>(enemyGenerate, shotSpawn1.position, shotSpawn1.rotation);                               
            }
            if (spawnNow == 2)
            {                              
                    Instantiate<GameObject>(enemyGenerate, shotSpawn2.position, shotSpawn2.rotation);                
            }
            if (spawnNow == 3)
            {             
                    Instantiate<GameObject>(enemyGenerate, shotSpawn3.position, shotSpawn3.rotation);
            }

            nextEnemy = Time.time + Random.Range(minGenerateTime, maxGenerateTime);
        }




        if (Time.time > nextShot)
        {
            GameObject newShot = Instantiate<GameObject>(shot, shotSpawnBoss.position, shotSpawnBoss.rotation);

            ShotBehaviour shotControl = newShot.GetComponent<ShotBehaviour>();
            shotControl.vector = targetVector;
            nextShot = Time.time + Random.Range(minFireTime, maxFireTime);
        }

        gameObject.GetComponent<Transform>().localScale = new Vector3(20+bossHP,20+bossHP,0);
        gameObject.GetComponent<Transform>().localPosition = initialPosition + (new Vector3(0, - 0.65f * (10-bossHP), 0));

        shotSpawn1.GetComponent<Transform>().position = init1;
        shotSpawn2.GetComponent<Transform>().position = init2;
        shotSpawn3.GetComponent<Transform>().position = init3;

    }
}
