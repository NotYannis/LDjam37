using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutoScript : MonoBehaviour {

    Text tuto;
    float tutotimer;

	// Use this for initialization
	void Start () {
        tuto = gameObject.GetComponent<Text>();
        tutotimer = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
        tutotimer -= Time.deltaTime;
        if(tuto.color.a > 0.0f && tutotimer < 0.0f)
        {
            tuto.color = new Color(255, 255, 255, tuto.color.a - 0.03f);
        }
        if(tuto.color.a <= 0.0f)
        {
            Destroy(gameObject);
        }
	}
}
