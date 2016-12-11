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
    public AudioSource[] objectActivatedSounds = new AudioSource[21];
    public AudioSource[] objectDesactivatedSounds = new AudioSource[21];
    public AudioSource[] questionVoices;
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

    public void MakeActivatedObjectSound(int index)
    {
        if(objectActivatedSounds[index] != null)
        {
            MakeSound(objectActivatedSounds[index], Camera.main.transform.position, true);
        }
    }

    public void MakeDesactivatedObjectSound(int index)
    {
        if(objectDesactivatedSounds[index] != null)
        {
            MakeSound(objectDesactivatedSounds[index], Camera.main.transform.position, true);
        }
    }

    public void MakeQuestionVoices(int index)
    {
        if(questionVoices[index] != null)
        {
           MakeSound(questionVoices[index], Camera.main.transform.position, true);
        }
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
        GameObject soundPlaying;
        float soundDuration = 0.0f;
        if (soundIn2D){
            soundPlaying = Instantiate(originalClip, Camera.main.transform, false) as GameObject;
            soundPlaying = GameObject.Find(originalClip.name + "(Clone)");
            soundDuration = originalClip.clip.length;
        }
        else
        {
            soundPlaying = Instantiate(originalClip, position, Quaternion.identity, Camera.main.transform) as GameObject;
            soundPlaying = GameObject.Find(originalClip.name + "(Clone)");
            soundDuration = originalClip.clip.length;
        }
        Destroy(soundPlaying, soundDuration);
    }
}
