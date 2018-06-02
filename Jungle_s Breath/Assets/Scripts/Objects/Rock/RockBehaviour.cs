using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour {

    public Sprite rockFullHP;
    public Sprite rock1Hit;
    public Sprite rock2Hit;
    public Sprite rock3Hit;


    public int health = 4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        switch (health)
        {
            case 4:
                this.GetComponent<SpriteRenderer>().sprite = rockFullHP;
                break;
            case 3:
                this.GetComponent<SpriteRenderer>().sprite = rock1Hit;
                break;
            case 2:
                this.GetComponent<SpriteRenderer>().sprite = rock2Hit;
                break;
            case 1:
                this.GetComponent<SpriteRenderer>().sprite = rock3Hit;
                break;
            case 0:
                Destroy(this.gameObject);
                break;
            default:
                break;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
        if (collision.collider.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<PlayerController>().shieldAtt)
        {
            health--;
        }
    }
}
