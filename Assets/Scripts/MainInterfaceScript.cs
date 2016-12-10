using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainInterfaceScript : MonoBehaviour {

    GameObject ouijaBoard;
    GameObject planchette;

    Vector3 toPositionOuija;
    Vector3 toPositionPlanchette;

    bool ouijaSlideOn;
    bool spiritTalking;
    bool ouijaSlideOff;

    public float planchetteMoveSpeed;
    public float ouijaMoveSpeed;

    public float spiritThinkingTime;
    private float spiritThinkingCooldown;

    public float ouijaStayTime;
    private float ouijaStayCooldown;

    private int xPeriod = 1;
    private int yPeriod = 1;
    private int frameCountx = 1;
    private int frameCounty = 1;


    // Use this for initialization
    void Start () {
        ouijaBoard = GameObject.Find("MainInterface/OuijaBoard");
        planchette = GameObject.Find("MainInterface/OuijaBoard/Planchette");

        toPositionOuija = new Vector3(0, -261, 0);
        spiritThinkingCooldown = spiritThinkingTime;
        ouijaStayCooldown = ouijaStayTime;

        OnOuijaCall(true, false);
    }
	
	// Update is called once per frame
	void Update () {
        //Slide the ouija board on the game
        if (ouijaSlideOn)
        {
            float speed = ouijaMoveSpeed * Time.deltaTime;
            Vector3 ouijaBoardPosition = ouijaBoard.GetComponent<RectTransform>().localPosition;

            ouijaBoard.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(ouijaBoardPosition, toPositionOuija, speed);
            if (ouijaBoardPosition == toPositionOuija)
            {
                spiritTalking = true;
                ouijaSlideOn = false;
                toPositionOuija = new Vector3(0, -500, 0);
            }
        }

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
                    ouijaSlideOff = true;
                    spiritThinkingCooldown = spiritThinkingTime;
                }
            }
        }


        if (ouijaSlideOff)
        {
            if(ouijaStayCooldown >= 0.0f)
            {
                ouijaStayCooldown -= Time.deltaTime;
            }
            else
            {
                float speed = ouijaMoveSpeed * Time.deltaTime;
                Vector3 ouijaBoardPosition = ouijaBoard.GetComponent<RectTransform>().localPosition;

                ouijaBoard.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(ouijaBoardPosition, toPositionOuija, speed);
                if (ouijaBoard.GetComponent<RectTransform>().localPosition == toPositionOuija)
                {
                    ouijaSlideOff = false;
                    toPositionOuija = new Vector3(0, -261, 0);
                    ouijaStayCooldown = ouijaStayTime;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnOuijaCall(false, false);
        }
	}

    public void OnOuijaCall(bool hasAnswer, bool isAffirmative)
    {
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
            ouijaSlideOn = true;
        }
        else
        {
            toPositionPlanchette = new Vector3(0, -175, 0);
            ouijaSlideOn = true;
        }

        //Initialize planchette position
        xPeriod = Random.Range(50, 200);
        yPeriod = Random.Range(50, 200);

        float xPosition = 70 * Mathf.Cos((Mathf.PI * 2) * frameCountx / xPeriod);
        float yPosition = 70 * Mathf.Sin((Mathf.PI * 2) * frameCounty / yPeriod);
        planchette.GetComponent<RectTransform>().localPosition = new Vector3(xPosition, yPosition, 0);
    }
}
