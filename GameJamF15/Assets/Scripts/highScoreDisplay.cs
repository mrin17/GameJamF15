using UnityEngine;
using System.Collections;

public class highScoreDisplay : MonoBehaviour {

    // Use this for initialization
    void Start() {
        FindObjectOfType<getScoreScript>().calcHighScore();
        float score = FindObjectOfType<getScoreScript>().getHighScore();
        GetComponent<TextMesh>().text = "High Score:"+ score.ToString();
    }
	
}
