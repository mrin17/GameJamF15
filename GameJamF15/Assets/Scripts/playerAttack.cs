using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	readonly string KICK_ATTACK = "z";
	readonly string LIFT_ATTACK = "x";
	readonly string OVERHEAD_ATTACK = "c";

	const float KICK_MAX = .2f;
	const float LIFT_MAX = .1f;
	const float OVERHEAD_MAX = .5f;
	const float KICK_INTRO_MAX = .12f;
	const float LIFT_INTRO_MAX = .0001f;
	const float OVERHEAD_INTRO_MAX = .0001f;
	const float KICK_COOL_MAX = .0001f;
	const float LIFT_COOL_MAX = .3f;
	const float OVERHEAD_COOL_MAX = .0001f;
	float attackTimer = 0;
	float attackIntroTimer = 0;
	float attackCooldownTimer = 0;

	GameObject attack;
	string attackType = ""; //kick, lift, overhead

	public const float MAX_HEALTH = 5;
	float health = MAX_HEALTH;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//time before the attack is created
		if (attackIntroTimer > 0) {
			attackIntroTimer -= Time.deltaTime;
		}
		//you cant attack while youre attacking
		else if (attackTimer > 0) {
			if (attackIntroTimer < 0) {
				attackIntroTimer = 0;
				//create attack
				if (attackType == "kick")
					attack = (GameObject) Instantiate (Resources.Load ("prePushAttack"), transform.position, Quaternion.identity);
				else if (attackType == "lift")
					attack = (GameObject) Instantiate (Resources.Load ("preLiftAttack"), transform.position, Quaternion.identity);
				else if (attackType == "overhead")
					attack = (GameObject) Instantiate (Resources.Load ("preThrownRock"), transform.position + new Vector3(0, .5f, 0), Quaternion.identity);
			}
			attackTimer -= Time.deltaTime;
		}
		else if (attackCooldownTimer > 0) {
			if (attackTimer < 0) {
				attackTimer = 0;
				Destroy (attack);
			}
			attackCooldownTimer -= Time.deltaTime;
		}
		//attack keys
		else if (Input.GetKeyDown (KICK_ATTACK)) {
			attackTimer = KICK_MAX;
			attackIntroTimer = KICK_INTRO_MAX;
			attackCooldownTimer = KICK_COOL_MAX;
			//SET KICK ANIM
			attackType = "kick";
			anim.SetInteger("attackType", 1);
			//CREATE SOME SORT OF OBJ (after introtimer is done)

		}
		else if (Input.GetKeyDown (LIFT_ATTACK)) {
			attackTimer = LIFT_MAX;
			attackIntroTimer = LIFT_INTRO_MAX;
			attackCooldownTimer = LIFT_COOL_MAX;
			attackType = "lift";
			//SET LIFT ANIM
			anim.SetInteger("attackType", 2);
			//CREATE SOME SORT OF OBJ (after introtimer is done)

		}
		else if (Input.GetKeyDown (OVERHEAD_ATTACK)) {
			attackTimer = OVERHEAD_MAX;
			attackIntroTimer = OVERHEAD_INTRO_MAX;
			attackCooldownTimer = OVERHEAD_COOL_MAX;
			attackType = "overhead";
			//we dont need to set an anim for this
			//CREATE SOME SORT OF OBJ (after introtimer is done)
		}

		if (attack != null && attackType != "overhead") {
			//it has to lock on to the same location
			float dir = GetComponent<CubeControl>().getLastDir();
			if (dir > 0)
				attack.transform.position = transform.position + new Vector3(.5f, -.35f, 0);
			else if (dir < 0)
				attack.transform.position = transform.position + new Vector3(-.5f, -.35f, 0);;
		}

		if (attackCooldownTimer < 0) {
			attackCooldownTimer = 0;
			attackType = "";
			anim.SetInteger("attackType", 0);
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
