using UnityEngine;
using System.Collections;

public class kevinMonument : MonoBehaviour {

	float damageTaken = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeDamage() {
		damageTaken++;
		FindObjectOfType<scrCameraShake> ().Shake (.5f);
	}
}
