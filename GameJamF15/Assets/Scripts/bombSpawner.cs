using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Every so often, this class spawns a bomb
public class bombSpawner : MonoBehaviour {


	public const float SPAWN_X_LEFT = -13;
	public const float SPAWN_Y_BOMB = -3.5f;
	public const float SPAWN_Y_GRENADE = -1f;
	public const float SPAWN_X_RIGHT = 13;

	const float BETWEEN_BOMB_MAX = .5f;
	float betweenBombTimer = 0;
	const int RATIO = 100;

	List<GameObject> bombs = new List<GameObject> ();
	List<GameObject> grenades = new List<GameObject> ();

	const float BEFORE_GRENADE_MAX = 3f;
	float beforeGrenadeTimer = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (betweenBombTimer > 0)
			betweenBombTimer -= Time.deltaTime;
		else {
			if (Random.Range(0, RATIO) == 0) {
				CreateBomb();
			}
			else if (Random.Range(0, RATIO/3) == 0) {
				CreateGrenade ();
			}
		}
	}

	void CreateBomb() {
		Vector2 startPos;
		if (Random.Range (0, 2) == 0)
			startPos = new Vector2(SPAWN_X_LEFT, SPAWN_Y_BOMB);
		else
			startPos = new Vector2(SPAWN_X_RIGHT, SPAWN_Y_BOMB);
		GameObject b = (GameObject) Instantiate (Resources.Load ("preBomb"), startPos, Quaternion.identity);
		bombs.Add (b);
		betweenBombTimer = BETWEEN_BOMB_MAX;
	}

	void CreateGrenade() {
		bool canSpawnLeft = false;
		bool canSpawnRight = false;
		for (int i = 0; i < bombs.Count; i++) {
			if (bombs[i] == null) {
				bombs.RemoveAt(i);
				i--;
			}
			else if (bombs[i].GetComponent<bomb>().direction > 0) {
				canSpawnLeft = true;
			}
			else {
				canSpawnRight = true;
			}
		}
		Vector2 startPos = new Vector2(0, 0);
		if (Random.Range (0, 2) == 0 && canSpawnLeft)
			startPos = new Vector2(SPAWN_X_LEFT, SPAWN_Y_GRENADE);
		else if (canSpawnRight)
			startPos = new Vector2(SPAWN_X_RIGHT, SPAWN_Y_GRENADE);
		if (canSpawnLeft || canSpawnRight) {
			GameObject g = (GameObject)Instantiate (Resources.Load ("preGrenade"), startPos, Quaternion.identity);
			grenades.Add (g);
		}
	}
}
