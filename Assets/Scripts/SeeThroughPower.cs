using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeeThroughPower : MonoBehaviour
{

    public InputActionReference TriggerAxis;

    public Material SilhouetteMaterial;
    public float FadeTime = 1;

    private bool buttonPrev = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonPrev = TriggerAxis.action.ReadValue<float>() > 0.5f;

        Color col = SilhouetteMaterial.color;
        col.a = 0;
        SilhouetteMaterial.color = col;
    }

    // Update is called once per frame
    void Update()
    {
        bool buttonActive = TriggerAxis.action.ReadValue<float>() > 0.5f;

        if (!PowersManager.instance.TealActive) buttonActive = false;

        if (buttonActive && !buttonPrev)
        {
            StartCoroutine(FadeInCoroutine());
        }

        if (buttonActive)
        {

        }

        if (!buttonActive && buttonPrev)
        {
            StartCoroutine(FadeOutCoroutine());
        }

        buttonPrev = buttonActive;

    }

    private IEnumerator FadeInCoroutine()
    {
        float t = 0;
        while (t < 1)
        {
            Color c = SilhouetteMaterial.color;
            c.a = t;
            SilhouetteMaterial.color = c;

            t += Time.deltaTime / FadeTime;

            yield return new WaitForEndOfFrame();
        }

        Color col = SilhouetteMaterial.color;
        col.a = 1;
        SilhouetteMaterial.color = col;
    }

    private IEnumerator FadeOutCoroutine()
    {
        float t = 1;
        while (t > 0)
        {
            Color c = SilhouetteMaterial.color;
            c.a = t;
            SilhouetteMaterial.color = c;

            t -= Time.deltaTime / FadeTime;

            yield return new WaitForEndOfFrame();
        }

        Color col = SilhouetteMaterial.color;
        col.a = 0;
        SilhouetteMaterial.color = col;
    }
}
