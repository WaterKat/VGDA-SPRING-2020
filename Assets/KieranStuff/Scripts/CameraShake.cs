using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float minMag = 10f;
    private float maxMag = 50f;

    public bool startShakeFinished = true;
    public bool boostStarted = false;

    public float getMinMag()
    {
        return minMag;
    }

    public float getMaxMag()
    {
        return maxMag;
    }

    public void CallExplosionCoroutine(float duration, float magnitude, float distanceFromExplosion)
    {
        StartCoroutine(SingleShake(duration, Mathf.Clamp(magnitude / distanceFromExplosion, 0, magnitude)));
    }

    public IEnumerator SingleShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void CalledCameraShake(float[] inputs)
    {
        StartCoroutine(SingleShake(inputs[0], inputs[1]));
    }

    public IEnumerator BoostStartShake(float duration, float magnitude)
    {
       // Debug.Log("BOOST STARTED: " + boostStarted);
      //  Debug.Log("START BOOST FINISHED: " + startShakeFinished);

        if (boostStarted == false)
        {
            boostStarted = true;
            startShakeFinished = false;

            Vector3 originalPos = transform.localPosition;

            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);

                elapsed += Time.deltaTime;

                //Debug.Log(elapsed);

                yield return null;
            }

            transform.localPosition = originalPos;
            startShakeFinished = true;
        }
        else
        {
            yield return null;
        }
    }

    public void BoostCameraShake(float magnitude)
    {
        if (startShakeFinished)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, 0f);
        }
    }

    public void ResetShake()
    {
        transform.localScale = Vector3.zero;
        startShakeFinished = false;
        boostStarted = false;
    }

}
