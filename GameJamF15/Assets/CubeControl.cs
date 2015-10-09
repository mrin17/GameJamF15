using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(new Vector3(-.1f, 0, 0));
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(new Vector3(.1f, 0, 0));
		}
	}
}
