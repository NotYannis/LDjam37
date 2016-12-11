using UnityEngine;
using System.Collections;

public class InteractionObject : MonoBehaviour {

    QuestionDataScript questionData;
    SoundEffectsHelper soundEffects;

	// Use this for initialization
	void Start () {
        questionData = GameObject.Find("MainInterface").GetComponent<QuestionDataScript>();
        soundEffects = GameObject.Find("Sounds").GetComponent<SoundEffectsHelper>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InteractWithObject()
    {
        
        if (GetComponent<ActiveObject>().enabled)
        {
            soundEffects.MakeActivatedObjectSound(questionData.objectList.IndexOf(gameObject));
            GetComponent<ActiveObject>().AddQuestionToButtonList();
            gameObject.SetActive(false);
            GameObject.Find("RoomInterface/" + gameObject.name + "Button").SetActive(false);
        }
        else
        {
            soundEffects.MakeDesactivatedObjectSound(questionData.objectList.IndexOf(gameObject));
        }
    }

}
