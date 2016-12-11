using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EchapMenu : MonoBehaviour {
    private Text[] menuText;
    private Image menuImage;

    private float targetImageAlpha = 0.8f;
    public bool isActive;
    public bool menuFadeIn;
    public bool menuFadeOut;
    public bool isDeathMenu;
    public bool deathMenuFadeIn;
    private int deathTextIndex = 0;

    public float backgroundFadeSpeed;
    public float textFadeSpeed;


    // Use this for initialization
    void Start () {
        menuImage = GetComponentInChildren<Image>();
        menuText = GetComponentsInChildren<Text>();
        isActive = false;
        ToggleChild(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) & !isDeathMenu)
        {
            if (!isActive)
            {
                isActive = true;
                ToggleChild(true);
                menuFadeIn = true;
                menuFadeOut = false;
            }
            else
            {
                menuFadeIn = false;
                menuFadeOut = true;
                fadeOut();
            }
        }

        if (menuFadeIn)
        {
            fadeIn();
        }
        if (menuFadeOut)
        {
            fadeOut();
        }
        if (deathMenuFadeIn)
        {
            deathFadeIn();
        }
	}

    private void fadeIn()
    {
        if(menuImage.color.a < targetImageAlpha)
        {
            menuImage.color = new Color(menuImage.color.r, menuImage.color.g, menuImage.color.b, menuImage.color.a + backgroundFadeSpeed);
        }

        if(menuText[menuText.Length - 1].color.a < 1)
        {
            for(int i = 0; i < menuText.Length; ++i)
            {
                menuText[i].color = new Color(menuText[i].color.r, menuText[i].color.g, menuText[i].color.b, menuText[i].color.a + textFadeSpeed);
            }
        }
        else
        {
            menuFadeIn = false;
        }
    }

    private void fadeOut()
    {
        if (menuImage.color.a > 0.0f)
        {
            menuImage.color = new Color(menuImage.color.r, menuImage.color.g, menuImage.color.b, menuImage.color.a - backgroundFadeSpeed);
        }
        if (menuText[0].color.a > 0.0f)
        {
            for (int i = 0; i < menuText.Length; ++i)
            {
                menuText[i].color = new Color(menuText[i].color.r, menuText[i].color.g, menuText[i].color.b, menuText[i].color.a - textFadeSpeed);
            }
        }
        else
        {
            isActive = false;
            ToggleChild(false);
            menuFadeOut = false;
        }
    }

    private void deathFadeIn()
    {
        if (menuImage.color.a < targetImageAlpha)
        {
            menuImage.color = new Color(menuImage.color.r, menuImage.color.g, menuImage.color.b, menuImage.color.a + backgroundFadeSpeed);
        }
        else if(deathTextIndex < menuText.Length)
        {
            if(menuText[deathTextIndex].color.a < 1)
            {
                menuText[deathTextIndex].color = new Color(menuText[deathTextIndex].color.r, menuText[deathTextIndex].color.g, menuText[deathTextIndex].color.b, menuText[deathTextIndex].color.a + textFadeSpeed);
            }
            else
            {
                ++deathTextIndex;
            }
        }
        
        if(deathTextIndex == menuText.Length)
        {

        }
    }

    public void ToggleChild(bool enable)
    {
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(enable);
        }
    }

    public void OnContinue()
    {
        menuFadeIn = false;
        menuFadeOut = true;
        fadeOut();
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCredits()
    {

    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
