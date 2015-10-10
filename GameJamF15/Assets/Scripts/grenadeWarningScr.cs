using UnityEngine;
using System.Collections;

public class grenadeWarningScr : MonoBehaviour {

	float timer = bombSpawner.BEFORE_GRENADE_MAX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			FindObjectOfType<bombSpawner>().CreateGrenade ();
			Destroy (gameObject);
		}
	}
}
