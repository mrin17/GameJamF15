using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Every so often, this class spawns a bomb
public class bombSpawner : MonoBehaviour {


	public const float SPAWN_X_LEFT = -13;
	public const float SPAWN_Y = -3.5f;
	public const float SPAWN_X_RIGHT = 13;

	const float BETWEEN_BOMB_MAX = .5f;
	float betweenBombTimer = 0;
	const int RATIO = 100;

	List<GameObject> bombs = new List<GameObject> ();

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
		}
	}

	void CreateBomb() {
		Vector2 startPos;
		if (Random.Range (0, 2) == 0)
			startPos = new Vector2(SPAWN_X_LEFT, SPAWN_Y);
		else
			startPos = new Vector2(SPAWN_X_RIGHT, SPAWN_Y);
		GameObject b = (GameObject) Instantiate (Resources.Load ("preBomb"), startPos, Quaternion.identity);
		bombs.Add (b);
	}
}
