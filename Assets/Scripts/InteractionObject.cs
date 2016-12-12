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

    private void InteractWithObject()
    {

        if (gameObject.GetComponent<ActiveObject>().enabled)
        {
            soundEffects.MakeActivatedObjectSound(questionData.objectList.IndexOf(gameObject));
            gameObject.GetComponent<ActiveObject>().AddQuestionToButtonList();
            gameObject.GetComponent<ActiveObject>().enabled = false;
        }
        else
        {
            soundEffects.MakeDesactivatedObjectSound(questionData.objectList.IndexOf(gameObject));
        }
        CreateSprite(gameObject.GetComponent<SpriteRenderer>());
    }

    private void CreateSprite(SpriteRenderer spr)
    {
        Vector3 pos = Camera.main.transform.position;
        SpriteRenderer objectFound = Instantiate(spr, new Vector3(pos.x, pos.y, 0.0f), Quaternion.identity) as SpriteRenderer;
        objectFound.transform.localScale = new Vector3(1, 1, 1);
        Destroy(objectFound.gameObject, 2.0f);
    }
}
