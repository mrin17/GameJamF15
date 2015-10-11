using UnityEngine;
using System.Collections;

//Mike - this class represents a bomb
public class bomb : MonoBehaviour {

	protected Vector2 force = new Vector2(0, 0);
	protected const float INITIAL_VELOCITY = .03f;
	protected const float LIFT_FORCE_Y = .2f; //.165f
	protected const float GRAVITY_SCALE = 3; //2
	public float direction = 0;

	protected bool explodeOnAnyCollision = false;

	// Use this for initialization
	protected void Start () {
		if ((Vector2) transform.position == new Vector2(bombSpawner.SPAWN_X_LEFT, bombSpawner.SPAWN_Y_BOMB))
			force = new Vector2(INITIAL_VELOCITY, 0);
		else if ((Vector2) transform.position == new Vector2(bombSpawner.SPAWN_X_RIGHT, bombSpawner.SPAWN_Y_BOMB))
			force = new Vector2(-INITIAL_VELOCITY, 0);
		else if ((Vector2) transform.position == new Vector2(bombSpawner.SPAWN_X_LEFT, bombSpawner.SPAWN_Y_GRENADE))
			force = new Vector2(INITIAL_VELOCITY*5, 0);
		else if ((Vector2) transform.position == new Vector2(bombSpawner.SPAWN_X_RIGHT, bombSpawner.SPAWN_Y_GRENADE))
			force = new Vector2(-INITIAL_VELOCITY*5, 0);
		direction = force.x;
		GetComponent<Rigidbody2D> ().AddForce (force);
        if (direction > 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate (force);
		if (transform.position.x < bombSpawner.SPAWN_X_LEFT || transform.position.x > bombSpawner.SPAWN_X_RIGHT)
        {
            FindObjectOfType<playerAttack>().addToScore();
            Destroy(gameObject);
        }
	}

	//different types of collisions
	//1) another bomb, they both blow up
	//2) player's attack 1: the bomb rolls back in the opposite direction
	//3) player's attack 2: the bomb gets tossed in the air and goes in the opposite direction, not much horizontal
	//4) hits the beard/tower/whatever, and the player loses
	protected void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Bomb" || other.gameObject.tag == "Kevin" || other.gameObject.tag == "Explosion" || explodeOnAnyCollision || 
		    (other.gameObject.tag == "Player" && other.gameObject.GetComponent<playerAttack>().canExplode(gameObject))) {
			Explode ();
		}
		if (other.gameObject.tag == "Kevin") {
			other.gameObject.GetComponent<kevinMonument>().takeDamage();
		}
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<playerAttack>().takeDamage();
		}
        if (other.gameObject.tag == "Bomb" || other.gameObject.tag == "Explosion") {
            FindObjectOfType<playerAttack>().addToScore();
        }
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "PushAttack") {
			force = new Vector2(direction, 0) * -4;
		}
		if (other.gameObject.tag == "LiftAttack") {
			GetComponent<Rigidbody2D>().gravityScale = GRAVITY_SCALE;
			force = new Vector2(-direction, LIFT_FORCE_Y);
			explodeOnAnyCollision = true;
		}
		GetComponent<Rigidbody2D> ().velocity = new Vector2(0, 0);
		GetComponent<Rigidbody2D> ().AddForce (force);
		direction = -direction;
	}

	void Explode() {
		GameObject g = (GameObject) Instantiate (Resources.Load ("preExplosion"), new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
		Destroy (gameObject);
	}
}
