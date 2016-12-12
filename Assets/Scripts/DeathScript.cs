using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {
    SoundEffectsHelper soundsEffects;
    public GameObject deathMenu;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Die()
    {
        Instantiate(deathMenu);        
    }
}
