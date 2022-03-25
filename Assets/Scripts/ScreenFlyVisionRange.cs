using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenFlyVisionRange : MonoBehaviour
{

    public new bool enabled = true;
    [Range(0, 1)] public float RedCut = 0.1f;

    private Material material;

    private void Awake()
    {
        material = new Material(Shader.Find("Hidden/ScreenFlyVisionRange"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!enabled)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_RedCut", RedCut);
        Graphics.Blit(source, destination, material);
    }
}
