using UnityEngine;
using System.Collections;

public class parachuteBomb : bomb {

	bool started = false;

	// Use this for initialization
	protected new void Start () {
		force = new Vector2 (0, -.1f);
		GetComponent<Rigidbody2D> ().AddForce (force);
	}
	
	protected new void OnCollisionEnter2D(Collision2D other) {
		base.OnCollisionEnter2D (other);
		if (!started) {
			started = true;
			if (transform.position.x > 0)
				force = new Vector2 (-INITIAL_VELOCITY, 0);
			else
				force = new Vector2 (INITIAL_VELOCITY, 0);
			direction = force.x;
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0, 0);
			GetComponent<Rigidbody2D> ().AddForce (force);
		}
	}
}
