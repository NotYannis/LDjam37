using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MovieScript : MonoBehaviour {
    float movieTime;
    public Scene mainScene;
    AsyncOperation loadingMainscene;
    MovieTexture movie;
    public GameObject planchette;
    public GameObject canvas;

    Vector2 frameCount;

    // Use this for initialization
    void Start () {
        movie = ((MovieTexture)GetComponent<Renderer>().material.mainTexture);
        movieTime = movie.duration;

        loadingMainscene = SceneManager.LoadSceneAsync(1);
        loadingMainscene.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (loadingMainscene.progress == 0.9f)
        {
            movie.Play();

            Destroy(canvas);

            movieTime -= Time.deltaTime;

            if (movieTime <= 0.0f || Input.GetKeyDown(KeyCode.Escape))
            {
                loadingMainscene.allowSceneActivation = true;
            }
        }
        else
        {
            ++frameCount.x;
            ++frameCount.y;

            float xPosition = 100 * Mathf.Cos((Mathf.PI * 2) * frameCount.x / 400);
            float yPosition = 80 * Mathf.Sin((Mathf.PI * 2) * frameCount.y / 300);
            planchette.GetComponent<RectTransform>().localPosition = new Vector3(xPosition, yPosition, 0);
        }
	}
}
