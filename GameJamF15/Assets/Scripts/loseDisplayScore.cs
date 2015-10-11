using UnityEngine;
using System.Collections;

public class loseDisplayScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float score = FindObjectOfType<getScoreScript>().getScore();
        GetComponent<TextMesh>().text = score.ToString();
	}
	
}
