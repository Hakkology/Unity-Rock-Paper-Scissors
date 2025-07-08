Shader "Custom/ArenaBackground"
{
    Properties
    {
        _RockRatio ("Rock Ratio", Range(0,1)) = 0.33
        _PaperRatio ("Paper Ratio", Range(0,1)) = 0.33
        _ScissorsRatio ("Scissors Ratio", Range(0,1)) = 0.34
        _WaveFreq ("Wave Frequency", Float) = 5.0
        _WaveAmp ("Wave Amplitude", Float) = 0.05
        _Transition ("Softness", Float) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Background" }
        Pass
        {
            Name "Unlit"
            Blend Off
            ZWrite Off
            ZTest Always

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float _RockRatio;
            float _PaperRatio;
            float _ScissorsRatio;
            float _WaveFreq;
            float _WaveAmp;
            float _Transition;

            Varyings vert (Attributes input)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                o.uv = input.uv;
                return o;
            }

            float waveOffset(float y)
            {
                return sin(y * _WaveFreq * 6.28) * _WaveAmp;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float x = i.uv.x;
                float y = i.uv.y;

                float wave = waveOffset(y);

                float rockEnd = _RockRatio + wave;
                float paperEnd = rockEnd + _PaperRatio;

                float3 colRock = float3(0, 1, 0);     // Green
                float3 colPaper = float3(0, 0, 1);    // Blue
                float3 colScissors = float3(1, 0, 0); // Red

                float rockBlend = smoothstep(rockEnd - _Transition, rockEnd + _Transition, x);
                float paperBlend = smoothstep(paperEnd - _Transition, paperEnd + _Transition, x);

                float3 col = colRock;
                col = lerp(col, colPaper, rockBlend);
                col = lerp(col, colScissors, paperBlend);

                return float4(col, 1);
            }
            ENDHLSL
        }
    }
}
