using UnityEngine;
using System.Collections;

public class PosterScript : MonoBehaviour {

    public GameObject poster;
    Animator posterAnim;

    private float animCooldown;

	// Use this for initialization
	void Start () {
        posterAnim = poster.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(animCooldown <= 0.0f)
        {
            animCooldown = Random.Range(20.0f, 30.0f);
            int rand = (int)Random.Range(0, 2);
            if (rand == 0)
            {
                posterAnim.SetBool("dir1", true);
            }
            else
            {
                posterAnim.SetBool("dir2", true);
            }
        }
        else
        {
            posterAnim.SetBool("dir1", false);
            posterAnim.SetBool("dir2", false);
            animCooldown -= Time.deltaTime;
        }
    }

    public void PosterVener()
    {
        poster.GetComponent<Animator>().enabled = true;
    }
}
