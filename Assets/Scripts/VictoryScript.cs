using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryScript : MonoBehaviour {

    private float victoryTimer;
    public Image endScreen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        victoryTimer -= Time.deltaTime;

	}

    public void Victory()
    {
        victoryTimer = 5.0f;
        GameObject.Find("MainInterface").SetActive(false);
        
    }
}
