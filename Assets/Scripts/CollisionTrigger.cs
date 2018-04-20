using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour {

    private BoxCollider2D playerCollider;
    [SerializeField]

    private BoxCollider2D platformcollider;
    [SerializeField]

    private BoxCollider2D platformtrigger;

	// Use this for initialization
	void Start () {
		
		playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformcollider, platformtrigger, true);
	}

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.name == "Player") {
            Physics2D.IgnoreCollision(platformcollider, playerCollider,true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformcollider, playerCollider, false);
        }
        
    }
}
