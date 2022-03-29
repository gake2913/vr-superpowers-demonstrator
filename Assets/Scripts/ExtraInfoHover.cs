using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraInfoHover : Selectable
{

    public bool Active = false;
    public CanvasGroup CanvasGroup;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        CanvasGroup.alpha = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public override void Select(Selector selector)
    {
        base.Select(selector);

        Active = !Active;

        if (Active) StartCoroutine(CanvasFadeIn());
        else StartCoroutine(CanvasFadeOut());
    }

    private IEnumerator CanvasFadeIn()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            CanvasGroup.alpha = t;
            yield return new WaitForEndOfFrame();
        }
        CanvasGroup.alpha = 1;
    }

    private IEnumerator CanvasFadeOut()
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime;
            CanvasGroup.alpha = t;
            yield return new WaitForEndOfFrame();
        }
        CanvasGroup.alpha = 0;
    }
}
