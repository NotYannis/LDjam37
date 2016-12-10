using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CameraScript : MonoBehaviour {
    private SoundEffectsHelper soundEffects;
    private Vector3[] viewPositions = new Vector3[4];

    private int currentView = 1;
    public int droneSoundProb;

	// Use this for initialization
	void Start () {
        soundEffects = GameObject.Find("Sounds").GetComponent<SoundEffectsHelper>();

        GameObject[] views = new GameObject[4];
        views[0] = GameObject.Find("FirstView");
        views[1] = GameObject.Find("SecondView");
        views[2] = GameObject.Find("ThirdView");
        views[3] = GameObject.Find("FourthView");

        for(int i = 0; i < 4; ++i)
        {
            SpriteRenderer sprite = views[i].GetComponent<SpriteRenderer>();
            Vector3 view = new Vector3(views[i].transform.position.x + (sprite.bounds.size.x / 2),
                                        views[i].transform.position.y + (sprite.bounds.size.y / 2), -10.0f);
            viewPositions[i] = view;
        }

        Camera.main.transform.position = viewPositions[1];
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void GoRight()
    {
        if (Random.Range(0, droneSoundProb) == 1) soundEffects.MakeDroneSound(Camera.main.transform.position);
        ResetSelected();

        currentView++;
        if(currentView >= 3)
        {
            currentView = 0;
        }

        Vector3 nextView = new Vector3(viewPositions[currentView].x, viewPositions[currentView].y, -10.0f);
        Camera.main.transform.position = nextView;
    }

    public void GoLeft()
    {
        if (Random.Range(0, droneSoundProb) == 1) soundEffects.MakeDroneSound(Camera.main.transform.position);
        ResetSelected();

        currentView--;
        if (currentView < 0)
        {
            currentView = 2;
        }

        Vector3 nextView = new Vector3(viewPositions[currentView].x, viewPositions[currentView].y, -10.0f);
        Camera.main.transform.position = nextView;
    }

    public void GoDown()
    {
        if (Random.Range(0, droneSoundProb) == 1) soundEffects.MakeDroneSound(Camera.main.transform.position);
        Camera.main.transform.position = viewPositions[3];
        GameObject.Find("MainInterface/NavigationButtons/Left").SetActive(false);
        GameObject.Find("MainInterface/NavigationButtons/Right").SetActive(false);
        GameObject.Find("MainInterface/NavigationButtons/Down").SetActive(false);
        GameObject.Find("MainInterface/NavigationButtons/Up").SetActive(true);
    }

    public void GoUp()
    {
        if (Random.Range(0, droneSoundProb) == 1) soundEffects.MakeDroneSound(Camera.main.transform.position);
        Camera.main.transform.position = viewPositions[currentView];
        GameObject.Find("MainInterface/NavigationButtons/Left").SetActive(true);
        GameObject.Find("MainInterface/NavigationButtons/Right").SetActive(true);
        GameObject.Find("MainInterface/NavigationButtons/Down").SetActive(true);
        GameObject.Find("MainInterface/NavigationButtons/Up").SetActive(false);
    }

    private void ResetSelected()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        if(eventSystem != null)
        {
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
}
