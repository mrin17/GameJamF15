using UnityEngine;
using System.Collections;

public class kevinMonument : MonoBehaviour {

	float MAX_HEALTH = 10;
	float damageTaken = 0;
	SpriteRenderer mySR;
	Animator myAnim;
	SpriteRenderer childSR;
	Animator childAnim;
	bool gameOver = false;
	const float GAME_OVER_MAX = 2f;
	float gameOverTimer = 0;
	const float LOSE_MAX = .5f;
	float loseTimer = 0;

	// Use this for initialization
	void Start () {
		mySR = GetComponent<SpriteRenderer> ();
		childSR = transform.GetChild(0).GetComponent<SpriteRenderer> ();
		myAnim = GetComponent<Animator> ();
		childAnim = transform.GetChild(0).GetComponent<Animator> ();
		childAnim.SetInteger("level", 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			if (gameOverTimer > 0)
				gameOverTimer -= Time.deltaTime;
			else {
                Application.LoadLevel("gameOverScene");
                Destroy (gameObject);
			}
		}
	}

	public void takeDamage() {
		damageTaken++;
		childSR.color = new Color (1, 1, 1, (damageTaken % 3) * .33f);
		if (damageTaken % 3 == 0) {
			myAnim.SetInteger("level", (int) damageTaken / 3);
			//CHECK IF WE ARE AT 9
			if (damageTaken == 9)
				childSR.enabled = false;
			childAnim.SetInteger("level", (int) (damageTaken / 3) + 1);
		}
		if (damageTaken == 10) {
			myAnim.SetInteger ("level", 4);
			gameOverTimer = GAME_OVER_MAX;
			gameOver = true;
            FindObjectOfType<bombSpawner>().GetComponent<AudioSource>().Stop();
            FindObjectOfType<playerAttack>().playDeathSound();
        }
		FindObjectOfType<scrCameraShake> ().Shake (.5f);
	}
}
