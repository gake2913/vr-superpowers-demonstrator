Shader "Hidden/Custom/FlyEyes"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
        #include "Random.cginc"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        float _CellSize;
        float _BorderSize;
        float _BorderValue;
        float _ColorAmount;
        float _MinMult;
        float _MaxMult;

        float3 voronoiNoise(float2 value) {
            float2 baseCell = floor(value);

            //first pass to find the closest cell
            float minDistToCell = 10;
            float2 toClosestCell;
            float2 closestCell;
            [unroll]
            for (int x1 = -1; x1 <= 1; x1++) {
                [unroll]
                for (int y1 = -1; y1 <= 1; y1++) {
                    float2 cell = baseCell + float2(x1, y1);
                    float2 cellPosition = cell + rand2dTo2d(cell);
                    float2 toCell = cellPosition - value;
                    float distToCell = length(toCell);
                    if (distToCell < minDistToCell) {
                        minDistToCell = distToCell;
                        closestCell = cell;
                        toClosestCell = toCell;
                    }
                }
            }

            //second pass to find the distance to the closest edge
            float minEdgeDistance = 10;
            [unroll]
            for (int x2 = -1; x2 <= 1; x2++) {
                [unroll]
                for (int y2 = -1; y2 <= 1; y2++) {
                    float2 cell = baseCell + float2(x2, y2);
                    float2 cellPosition = cell + rand2dTo2d(cell);
                    float2 toCell = cellPosition - value;

                    float2 diffToClosestCell = abs(closestCell - cell);
                    bool isClosestCell = diffToClosestCell.x + diffToClosestCell.y < 0.1;
                    if (!isClosestCell) {
                        float2 toCenter = (toClosestCell + toCell) * 0.5;
                        float2 cellDifference = normalize(toCell - toClosestCell);
                        float edgeDistance = dot(toCenter, cellDifference);
                        minEdgeDistance = min(minEdgeDistance, edgeDistance);
                    }
                }
            }

            float random = rand2dTo1d(closestCell);
            return float3(minDistToCell, random, minEdgeDistance);
        }

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);

            float2 value = i.texcoord / _CellSize;
            float3 noise = voronoiNoise(value);

            float v = noise.y;
            float vm = (v * (_MaxMult - _MinMult) + _MinMult);

            float isBorder = step(noise.z, _BorderSize);
            float multiplier = lerp(vm, _BorderValue, isBorder);

            float3 randColor = rand1dTo3d(noise.y) * _ColorAmount;
            float4 randMult = float4(randColor.r, randColor.g, randColor.b, 0);

            float4 result = (c + randMult) * multiplier;

            return result;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}