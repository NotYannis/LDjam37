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
    void Start () {
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
        Debug.Log(index);

        Question currentQuestion = currentQuestions[index];

        interfaceScript.OnOuijaCall(currentQuestion.hasAnswer, currentQuestion.isAffirmative);
        if(currentQuestion.activatingObject != null)
        {
            GameObject.Find("Views/InteractiveObjects/" + currentQuestion.activatingObject);
        }
        Debug.Log("kiki");
        currentQuestions.Remove(currentQuestion);
        Debug.Log("koukou");
        buttonList.RemoveAt(index);

//            Destroy(target);
    }
}
