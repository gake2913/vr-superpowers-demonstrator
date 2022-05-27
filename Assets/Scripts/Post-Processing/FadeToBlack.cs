using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(FadeToBlackRenderer), PostProcessEvent.AfterStack, "Custom/FadeToBlack")]
public sealed class FadeToBlack : PostProcessEffectSettings
{
    [Range(0f, 1f)]
    public FloatParameter fade = new FloatParameter { value = 0.5f };
}

public sealed class FadeToBlackRenderer : PostProcessEffectRenderer<FadeToBlack>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/FadeToBlack"));
        sheet.properties.SetFloat("_Fade", settings.fade);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}