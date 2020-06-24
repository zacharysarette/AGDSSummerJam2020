using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackgroundController : MonoBehaviour
{
    [SerializeField]
    [Range(-1.0f, 1.0f)]
    private float speed = -0.5f;

    [SerializeField]
    private Renderer meshRenderer;

    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * speed);
        meshRenderer.material.mainTextureOffset = offset;
    }
}
