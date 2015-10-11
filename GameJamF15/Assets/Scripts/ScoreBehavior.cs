using UnityEngine;
using System.Collections;

public class ScoreBehavior : MonoBehaviour {
    TextMesh s;
	// Use this for initialization
	void Start () {
        s = ((GameObject)Instantiate(Resources.Load("preScore"), new Vector3(-4, -4, 10), Quaternion.identity)).GetComponent<TextMesh>();
        s.text = "Score: " + FindObjectOfType<getScoreScript>().getScore().ToString();
    }
	
	// Update is called once per frame
	void Update () {
        s.text = "Score: " + FindObjectOfType<getScoreScript>().getScore().ToString();
	}
}
