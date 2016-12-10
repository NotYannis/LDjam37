using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainInterfaceScript : MonoBehaviour {
    SoundEffectsHelper soundEffects;

    GameObject menu;
    GameObject ouijaBoard;
    GameObject planchette;

    Vector3 toPositionMenu;
    Vector3 toPositionPlanchette;

    bool menuSlide;
    bool isMenuOpen;
    bool spiritTalking;

    public float planchetteMoveSpeed;
    public float menuMoveSpeed;

    public float spiritThinkingTime;
    private float spiritThinkingCooldown;

    public float menuStayTime;
    private float menuStayCooldown;

    private int xPeriod = 1;
    private int yPeriod = 1;
    private int frameCountx = 1;
    private int frameCounty = 1;


    // Use this for initialization
    void Start () {
        menu = GameObject.Find("MainInterface/Menu");
        ouijaBoard = GameObject.Find("MainInterface/Menu/OuijaBoard");
        planchette = GameObject.Find("MainInterface/Menu/OuijaBoard/Planchette");

        if (GameObject.Find("Sounds").GetComponent<SoundEffectsHelper>() != null)
        {
            soundEffects = GameObject.Find("Sounds").GetComponent<SoundEffectsHelper>();
        }

        toPositionMenu = new Vector3(0, -289, 0);
        spiritThinkingCooldown = spiritThinkingTime;
        menuStayCooldown = menuStayTime;
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
                    xPeriod = Random.Range(50, 200);
                    frameCountx = 1;
                }
                if(frameCounty == yPeriod)
                {
                    yPeriod = Random.Range(50, 200);
                    frameCounty = 1;
                }

                float xPosition = 80 * Mathf.Cos((Mathf.PI * 2) * frameCountx / xPeriod);
                float yPosition = 80 * Mathf.Sin((Mathf.PI * 2) * frameCounty / yPeriod);
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
            toPositionMenu = new Vector3(0, -787, 0);
        }
        else
        {
            toPositionMenu = new Vector3(0, -289, 0);
        }
        isMenuOpen = !isMenuOpen;
        menuSlide = true;
    }

    public void OnOuijaCall(bool hasAnswer, bool isAffirmative)
    {
        soundEffects.MakeAnswerSpiritSound(Camera.main.transform.position);
        if (hasAnswer)
        {
            if (isAffirmative)
            {
                toPositionPlanchette = new Vector3(-138, 114, 0);
            }
            else
            {
                toPositionPlanchette = new Vector3(178, 114, 0);
            }
            spiritTalking = true;
        }
        else
        {
            toPositionPlanchette = new Vector3(0, -175, 0);
        }

        //Initialize planchette position
        xPeriod = Random.Range(50, 200);
        yPeriod = Random.Range(50, 200);

        float xPosition = 70 * Mathf.Cos((Mathf.PI * 2) * frameCountx / xPeriod);
        float yPosition = 70 * Mathf.Sin((Mathf.PI * 2) * frameCounty / yPeriod);
        planchette.GetComponent<RectTransform>().localPosition = new Vector3(xPosition, yPosition, 0);
    }
}
