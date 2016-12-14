using UnityEngine;
using System.Collections;

public class MovieEndScript : MonoBehaviour {
    float movieTime;

    Vector2 frameCount;

    // Use this for initialization
    void Start()
    {
        MovieTexture movie = ((MovieTexture)GetComponent<Renderer>().material.mainTexture);
        movie.Play();
        movieTime = movie.duration;
    }

    // Update is called once per frame
    void Update()
    {
        movieTime -= Time.deltaTime;
        if (movieTime <= 0.0f)
        {
            Application.Quit();
        }
    }
}