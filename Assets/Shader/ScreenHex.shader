Shader "Hidden/ScreenHex"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _CellSize("Cell Size", Range(0, 2)) = 1
        _BorderSize("Border Size", Range(0, 0.1)) = 0.05
        _BorderValue("Border Value", Range(0, 1)) = 0
        _ColorAmount("Color Amount", Range(0, 1)) = 0.1
        _MinMult("Min Multiplier", Range(0, 1)) = 0
        _MaxMult("Max Multiplier", Range(0, 1)) = 1
    }

    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Random.cginc"

            uniform sampler2D _MainTex;
            uniform float _CellSize;
            uniform float _BorderSize;
            uniform float _BorderValue;
            uniform float _ColorAmount;
            uniform float _MinMult;
            uniform float _MaxMult;

            float3 voronoiNoise(float2 value){
                float2 baseCell = floor(value);

                //first pass to find the closest cell
                float minDistToCell = 10;
                float2 toClosestCell;
                float2 closestCell;
                [unroll]
                for(int x1=-1; x1<=1; x1++){
                    [unroll]
                    for(int y1=-1; y1<=1; y1++){
                        float2 cell = baseCell + float2(x1, y1);
                        float2 cellPosition = cell + rand2dTo2d(cell);
                        float2 toCell = cellPosition - value;
                        float distToCell = length(toCell);
                        if(distToCell < minDistToCell){
                            minDistToCell = distToCell;
                            closestCell = cell;
                            toClosestCell = toCell;
                        }
                    }
                }

                //second pass to find the distance to the closest edge
                float minEdgeDistance = 10;
                [unroll]
                for(int x2=-1; x2<=1; x2++){
                    [unroll]
                    for(int y2=-1; y2<=1; y2++){
                        float2 cell = baseCell + float2(x2, y2);
                        float2 cellPosition = cell + rand2dTo2d(cell);
                        float2 toCell = cellPosition - value;

                        float2 diffToClosestCell = abs(closestCell - cell);
                        bool isClosestCell = diffToClosestCell.x + diffToClosestCell.y < 0.1;
                        if(!isClosestCell){
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

            float4 frag(v2f_img i) : COLOR {
                float4 c = tex2D(_MainTex, i.uv);
                
                float2 value = i.uv / _CellSize;
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

            ENDCG
        }
    }
}
