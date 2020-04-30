using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;

public class NextUI : MonoBehaviour
{
    private int current = 1;
    private GameObject currentUIObject;

    public AnimationClip enterAnimation;
    public AnimationClip exitAnimation;

    public string nextScene = "FinalLevel";

    public bool playing = false;

    [SerializeField]
    private float sceneSkipTime = 0;
    [SerializeField]
    private float sceneSkipMax = 1.5f;
    public Image skipImage;

    private void Start()
    {
        currentUIObject = transform.Find("Text" + current).gameObject;
        currentUIObject.SetActive(true);
        currentUIObject.GetComponent<Animation>().clip = enterAnimation;

        currentUIObject.GetComponent<Animation>().Play("enterAnimation");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!playing&&!currentUIObject.GetComponent<Animation>().isPlaying)
        {
            StartCoroutine("LoadNext") ;
        }
        if (Input.GetMouseButton(1))
        {
            sceneSkipTime += Time.deltaTime;
            if (sceneSkipTime > sceneSkipMax)
            {
                StartGame();
            }
        }
        else
        {
            if (sceneSkipTime < sceneSkipMax)
            {
                sceneSkipTime = Mathf.Max(sceneSkipTime - (Time.deltaTime * 5f), 0);
            }
        }
        skipImage.fillAmount = sceneSkipTime / sceneSkipMax;
    }

    IEnumerator LoadNext()
    {
        playing = true;
        currentUIObject.GetComponent<Animation>().clip = exitAnimation;
        currentUIObject.GetComponent<Animation>().Play("exitAnimation");
        while (currentUIObject.GetComponent<Animation>().isPlaying)
        {
            yield return null;
        }
        currentUIObject.SetActive(false);
        current++;
        Transform testTransform = transform.Find("Text" + current);
        if (testTransform == null)
        {
            StartGame();
            yield return new WaitForSeconds(5);
        }
        currentUIObject = testTransform.gameObject;

        currentUIObject.SetActive(true);
        currentUIObject.GetComponent<Animation>().Play("enterAnimation");
        playing = false;
    }

    void StartGame()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        FadeInOutScript.StartFade(nextScene);

    }
}
