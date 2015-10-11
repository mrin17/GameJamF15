using UnityEngine;
using System.Collections;

//Mike - this is meant to be put on the camera to shake it once the method triggers
public class scrCameraShake : MonoBehaviour {

	float shakeTimer = 0;
	float initialZ;
	Vector3 offset = new Vector3(0, 0, 0);
	const float SHAKE_RATE = 1;

	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (shakeTimer > 0) {
			shakeTimer -= Time.deltaTime;
			transform.position = offset +
				new Vector3(Random.Range (-shakeTimer*SHAKE_RATE, shakeTimer*SHAKE_RATE), Random.Range (-shakeTimer*SHAKE_RATE, shakeTimer*SHAKE_RATE), 0);
		}
		else if (shakeTimer < 0) {
			shakeTimer = 0;
			transform.position = offset;
		}
	}

	//Mike - different levels of shaking
	public void Shake(float shake) {
		shakeTimer += shake;
	}

	public void stopShaking() {
		shakeTimer = 0;
	}

}
