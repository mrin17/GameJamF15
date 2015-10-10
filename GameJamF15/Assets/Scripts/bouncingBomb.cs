using UnityEngine;
using System.Collections;

public class bouncingBomb : bomb {

	protected new void Start() {
		base.Start ();
		GetComponent<Rigidbody2D> ().gravityScale = 2;
	}

	protected new void OnCollisionEnter2D(Collision2D other) {
		base.OnCollisionEnter2D (other);
		if (other.gameObject.tag == "Ground") {
			force = new Vector2 (direction, LIFT_FORCE_Y / 2f);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			GetComponent<Rigidbody2D> ().AddForce (force);
		}
	}
}
