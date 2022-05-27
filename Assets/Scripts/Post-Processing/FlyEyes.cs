using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(FlyEyesRenderer), PostProcessEvent.AfterStack, "Custom/FlyEyes")]
public sealed class FlyEyes : PostProcessEffectSettings
{
    [Range(0, 2)] public FloatParameter CellSize = new FloatParameter { value = 2 };
    [Range(0, 0.1f)] public FloatParameter BorderSize = new FloatParameter { value = 0.05f };
    [Range(0, 1)] public FloatParameter BorderValue = new FloatParameter { value = 0 };
    [Range(0, 1)] public FloatParameter ColorAmount = new FloatParameter { value = 0.1f };
    [Range(0, 1)] public FloatParameter MinMultiplier = new FloatParameter { value = 0 };
    [Range(0, 1)] public FloatParameter MaxMultiplier = new FloatParameter { value = 1 };
}

public sealed class FlyEyesRenderer : PostProcessEffectRenderer<FlyEyes>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/FlyEyes"));
        sheet.properties.SetFloat("_CellSize", settings.CellSize);
        sheet.properties.SetFloat("_BorderSize", settings.BorderSize);
        sheet.properties.SetFloat("_BorderValue", settings.BorderValue);
        sheet.properties.SetFloat("_ColorAmount", settings.ColorAmount);
        sheet.properties.SetFloat("_MinMult", settings.MinMultiplier);
        sheet.properties.SetFloat("_MaxMult", settings.MaxMultiplier);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}