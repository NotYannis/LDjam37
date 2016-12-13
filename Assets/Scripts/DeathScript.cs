using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {
    SoundEffectsHelper soundsEffects;
    public GameObject deathMenu;

	// Use this for initialization
	void Start () {
        soundsEffects = GameObject.Find("Sounds").GetComponent<SoundEffectsHelper>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Die()
    {
        Instantiate(deathMenu);
        soundsEffects.MakeDeadSound(Camera.main.transform.position);
    }
}
