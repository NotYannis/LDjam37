using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ActiveObject : QuestionDataScript {

    private QuestionDataScript mainQuestions;
    public List<Question> questions;

    int startY = -20;
    int currentButtonIndex = 0;

    // Use this for initialization
    void Start () {
        mainQuestions = GameObject.Find("MainInterface").GetComponent<QuestionDataScript>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddQuestionToButtonList()
    {
        for(int i = 0; i < questions.Count; ++i)
        {
            List<GameObject> currentListbutton = mainQuestions.buttonList;
            GameObject button = Instantiate(mainQuestions.buttonPrefab) as GameObject;
            currentListbutton.Add(button);
            
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() => { ActivateQuestion(currentButtonIndex); });
            button.transform.SetParent(GameObject.Find("MainInterface/Menu/Scroll View/Viewport/Content").GetComponent<Transform>());

            float buttonYPos = startY - (currentListbutton.Count * (button.GetComponent<RectTransform>().rect.height + 5));
            button.transform.localScale = new Vector3(1, 1, 1);
            button.GetComponentInChildren<Text>().text = questions[i].question;
            button.GetComponent<RectTransform>().localPosition = new Vector3(button.GetComponent<RectTransform>().rect.width / 2 + 12, buttonYPos, 0.0f);

            mainQuestions.currentQuestions.Add(questions[i]);
            currentButtonIndex++;
        }
    }

   


}
