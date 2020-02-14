using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class UI_UpdateAlphaMask : MonoBehaviour
{
    private Material imageMaterial;

    public float maskMin = 0.0f;
    public float maskMax = 0.5f;

    [SerializeField]
    private float desiredMask = 1;
    public float DesiredMask
    {
        get
        {
            return desiredMask;
        }
        set
        {
            desiredMask = Mathf.Clamp(value,maskMin,maskMax);
            UpdateMask();
        }
    }

    public float deltaDesiredMask
    {
        get
        {
            return (desiredMask-maskMin) / (maskMax-maskMin);
        }
        set
        {
            DesiredMask = Mathf.Lerp(maskMin, maskMax, value);
        }
    }

    private void Start()
    {
        Image imageClass = GetComponent<Image>();
        if (imageClass != null)
        {
            imageMaterial = imageClass.material;
        }
    }

    [ContextMenu("UpdateMask")]
    void UpdateMask()
    {
        if (imageMaterial == null) { Start(); return; }

        imageMaterial.SetFloat("_MaskCutoff", desiredMask);
    }

#if UNITY_EDITOR
    private void Update()
    {
        DesiredMask = desiredMask;
        UpdateMask();
    }
#endif
}
