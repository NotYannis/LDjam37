using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffectsHelper : MonoBehaviour
{

    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffectsHelper Instance;

    public AudioSource questionUnlock;
    public AudioSource wrongQuestion;
    public AudioSource deadSound;
    public AudioSource startSound;
    public AudioSource endSound;
    public AudioSource unlockSomething;
    public AudioSource ambianceMusic;
    public AudioSource endMusic;
    public AudioSource firePaper;
    public AudioSource paperMouvement;
    public AudioSource[] answerSpiritSounds = new AudioSource[3];
    public AudioSource[] droneSounds = new AudioSource[3];
    public AudioSource[] objectActivatedSounds = new AudioSource[21];
    public AudioSource[] objectDesactivatedSounds = new AudioSource[21];
    public AudioSource[] questionVoices;
    public AudioSource[] jingleMusic = new AudioSource[9];



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
        MakeSound(answerSpiritSounds[sound], position, false);
    }

    public void MakeDroneSound(Vector3 position)
    {
        int sound = Random.Range(0, 3);
        MakeSound(droneSounds[sound], position, false);
    }

    public void MakeActivatedObjectSound(int index)
    {
        if(objectActivatedSounds[index] != null)
        {
            MakeSound(objectActivatedSounds[index], Camera.main.transform.position, false);
        }
    }

    public void MakeDesactivatedObjectSound(int index)
    {
        if(objectDesactivatedSounds[index] != null)
        {
            MakeSound(objectDesactivatedSounds[index], Camera.main.transform.position, false);
        }
    }

    public float MakeQuestionVoices(int index)
    {
        float clipTime = 0.0f;
        if(questionVoices[index] != null)
        {
           MakeSound(questionVoices[index], Camera.main.transform.position, false);
           clipTime = questionVoices[index].clip.length;
        }
        return clipTime;
    }

    public void MakeWrongQuestionSound(Vector3 position)
    {
        MakeSound(wrongQuestion, position, false);
    }

    public void MakeQuestionUnlockSound(Vector3 position)
    {
        MakeSound(questionUnlock, position, false);
    }

    public void MakeDeadSound(Vector3 position)
    {
        MakeSound(deadSound, position, false);
    }

    public void MakeStartSound(Vector3 position)
    {
        MakeSound(startSound, position, false);
    }

    public void MakeEndSound()
    {
        MakeSound(endSound, Camera.main.transform.position, false);
    }

    public void MakeUnlockSomethingSound(Vector3 position)
    {
        MakeSound(unlockSomething, position, false);
    }

    public void MakeEndMusic()
    {
        MakeSound(endMusic, Camera.main.transform.position, false);
    }

    public void MakeAmbianceMusic()
    {
        MakeSound(ambianceMusic, Camera.main.transform.position, true);
    }

    public void MakeJingleMusic(int index)
    {
        MakeSound(jingleMusic[index], Camera.main.transform.position, false);
    }

    public void MakePaperMouvement()
    {
        MakeSound(paperMouvement, Camera.main.transform.position, true);
    }

    public void MakePaperBurning()
    {
        MakeSound(firePaper, Camera.main.transform.position, true);
    }

    public void StopPaperMovement()
    {
        Destroy(GameObject.Find("paperMouvement(Clone)"));
        MakePaperBurning();
    }

    /// <summary>
    /// Play a given sound
    /// </summary>
    /// <param name="originalClip"></param>
    /// <param name="position"></param>

    private void MakeSound(AudioSource originalClip, Vector3 position, bool loop)
    {
        GameObject soundPlaying;
        float soundDuration = 0.0f;

        soundPlaying = Instantiate(originalClip, Camera.main.transform, false) as GameObject;
        soundPlaying = GameObject.Find(originalClip.name + "(Clone)");
        soundDuration = originalClip.clip.length;

        if(!loop)
        {
            Destroy(soundPlaying, soundDuration);
        }
    }
}
