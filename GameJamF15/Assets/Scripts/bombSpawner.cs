using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Every so often, this class spawns a bomb
public class bombSpawner : MonoBehaviour {


	public const float SPAWN_X_LEFT = -13;
	public const float SPAWN_X_RIGHT = 13;
	public const float SPAWN_X_LEFT_PARA = -3;
	public const float SPAWN_X_RIGHT_PARA = 3;

	public const float SPAWN_Y_BOMB = -3.85f;
	public const float SPAWN_Y_GRENADE = -1f;
	public const float SPAWN_Y_PARACHUTE = 7f;


	const float BETWEEN_BOMB_MAX = .5f;
	float betweenBombTimer = 0;
	const int RATIO_INIT = 150;
    int ratio = RATIO_INIT;
	int bounceRatio = 4;
	int parachuteRatio = 4;
    float timePassed = 0;

	List<GameObject> bombs = new List<GameObject> ();
	List<GameObject> grenades = new List<GameObject> ();

	public const float BEFORE_GRENADE_MAX = 3f;
	float beforeGrenadeTimer = 0;

	string grenadeLoc = "";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        ratio = RATIO_INIT - ((int)timePassed / 10);
		UpdateLists ();
		if (beforeGrenadeTimer > 0) {
			beforeGrenadeTimer -= Time.deltaTime;
		}
		if (betweenBombTimer > 0)
			betweenBombTimer -= Time.deltaTime;
		else {
			if (Random.Range(0, ratio) == 0) {
				if (Random.Range (0, parachuteRatio) == 0)
					CreateBomb ("preParachuteBomb");
				else if (Random.Range (0, bounceRatio) == 0)
					CreateBomb ("preBouncingBomb");
				else
					CreateBomb("preBomb");
			}
			else if (Random.Range(0, ratio * 2) == 0 && beforeGrenadeTimer <= 0 && grenades.Count == 0) {
				CreateGrenadeWarning();
			}
		}
	}

	void UpdateLists() {
		for (int i = 0; i < bombs.Count; i++) {
			if (bombs [i] == null) {
				bombs.RemoveAt (i);
				i--;
			}
		}
		for (int i = 0; i < grenades.Count; i++) {
			if (grenades [i] == null) {
				grenades.RemoveAt (i);
				i--;
			}
		}
	}

	public void CreateBomb(string type) {
		CreateBomb (type, "");
	}

	public void CreateBomb(string type, string dir) {
		Vector2 startPos;
		if ((Random.Range (0, 2) == 0 && dir != "right") || dir == "left") {
			if (type == "preParachuteBomb")
				startPos = new Vector2 (Random.Range (SPAWN_X_LEFT, SPAWN_X_LEFT_PARA), SPAWN_Y_PARACHUTE);
			else
				startPos = new Vector2 (SPAWN_X_LEFT, SPAWN_Y_BOMB);

		}
		else {
			if (type == "preParachuteBomb")
				startPos = new Vector2 (Random.Range (SPAWN_X_RIGHT_PARA, SPAWN_X_RIGHT), SPAWN_Y_PARACHUTE);
			else
				startPos = new Vector2(SPAWN_X_RIGHT, SPAWN_Y_BOMB);
		}
		GameObject b = (GameObject) Instantiate (Resources.Load (type), startPos, Quaternion.identity);
		bombs.Add (b);
		betweenBombTimer = BETWEEN_BOMB_MAX;
	}

	void CreateGrenadeWarning() {
		bool canSpawnLeft = false;
		bool canSpawnRight = false;
		for (int i = 0; i < bombs.Count; i++) {
			if (bombs[i].GetComponent<bomb>().direction > 0) {
				canSpawnLeft = true;
			}
			else {
				canSpawnRight = true;
			}
		}
		Vector2 startPos = new Vector2(0, 0);
		if (((Random.Range (0, 2) == 0 || !canSpawnRight) && canSpawnLeft)) {
			startPos = new Vector2 (SPAWN_X_LEFT + 3, SPAWN_Y_GRENADE);
			grenadeLoc = "left";
		} 
		else if (canSpawnRight) {
			startPos = new Vector2 (SPAWN_X_RIGHT - 3, SPAWN_Y_GRENADE);
			grenadeLoc = "right";
		}
		if (canSpawnLeft || canSpawnRight) {
			GameObject g = (GameObject)Instantiate (Resources.Load ("preGrenadeWarning"), startPos, Quaternion.identity);
			CreateBomb ("preBomb", grenadeLoc);
			beforeGrenadeTimer = BEFORE_GRENADE_MAX;
		}
	}

	public void CreateGrenade() {

		Vector2 startPos = new Vector2(0, 0);
		if (grenadeLoc == "left")
			startPos = new Vector2(SPAWN_X_LEFT, SPAWN_Y_GRENADE);
		else if (grenadeLoc == "right")
			startPos = new Vector2(SPAWN_X_RIGHT, SPAWN_Y_GRENADE);
		if (grenadeLoc != "") {
			GameObject g = (GameObject)Instantiate (Resources.Load ("preGrenade"), startPos, Quaternion.identity);
			grenades.Add (g);
		}
	}
}
