using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{

    private Renderer myrenderer;
        private MaterialPropertyBlock propblock;
    float offset = 0;
    [SerializeField]
    private float offsetmultiplier = 0.01f;
    private void Start()
    {
        myrenderer = GetComponent<Renderer>();

        propblock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        offset += offsetmultiplier*Time.deltaTime;

        propblock.SetVector("_MainTex_ST", new Vector4(5f, 5f, 0, offset));
        myrenderer.SetPropertyBlock(propblock);
    }
}
