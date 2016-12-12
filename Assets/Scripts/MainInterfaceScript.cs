using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainInterfaceScript : MonoBehaviour {
    SoundEffectsHelper soundEffects;
    HangManScript hangMan;

    GameObject menu;
    GameObject planchette;
    GameObject scripts;

    Vector3 toPositionMenu;
    Vector3 toPositionPlanchette;

    bool addHangManPart;
    public bool menuSlide;
    bool isMenuOpen;
    public bool spiritTalking;
    public bool hasAnswer;
    public bool isAffirmative;
    public bool isAskingQuestion;
    public bool isLastQuestion;
    public bool unlockSomething;

    public float planchetteMoveSpeed;
    public float menuMoveSpeed;

    public float spiritThinkingTime;
    private float spiritThinkingCooldown;

    private int xPeriod = 1;
    private int yPeriod = 1;
    private int frameCountx = 1;
    private int frameCounty = 1;
    private int planchetteAmplitudeX = 100;
    private int planchetteAmplitudeY = 80;


    // Use this for initialization
    void Start () {
        scripts = GameObject.Find("Scripts");
        menu = GameObject.Find("MainInterface/Menu");
        planchette = GameObject.Find("MainInterface/Menu/OuijaBoard/Planchette");
        hangMan = GameObject.Find("MainInterface/Menu/OuijaBoard/HangMan").GetComponent<HangManScript>();

        if (GameObject.Find("Sounds").GetComponent<SoundEffectsHelper>() != null)
        {
            soundEffects = GameObject.Find("Sounds").GetComponent<SoundEffectsHelper>();
        }

        toPositionMenu = new Vector3(0, 0, 0);
        spiritThinkingCooldown = spiritThinkingTime;
        soundEffects.MakeStartSound(Camera.main.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        //Move the planchette
        if (spiritTalking)
        {
            if(spiritThinkingCooldown >= 0.0f)
            {
                ++frameCountx;
                ++frameCounty;

                spiritThinkingCooldown -= Time.deltaTime;

                if (frameCountx == xPeriod)
                {
                    xPeriod = Random.Range(300, 400);
                    frameCountx = 1;
                }
                if(frameCounty == yPeriod)
                {
                    yPeriod = Random.Range(200, 300);
                    frameCounty = 1;
                }

                float xPosition = planchetteAmplitudeX * Mathf.Cos((Mathf.PI * 2) * frameCountx / xPeriod);
                float yPosition = planchetteAmplitudeY * Mathf.Sin((Mathf.PI * 2) * frameCounty / yPeriod);
                planchette.GetComponent<RectTransform>().localPosition = new Vector3(xPosition, yPosition, 0);
            }
            else
            {
                float speed = planchetteMoveSpeed * Time.deltaTime;
                Vector3 planchettePos = planchette.GetComponent<RectTransform>().localPosition;

                planchette.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(planchettePos, toPositionPlanchette, speed);
                if(planchette.GetComponent<RectTransform>().localPosition == toPositionPlanchette)
                {
                    spiritTalking = false;
                    spiritThinkingCooldown = spiritThinkingTime;
                    if (addHangManPart)
                    {
                        hangMan.EnableHangManPart();
                        addHangManPart = false;
                        soundEffects.MakeWrongQuestionSound(Camera.main.transform.position);
                    }
                    else
                    {
                        if (!isLastQuestion)
                        {
                            if (unlockSomething)
                            {
                                soundEffects.MakeUnlockSomethingSound(Camera.main.transform.position);
                                unlockSomething = false;
                            }
                            else
                            {
                                soundEffects.MakeQuestionUnlockSound(Camera.main.transform.position);
                            }
                        }
                        else
                        {
                            soundEffects.MakeEndSound(Camera.main.transform.position);
                            scripts.GetComponent<ScreenShake>().StartShaking();
                            scripts.GetComponent<PosterScript>().enabled = true;
                            scripts.GetComponent<PosterScript>().PosterVener();
                        }
                    }
                }
            }
        }

        //Slide the menu on the game
        if (menuSlide)
        {
            float speed = menuMoveSpeed * Time.deltaTime;
            Vector3 menuPosition = menu.GetComponent<RectTransform>().localPosition;

            menu.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(menuPosition, toPositionMenu, speed);
            if (menuPosition == toPositionMenu)
            {
                menuSlide = false;
            }
        }
	}

    public void OnMenuCall() {
        if (isMenuOpen)
        {
            toPositionMenu = new Vector3(0, -1080, 0);
        }
        else
        {
            toPositionMenu = new Vector3(0, 0, 0);
        }
        isMenuOpen = !isMenuOpen;
        menuSlide = true;
    }

    public void OnOuijaCall()
    {
        isAskingQuestion = false;

        if (hasAnswer)
        {

            if (isAffirmative)
            {
                toPositionPlanchette = new Vector3(-123, 39, 0);
            }
            else
            {
                toPositionPlanchette = new Vector3(125, 39, 0);
            }
            spiritTalking = true;
        }
        else
        {
            toPositionPlanchette = new Vector3(0, -60, 0);
            spiritTalking = true;
            addHangManPart = true;
        }


        //Initialize planchette position
        xPeriod = Random.Range(100, 300);
        yPeriod = Random.Range(50, 200);

        float xPosition = planchetteAmplitudeX * Mathf.Cos((Mathf.PI * 2) * frameCountx / xPeriod);
        float yPosition = planchetteAmplitudeY * Mathf.Sin((Mathf.PI * 2) * frameCounty / yPeriod);
        planchette.GetComponent<RectTransform>().localPosition = new Vector3(xPosition, yPosition, 0);

        soundEffects.MakeAnswerSpiritSound(Camera.main.transform.position);
    }
}
