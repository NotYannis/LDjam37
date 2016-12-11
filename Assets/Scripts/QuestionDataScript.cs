using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestionDataScript : MonoBehaviour {
    protected SoundEffectsHelper soundEffects;
    public GameObject buttonPrefab;
    private MainInterfaceScript interfaceScript;

    [System.Serializable]
    public struct Question
    {
        [Tooltip("Par quel objet la question est ajoutée ?")]
        public string activatingObject;
        public string question;
        public bool hasAnswer;
        public bool isAffirmative;
        [Tooltip("Quel objet la question active ?")]
        public string activatedObject;
    }

    public List<Question> questionsData;
    public List<Question> currentQuestions;
    public List<GameObject> buttonList;
    public List<GameObject> objectList;

    // Use this for initialization
    void Awake () {
        buttonList = new List<GameObject>();
        soundEffects = GameObject.Find("Sounds").GetComponent<SoundEffectsHelper>();
        interfaceScript = GetComponent<MainInterfaceScript>();

        //Fill all the interactive objects with the rigth data
        for (int i = 0; i < questionsData.Count; ++i)
        {
            GameObject interactiveObject = GameObject.Find("Views/InteractiveObjects/" + questionsData[i].activatingObject);
            interactiveObject.GetComponent<ActiveObject>().questions.Add(questionsData[i]);
        }
        currentQuestions = new List<Question>();

        //Fill an array with all the interactive objects
        GameObject interactiveObjects = GameObject.Find("Views/InteractiveObjects");
        for (int i = 0; i < interactiveObjects.transform.childCount; ++i)
        {
            Transform child = interactiveObjects.transform.GetChild(i);
            if(child != null)
            {
                objectList.Add(child.gameObject);
                child.GetComponent<ActiveObject>().enabled = false;

            }
        }

        objectList[0].GetComponent<ActiveObject>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActivateQuestion(GameObject button)
    {
        if (!interfaceScript.spiritTalking)
        {
            int index = buttonList.IndexOf(button);
            Question currentQuestion = currentQuestions[index];

            //soundEffects.MakeQuestionVoices(questionsData.IndexOf(currentQuestion));
            interfaceScript.OnOuijaCall(currentQuestion.hasAnswer, currentQuestion.isAffirmative);
            if (currentQuestion.activatedObject != "")
            {
                GameObject.Find("Views/InteractiveObjects/" + currentQuestion.activatedObject).GetComponent<ActiveObject>().enabled = true;
            }

            currentQuestions.Remove(currentQuestion);
            DestroyButton(buttonList[index], index);
        }
    }

    public void DestroyButton(GameObject button, int index)
    {
        Destroy(button);
        buttonList.Remove(button);
        for(int i = index; i < buttonList.Count; ++i)
        {
            Vector3 pos = buttonList[i].GetComponent<RectTransform>().localPosition;
            pos = new Vector3(
                    pos.x,
                    pos.y + buttonList[i].GetComponent<RectTransform>().rect.height + 5,
                    pos.z);
            buttonList[i].GetComponent<RectTransform>().localPosition = pos;
        }
    }
}
