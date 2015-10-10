using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	readonly string PUSH_ATTACK = "z";
	readonly string LIFT_ATTACK = "x";

	const float PUSH_MAX = .5f;
	float pushTimer = 0;

	const float LIFT_MAX = .5f;
	float liftTimer = 0;

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
		}
		else if (Input.GetKeyDown (LIFT_ATTACK)) {
			liftTimer = LIFT_MAX;
			//CREATE SOME SORT OF OBJ
		}
	}
}
