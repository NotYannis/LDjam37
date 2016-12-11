using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HangManScript : MonoBehaviour {

    private List<GameObject> hangManDisabledParts;
    private List<GameObject> hangManEnabledParts;

	// Use this for initialization
	void Start () {
        hangManDisabledParts = new List<GameObject>();
        hangManEnabledParts = new List<GameObject>();

        GameObject hangMan = GameObject.Find("MainInterface/Menu/OuijaBoard/HangMan");
        for(int i = 0; i < hangMan.transform.childCount; ++i)
        {
            GameObject child = GameObject.Find("MainInterface/Menu/OuijaBoard/HangMan/HangPart" + i);
            if(child != null)
            {
                hangManDisabledParts.Add(child.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Scripts").GetComponent<DeathScript>().Die();
        }
	}

    public void EnableHangManPart()
    {
        hangManEnabledParts.Add(hangManDisabledParts[0]);
        hangManDisabledParts.RemoveAt(0);
        hangManEnabledParts[hangManEnabledParts.Count - 1].SetActive(true);

        if(hangManDisabledParts.Count == 0)
        {
            GameObject.Find("Scripts").GetComponent<DeathScript>().Die();
        }
    }
}
