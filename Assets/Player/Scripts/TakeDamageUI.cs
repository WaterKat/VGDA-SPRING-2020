using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamageUI : MonoBehaviour
{
    public Image damageImage;
    private Color originalColor;
    public float fadeTime = .25f;

    public GameObject bubble;
    public Material damageMaterial;

    private Coroutine currentCoroutine;

    private void Start()
    {
        bubble.SetActive(false);
        originalColor = damageImage.color;
        damageImage.color = new Color(0, 0, 0, 0);
    }
    public void StartTakeDamageUI()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine("animateUI");
    }

    IEnumerator animateUI()
    {
        Debug.Log("DamageTaken");
        float duration = 0;
        bubble.SetActive(true);

        while (duration < fadeTime)
        {
            duration += Time.deltaTime;
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(originalColor.a, 0, duration / fadeTime);
            damageImage.color = newColor;
            damageMaterial.color = Color.Lerp(new Color(0, 1f, 1f, .5f), new Color(0, 1f, 1f, 0), duration / fadeTime);
            yield return null;
        }
        yield return null;
        damageImage.color = new Color(0, 0, 0, 0);
        bubble.SetActive(false);


    }
}
