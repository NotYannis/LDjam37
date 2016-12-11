using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {
    SoundEffectsHelper soundsEffects;
    GameObject deathMenu;

	// Use this for initialization
	void Start () {
        deathMenu = GameObject.Find("DeathMenu");
        deathMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Die()
    {
        deathMenu.SetActive(true);
        deathMenu.GetComponent<EchapMenu>().isActive = true;
        deathMenu.GetComponent<EchapMenu>().ToggleChild(true);
        deathMenu.GetComponent<EchapMenu>().deathMenuFadeIn = true;
        deathMenu.GetComponent<EchapMenu>().menuFadeOut = false;
        //mainInterface.menuSlide = true;
    }
}
