using UnityEngine;
using System.Collections;

public class thrownRock : MonoBehaviour {

	float maxy = 0;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, 1050));
		maxy = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > maxy)
			maxy = transform.position.y;
	}

	void OnCollisionEnter2D(Collision2D other) {
		Destroy (gameObject);
	}

	void OnDestroy() {
		Debug.Log ("MAX Y "+maxy);
	}
}
