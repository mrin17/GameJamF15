using UnityEngine;
using System.Collections;

public class grenadeWarningScr : MonoBehaviour {

	float timer = bombSpawner.BEFORE_GRENADE_MAX;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer % 1 < .1f) {
			sr.enabled = false;
		}
		else {
			sr.enabled = true;
		}
		if (timer < 0) {
			FindObjectOfType<bombSpawner>().CreateGrenade ();
			Destroy (gameObject);
		}
	}
}
