using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {
	public Material push;
	public Material flip;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKey (KeyCode.Z)) {
			renderer.material = push;	
		}
    if (Input.GetKey (KeyCode.X)) {
			renderer.material = flip;	
		}
	if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(new Vector3(-.1f, 0, 0));
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(new Vector3(.1f, 0, 0));
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name.Equals ("Block")) {
			for(float x = 0; x < 1; x += .1f)
			{
				col.gameObject.transform.Translate (new Vector3 (-x, x, 0));
			}
		}
	}
}
