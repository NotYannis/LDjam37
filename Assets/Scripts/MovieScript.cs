using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MovieScript : MonoBehaviour {
    float movieTime;
    public bool isEnd;
	// Use this for initialization
	void Start () {
        MovieTexture movie = ((MovieTexture)GetComponent<Renderer>().material.mainTexture);
        movie.Play();
        movieTime = movie.duration;
	}
	
	// Update is called once per frame
	void Update () {
        movieTime -= Time.deltaTime;
        if(!isEnd && (movieTime <= 0.0f || Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadSceneAsync(1);
        }
        if (isEnd)
        {
            Application.Quit();
        }
	}
}
