using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class InteractionObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playAnimation()
    {
        Animator anim = GetComponent<Animator>();
        if(anim != null)
        {
            anim.SetBool("animate", true);
        }
        if (GetComponent<ActiveObject>().enabled)
        {
            GetComponent<ActiveObject>().AddQuestionToButtonList();
            GetComponent<ActiveObject>().enabled = false;
        }
    }

}
