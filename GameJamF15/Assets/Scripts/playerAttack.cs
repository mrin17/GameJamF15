using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	readonly string PUSH_ATTACK = "z";
	readonly string LIFT_ATTACK = "x";

	const float PUSH_MAX = .25f;
	float pushTimer = 0;

	const float LIFT_MAX = .25f;
	float liftTimer = 0;

	GameObject attack;

	public const float MAX_HEALTH = 5;
	float health = MAX_HEALTH;
    public const float X_LIFE_SCORE = 1000;
    float score = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//you cant attack while youre attacking
		if (liftTimer > 0) {
			liftTimer -= Time.deltaTime;
		}
		else if (pushTimer > 0) {
			pushTimer -= Time.deltaTime;
		}
		//attack keys
		else if (Input.GetKeyDown (PUSH_ATTACK)) {
			pushTimer = PUSH_MAX;
			//CREATE SOME SORT OF OBJ
			attack = (GameObject) Instantiate (Resources.Load ("prePushAttack"), transform.position, Quaternion.identity);
			//SET PUSH ANIM
		}
		else if (Input.GetKeyDown (LIFT_ATTACK)) {
			liftTimer = LIFT_MAX;
			//CREATE SOME SORT OF OBJ
			attack = (GameObject) Instantiate (Resources.Load ("preLiftAttack"), transform.position, Quaternion.identity);
			//SET LIFT ANIM
		}
        if (attack != null) {
			//it has to lock on to the same location
			float dir = GetComponent<CubeControl>().getLastDir();
			if (dir > 0)
				attack.transform.position = transform.position + new Vector3(.75f, -.4f, 0);
			else if (dir < 0)
				attack.transform.position = transform.position + new Vector3(-.75f, -.4f, 0);;
		}

		if (liftTimer < 0 || pushTimer < 0) {
			liftTimer = 0;
			pushTimer = 0;
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
    public float getScore()
    {
        return score;
    }
    public void addToScore() {
        score += 100;
    }
}
