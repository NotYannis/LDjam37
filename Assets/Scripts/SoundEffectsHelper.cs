using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour
{

    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffectsHelper Instance;

    public AudioSource[] answerSpiritSounds = new AudioSource[3];
    public AudioSource[] droneSounds = new AudioSource[3];
    public AudioSource questionUnlock;
    public AudioSource wrongQuestion;
    public AudioSource deadSound;




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

    public void MakeDroneSound(Vector3 position)
    {
        int sound = Random.Range(0, 3);
        MakeSound(droneSounds[sound], position, true);
    }

    public void MakeWrongQuestionSound(Vector3 position)
    {
        MakeSound(wrongQuestion, position, true);
    }

    public void MakeQuestionUnlockSound(Vector3 position)
    {
        MakeSound(questionUnlock, position, true);
    }

    public void MakeDeadSound(Vector3 position)
    {
        MakeSound(deadSound, position, true);
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
            //Debug.Log(soundPlaying.name);
            Destroy(soundPlaying);
        }
        else
        {
            GameObject soundPlaying = Instantiate(originalClip, position, Quaternion.identity, Camera.main.transform) as GameObject;
        }
    }
}
