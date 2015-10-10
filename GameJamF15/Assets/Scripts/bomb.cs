using UnityEngine;
using System.Collections;

//Mike - this class represents a bomb
public class bomb : MonoBehaviour {

	Vector2 force = new Vector2(0, 0);
	const float INITIAL_VELOCITY = .03f;
	public float direction = 0;

	// Use this for initialization
	void Start () {
		if ((Vector2) transform.position == new Vector2(bombSpawner.SPAWN_X_LEFT, bombSpawner.SPAWN_Y_BOMB))
			force = new Vector2(INITIAL_VELOCITY, 0);
		else if ((Vector2) transform.position == new Vector2(bombSpawner.SPAWN_X_RIGHT, bombSpawner.SPAWN_Y_BOMB))
			force = new Vector2(-INITIAL_VELOCITY, 0);
		else if ((Vector2) transform.position == new Vector2(bombSpawner.SPAWN_X_LEFT, bombSpawner.SPAWN_Y_GRENADE))
			force = new Vector2(INITIAL_VELOCITY*3, 0);
		else if ((Vector2) transform.position == new Vector2(bombSpawner.SPAWN_X_RIGHT, bombSpawner.SPAWN_Y_GRENADE))
			force = new Vector2(-INITIAL_VELOCITY*3, 0);
		direction = force.x;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (force);
	}

	//different types of collisions
	//1) another bomb, they both blow up
	//2) player's attack 1: the bomb rolls back in the opposite direction
	//3) player's attack 2: the bomb gets tossed in the air and goes in the opposite direction, not much horizontal
	//4) hits the beard/tower/whatever, and the player loses
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Bomb") {
			Explode ();
		}
	}

	void Explode() {
		GameObject g = (GameObject) Instantiate (Resources.Load ("preExplosion"), new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
		Destroy (gameObject);
	}
}
