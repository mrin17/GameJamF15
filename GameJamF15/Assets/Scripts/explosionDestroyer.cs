using UnityEngine;
using System.Collections;

//USE THIS TO CREATE AN EXPLOSION: GameObject g = (GameObject) Instantiate (Resources.Load ("preExplosion"));

//Mike
//this waits for the explosion to be finished until destroying it
//also it destroys the collision box halfway through
public class explosionDestroyer : MonoBehaviour {

	ParticleSystem ps;
	float destroyCollisionBoxTimer = .5f;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (destroyCollisionBoxTimer > 0)
			destroyCollisionBoxTimer -= Time.deltaTime;
		else if (destroyCollisionBoxTimer < 0)
			GetComponent<CircleCollider2D> ().enabled = false;
		if (ps.isStopped)
			Destroy (gameObject);
	}
}
