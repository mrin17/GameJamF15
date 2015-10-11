using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class heartController : MonoBehaviour {

	List<GameObject> hearts = new List<GameObject>();
    private int lastExtraLifeScore = 0;
    private AudioSource source;
    private AudioClip lifeSound;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < playerAttack.MAX_HEALTH; i++) {
			GameObject h = (GameObject) Instantiate (Resources.Load ("preHeart"), new Vector3(-11.75f + 1.25f * i, 6.4f, 0), Quaternion.identity);
			hearts.Add (h);
		}
        source = GetComponent<AudioSource>();
        lifeSound = (AudioClip)Resources.Load("GetLife");
    }

	void Update() {
        float score = FindObjectOfType<playerAttack>().getScore();
        if ((score % 2000) == 0 && score > 0 && (int)score != lastExtraLifeScore)
        {
            lastExtraLifeScore = (int) score;
            source.PlayOneShot(lifeSound, 1f);
            addHeart();
        }
	}
	
	public void addHeart() {
            GameObject h = (GameObject)Instantiate(Resources.Load("preHeart"), new Vector3(-11.75f + 1.25f * (hearts.Count), 6.4f, 0), Quaternion.identity);
            hearts.Add(h);
	}

	public void removeHeart() {
		Destroy (hearts [hearts.Count - 1]);
		hearts.RemoveAt (hearts.Count - 1);
	}
}
