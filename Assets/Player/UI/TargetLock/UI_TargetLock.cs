using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TargetLock : MonoBehaviour
{
    public Transform targetTransform;
    private Renderer targetRenderer;

    public float RelativeSize = 12;
    public float MaximumSize = 1;
    public float MinimumSize = 0.25f;
    private RectTransform rectTransform;
    private Vector2 tempSize = new Vector2(100, 100);


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetRenderer = targetTransform.GetComponent<Renderer>();
        if (targetRenderer == null)
        {
            targetRenderer = targetTransform.GetComponentInChildren<Renderer>();
        }
    }

    void LateUpdate()
    {
        Vector3 TargetPosition;
        if (targetRenderer != null)
        {
            TargetPosition = Camera.main.WorldToScreenPoint(targetRenderer.bounds.center);

            //   Vector2 minPoint = Camera.main.WorldToScreenPoint(Quaternion.Inverse(Camera.main.transform.rotation) * targetRenderer.bounds.min);
            // Vector2 maxPoint = Camera.main.WorldToScreenPoint(targetRenderer.bounds.max);
        }
        else
        {
            TargetPosition = targetTransform.position;
        }

        rectTransform.position = TargetPosition;
        rectTransform.localScale = Vector2.one * RelativeSize * Mathf.Clamp(targetTransform.localScale.magnitude / Vector3.Distance(Camera.main.transform.position, targetRenderer.bounds.center), MinimumSize / 10, MaximumSize / 10);
    }
}
