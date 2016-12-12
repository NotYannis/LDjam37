using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestionDataScript : MonoBehaviour {
    protected SoundEffectsHelper soundEffects;
    public GameObject buttonPrefab;
    private MainInterfaceScript interfaceScript;
    private GameObject scripts; 

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
            if(GameObject.Find("Views/InteractiveObjects/" + questionsData[i].activatingObject) != null){
                GameObject interactiveObject = GameObject.Find("Views/InteractiveObjects/" + questionsData[i].activatingObject);
                interactiveObject.GetComponent<ActiveObject>().questions.Add(questionsData[i]);
            }
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

    public void ActivateQuestion(GameObject button)
    {
        if (interfaceScript.GetSpiritTalking() == false && interfaceScript.GetIsAskingQuestion() == false)
        {
            int index = buttonList.IndexOf(button);
            Question currentQuestion = currentQuestions[index];
            if(questionsData.IndexOf(currentQuestion) == 69)
            {
                interfaceScript.SetIsLastQuestion(true);
            }
            float clipTime = soundEffects.MakeQuestionVoices(questionsData.IndexOf(currentQuestion));
            Debug.Log(clipTime);
            Debug.Log(questionsData.IndexOf(currentQuestion));

            interfaceScript.SetIsAskingQuestion(true);
            interfaceScript.SetHasAnswer(currentQuestion.hasAnswer);
            interfaceScript.SetIsAffirmative(currentQuestion.isAffirmative);
            interfaceScript.Invoke("OnOuijaCall", clipTime); //Add clipaudio timer

            if (currentQuestion.activatedObject != "")
            {
                interfaceScript.SetUnlockSomething(true);
                if (currentQuestion.activatedObject == "FirstQuestions")
                {
                    for(int i = 1; i < 5; ++i)
                    {
                        GameObject but = Instantiate(buttonPrefab) as GameObject;

                        but.GetComponent<Button>().onClick.AddListener(() => { ActivateQuestion(but); });
                        but.transform.SetParent(GameObject.Find("MainInterface/Menu/Scroll View/Viewport/Content").GetComponent<Transform>());

                        float buttonYPos = -20 - (buttonList.Count * (but.GetComponent<RectTransform>().rect.height + 5));
                        but.transform.localScale = new Vector3(1, 1, 1);
                        but.GetComponentInChildren<Text>().text = questionsData[i].question;
                        but.GetComponent<RectTransform>().localPosition = new Vector3(button.GetComponent<RectTransform>().rect.width / 2 + 12, buttonYPos, 0.0f);

                        buttonList.Add(but);
                        currentQuestions.Add(questionsData[i]);
                    }
                }
                else
                {
                    GameObject.Find("Views/InteractiveObjects/" + currentQuestion.activatedObject).GetComponent<ActiveObject>().enabled = true;
                }

                //Last object case
                if (currentQuestion.activatedObject == "DiskEnding")
                {
                    GameObject.Find("DiskEnding").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("DiskEndingButton").SetActive(true);
                    GameObject.Find("DiskEnding").SetActive(false);
                    GameObject.Find("DiskTutoButton").SetActive(false);
                }
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
