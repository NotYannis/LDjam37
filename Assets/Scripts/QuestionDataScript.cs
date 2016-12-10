using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestionDataScript : MonoBehaviour {

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

    // Use this for initialization
    void Awake () {
        buttonList = new List<GameObject>();

        interfaceScript = GetComponent<MainInterfaceScript>();

        for (int i = 0; i < questionsData.Count; ++i)
        {
            GameObject interactiveObject = GameObject.Find("Views/InteractiveObjects/" + questionsData[i].activatingObject);
            interactiveObject.GetComponent<ActiveObject>().questions.Add(questionsData[i]);
        }
        currentQuestions = new List<Question>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddButton()
    {

    }

    public void ActivateQuestion(int index)
    {
        if (!interfaceScript.spiritTalking)
        {
            Question currentQuestion = currentQuestions[index];

            interfaceScript.OnOuijaCall(currentQuestion.hasAnswer, currentQuestion.isAffirmative);
            if(currentQuestion.activatingObject != null)
            {
                GameObject.Find("Views/InteractiveObjects/" + currentQuestion.activatingObject);
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
            buttonList[i].GetComponent<Button>().onClick.RemoveAllListeners();
            buttonList[i].GetComponent<Button>().onClick.AddListener(() => { ActivateQuestion(index); });
            Vector3 pos = buttonList[i].GetComponent<RectTransform>().localPosition;
            pos = new Vector3(
                    pos.x,
                    pos.y + buttonList[i].GetComponent<RectTransform>().rect.height + 5,
                    pos.z);
            buttonList[i].GetComponent<RectTransform>().localPosition = pos;
        }
    }
}
