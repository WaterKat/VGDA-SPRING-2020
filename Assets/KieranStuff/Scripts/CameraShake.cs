using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float minMag = 10f;
    private float maxMag = 50f;

    public bool boostShakeStarted = false;
    public bool boostShakeRunning = false;

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

            Debug.Log(elapsed);
            Debug.Log("SHAKING CAM");

            yield return null;
            Debug.Log("CONTUINING AFTER NULL");
        }

        transform.localPosition = originalPos;
    }

    public IEnumerator BoostStartShake(float duration, float magnitude)
    {
        boostShakeStarted = true;
        boostShakeRunning = true;

        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            Debug.Log(elapsed);
            Debug.Log("SHAKING CAM");

            yield return null;
            Debug.Log("CONTUINING AFTER NULL");
        }

        transform.localPosition = originalPos;
        boostShakeRunning = false;
    }

    public void BoostCameraShake(float magnitude)
    {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, 0f);
    }

    public void ResetShake()
    {
        transform.localScale = Vector3.zero;
        boostShakeStarted = false;
    }

}
