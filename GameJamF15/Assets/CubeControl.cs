using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {

	float lastDirection = 1;
	bool walking = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(new Vector3(-.1f, 0, 0));
			if (!GetComponent<playerAttack>().isAttacking())
				lastDirection = -1;
			walking = true;
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(new Vector3(.1f, 0, 0));
			if (!GetComponent<playerAttack>().isAttacking())
				lastDirection = 1;
			walking = true;
		}
		else {
			walking = false;
		}
		transform.localScale = new Vector3 (lastDirection, 1, 1);
		anim.SetBool ("walking", walking);
	}

	public float getLastDir() {
		return lastDirection;
	}
}
