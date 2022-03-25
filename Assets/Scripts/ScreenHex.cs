using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenHex : MonoBehaviour
{

    public new bool enabled = true;
    [Range(0, 2)] public float CellSize = 2;
    [Range(0, 0.1f)] public float BorderSize = 0.05f;
    [Range(0, 1)] public float BorderValue = 0;
    [Range(0, 1)] public float ColorAmount = 0.1f;
    [Range(0, 1)] public float MinMultiplier = 0;
    [Range(0, 1)] public float MaxMultiplier = 1;

    private Material material;

    private void Awake()
    {
        material = new Material(Shader.Find("Hidden/ScreenHex"));
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

        material.SetFloat("_CellSize", CellSize);
        material.SetFloat("_BorderSize", BorderSize);
        material.SetFloat("_BorderValue", BorderValue);
        material.SetFloat("_ColorAmount", ColorAmount);
        material.SetFloat("_MinMult", MinMultiplier);
        material.SetFloat("_MaxMult", MaxMultiplier);
        Graphics.Blit(source, destination, material);
    }
}
