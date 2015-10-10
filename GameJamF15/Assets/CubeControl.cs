using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {

	float lastDirection = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(new Vector3(-.1f, 0, 0));
			if (!GetComponent<playerAttack>().isAttacking())
				lastDirection = -1;
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(new Vector3(.1f, 0, 0));
			if (!GetComponent<playerAttack>().isAttacking())
				lastDirection = 1;
		}
		transform.localScale = new Vector3 (lastDirection, 1, 1);
	}

	public float getLastDir() {
		return lastDirection;
	}
}
