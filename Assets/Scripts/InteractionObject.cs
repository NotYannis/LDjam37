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
            CreateSprite(GetComponent<SpriteRenderer>());
            GameObject.Find("RoomInterface/" + gameObject.name + "Button").SetActive(false);
        }
        else
        {
            soundEffects.MakeDesactivatedObjectSound(questionData.objectList.IndexOf(gameObject));
        }
    }

    private void CreateSprite(SpriteRenderer spr)
    {
        Vector3 pos = Camera.main.transform.position;
        SpriteRenderer objectFound = Instantiate(spr, new Vector3(pos.x, pos.y, 0.0f), Quaternion.identity) as SpriteRenderer;
        objectFound.transform.localScale = new Vector3(1, 1, 1);
        Destroy(objectFound, 2.0f);
    }
}
