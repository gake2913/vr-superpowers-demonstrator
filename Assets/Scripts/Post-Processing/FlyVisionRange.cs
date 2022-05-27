using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(FlyVisionRangeRenderer), PostProcessEvent.AfterStack, "Custom/FlyVisionRange")]
public sealed class FlyVisionRange : PostProcessEffectSettings
{
    [Range(0f, 1f)]
    public FloatParameter redCutoff = new FloatParameter { value = 0.1f };
}

public sealed class FlyVisionRangeRenderer : PostProcessEffectRenderer<FlyVisionRange>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/FlyVisionRange"));
        sheet.properties.SetFloat("_RedCut", settings.redCutoff);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}