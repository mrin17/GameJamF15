using UnityEngine;
using System.Collections;

//USE THIS TO CREATE AN EXPLOSION: GameObject g = (GameObject) Instantiate (Resources.Load ("preExplosion"));

//Mike
//this waits for the explosion to be finished until destroying it
public class explosionDestroyer : MonoBehaviour {

	ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (ps.isStopped)
			Destroy (gameObject);
	}
}
