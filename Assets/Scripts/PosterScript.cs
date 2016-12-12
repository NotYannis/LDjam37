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
            animCooldown = Random.Range(8.0f, 10.0f);
            int rand = (int)Random.Range(0, 2);
            if (rand == 0)
            {
                posterAnim.SetTrigger("dir1");
            }
            else
            {
                posterAnim.SetTrigger("dir2");
            }
        }
        else
        {
            posterAnim.ResetTrigger("dir1");
            posterAnim.ResetTrigger("dir2");
            animCooldown -= Time.deltaTime;
        }
    }

    public void PosterVener()
    {
        poster.GetComponent<Animator>().enabled = true;
    }
}
