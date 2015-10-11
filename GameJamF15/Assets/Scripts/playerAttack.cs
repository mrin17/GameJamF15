using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	readonly string PUSH_ATTACK = "z";
	readonly string LIFT_ATTACK = "x";
	readonly string OVERHEAD_ATTACK = "c";

	const float PUSH_MAX = .25f;
	const float LIFT_MAX = .25f;
	const float OVERHEAD_MAX = .5f;
	float attackTimer = 0;

	GameObject attack;
	bool overheadAttack = false;

	public const float MAX_HEALTH = 5;
	float health = MAX_HEALTH;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//you cant attack while youre attacking
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}
		//attack keys
		else if (Input.GetKeyDown (PUSH_ATTACK)) {
			attackTimer = PUSH_MAX;
			//CREATE SOME SORT OF OBJ
			attack = (GameObject) Instantiate (Resources.Load ("prePushAttack"), transform.position, Quaternion.identity);
			//SET PUSH ANIM
		}
		else if (Input.GetKeyDown (LIFT_ATTACK)) {
			attackTimer = LIFT_MAX;
			//CREATE SOME SORT OF OBJ
			attack = (GameObject) Instantiate (Resources.Load ("preLiftAttack"), transform.position, Quaternion.identity);
			//SET LIFT ANIM
		}
		else if (Input.GetKeyDown (OVERHEAD_ATTACK)) {
			attackTimer = OVERHEAD_MAX;
			//CREATE SOME SORT OF OBJ
			overheadAttack = true;
			attack = (GameObject) Instantiate (Resources.Load ("preThrownRock"), transform.position + new Vector3(0, .7f, 0), Quaternion.identity);
			//SET LIFT ANIM
		}

		if (attack != null && !overheadAttack) {
			//it has to lock on to the same location
			float dir = GetComponent<CubeControl>().getLastDir();
			if (dir > 0)
				attack.transform.position = transform.position + new Vector3(.75f, -.4f, 0);
			else if (dir < 0)
				attack.transform.position = transform.position + new Vector3(-.75f, -.4f, 0);;
		}

		if (attackTimer < 0) {
			attackTimer = 0;
			overheadAttack = false;
			Destroy (attack);
		}
	}

	public bool isAttacking() {
		return attack != null;
	}

	public void takeDamage() {
		health--;
		FindObjectOfType<scrCameraShake> ().Shake (.5f);
		FindObjectOfType<heartController> ().removeHeart ();
	}

	public float getHealth() { 
		return health; 
	}
}
