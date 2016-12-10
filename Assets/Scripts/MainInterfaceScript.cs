using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainInterfaceScript : MonoBehaviour {

    GameObject ouijaBoard;
    GameObject planchette;

    Vector3 toPositionOuija;
    Vector3 toPositionPlanchette;

    bool ouijaSlide;
    bool spiritTalking;

    public float planchetteMoveSpeed;
    public float ouijaMoveSpeed;

    public float spiritThinkingTime;
    private float spiritThinkingCooldown;

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

        OnOuijaCall(true, false);
    }
	
	// Update is called once per frame
	void Update () {
        if (ouijaSlide)
        {
            float speed = ouijaMoveSpeed * Time.deltaTime;
            Vector3 ouijaBoardPisition = ouijaBoard.GetComponent<RectTransform>().localPosition;

            ouijaBoard.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(ouijaBoardPisition, toPositionOuija, speed);
            if (ouijaBoardPisition == toPositionOuija)
            {
                spiritTalking = true;
                ouijaSlide = false;
            }
        }

        if (spiritTalking)
        {
            if(spiritThinkingCooldown >= 0.0f)
            {
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

                float xPosition = 100 * Mathf.Cos((Mathf.PI * 2) * frameCountx / xPeriod);
                float yPosition = 100 * Mathf.Sin((Mathf.PI * 2) * frameCounty / yPeriod);
                planchette.GetComponent<RectTransform>().localPosition = new Vector3(xPosition, yPosition, 0);

                ++frameCountx;
                ++frameCounty;
            }
            else
            {
                float speed = planchetteMoveSpeed * Time.deltaTime;
                Vector3 planchettePos = planchette.GetComponent<RectTransform>().localPosition;

                planchette.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(planchettePos, toPositionPlanchette, speed);
                if(planchette.transform.position == toPositionPlanchette)
                {
                    spiritTalking = false;
                }
            }
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
            ouijaSlide = true;
        }
    }
}
