using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour
{

    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffectsHelper Instance;

    public AudioSource[] answerSpiritSounds = new AudioSource[3];

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }

    public void MakeAnswerSpiritSound(Vector3 position)
    {
        int sound = Random.Range(0, 3);
        MakeSound(answerSpiritSounds[sound], position, true);
    }

    /// <summary>
    /// Play a given sound
    /// </summary>
    /// <param name="originalClip"></param>
    /// <param name="position"></param>

    private void MakeSound(AudioSource originalClip, Vector3 position, bool soundIn2D)
    {
        if (soundIn2D){
            GameObject soundPlaying = Instantiate(originalClip, Camera.main.transform, false) as GameObject;
            Destroy(soundPlaying);
        }
        else
        {
            GameObject soundPlaying = Instantiate(originalClip, position, Quaternion.identity, Camera.main.transform) as GameObject;
        }
        //AudioSource.PlayClipAtPoint(originalClip., position);
    }
}
