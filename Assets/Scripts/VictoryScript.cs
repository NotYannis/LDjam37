using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        if(victoryTimer <= 0.0f)
        {
            endScreen.color = new Color(0, 0, 0, endScreen.color.a + 0.001f);
            if(endScreen.color.a >= 1.0f)
            {
                SceneManager.LoadScene(2);
            }
        }
	}

    public void Victory()
    {
        victoryTimer = 1.0f;
        GameObject.Find("MainInterface").SetActive(false);
    }
}
