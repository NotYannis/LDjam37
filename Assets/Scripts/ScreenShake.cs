using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    float originalCameraPositionY;

    public float shakeAmt = 0;
    public bool shake = true;


    void Update()
    {
        if (shake)
        {
            CameraShake();
        }
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeYAmt = Random.value * shakeAmt * 2 - shakeAmt;
            float quakeXAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = Camera.main.transform.position;
            pp.y += quakeXAmt; // can also add to x and/or z
            pp.x += quakeYAmt;
            Camera.main.transform.position = pp;
        }
    }

    public void StartShaking()
    {
        shake = true;
        originalCameraPositionY = Camera.main.transform.position.y;
    }

    void StopShaking()
    {
        shake = false;
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, -9, Camera.main.transform.position.z);
    }

}
