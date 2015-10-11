using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class heartController : MonoBehaviour {

	List<GameObject> hearts = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < playerAttack.MAX_HEALTH; i++) {
			GameObject h = (GameObject) Instantiate (Resources.Load ("preHeart"), new Vector3(-11.75f + 1.25f * i, 6.4f, 0), Quaternion.identity);
			hearts.Add (h);
		}
	}

	void Update() {
		if ((FindObjectOfType<playerAttack>().getScore() % 2000) == 0 && FindObjectOfType<playerAttack>().getScore() > 0)
			addHeart();
	}
	
	public void addHeart() {
		GameObject h = (GameObject) Instantiate (Resources.Load ("preHeart"), new Vector3(-11.75f + 1.25f * (hearts.Count), 6.4f, 0), Quaternion.identity);
		hearts.Add (h);
	}

	public void removeHeart() {
		Destroy (hearts [hearts.Count - 1]);
		hearts.RemoveAt (hearts.Count - 1);
	}
}
