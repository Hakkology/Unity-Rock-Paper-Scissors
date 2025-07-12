Shader "UI/FrameOutline"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {} // The texture to apply on the outline
        _Color("Base Line Color", Color) = (1, 1, 1, 1)
        _Thickness("Line Thickness", Range(0.001, 0.1)) = 0.01
        _CornerRadius("Corner Radius", Range(0.0, 0.5)) = 0.1
        _GradientColor1("Gradient Color 1", Color) = (1, 0, 0, 1) // First color for gradient
        _GradientColor2("Gradient Color 2", Color) = (0, 0, 1, 1) // Second color for gradient
        _GradientDirection("Gradient Direction (X, Y)", Vector) = (1, 0, 0, 0) // Direction of the gradient
        _FlowSpeed("Flow Speed", Range(0.1, 5.0)) = 1.0 // Speed of the flowing effect
        _PulseFrequency("Pulse Frequency", Range(0.0, 10.0)) = 2.0 // Frequency of pulsing alpha
        _PulseAmplitude("Pulse Amplitude", Range(0.0, 1.0)) = 0.5 // Amplitude of pulsing alpha
        _TextureTiling("Texture Tiling", Float) = 1.0 // How many times the texture repeats
        _TextureBlend("Texture Blend", Range(0.0, 1.0)) = 0.5 // How much to blend the texture with the gradient color
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull Off ZWrite Off ZTest Always Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc" // Includes _Time for time-based animations

            sampler2D _MainTex;
            fixed4 _Color;
            float _Thickness;
            float _CornerRadius;
            fixed4 _GradientColor1;
            fixed4 _GradientColor2;
            float4 _GradientDirection;
            float _FlowSpeed;
            float _PulseFrequency;
            float _PulseAmplitude;
            float _TextureTiling; // New variable for texture tiling
            float _TextureBlend; // New variable for texture blend

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 size = float2(1.0, 1.0); // Assuming normalized UV (0 to 1)

                float halfThickness = _Thickness * 0.5;

                float2 p = uv * size;
                float2 halfSize = size * 0.5;
                p -= halfSize; // Center the coordinate system

                float2 absP = abs(p);

                float radius = _CornerRadius;
                float2 corner = halfSize - radius;

                // Distance to the outer rounded corner
                float dist = length(max(absP - corner, 0.0)) - radius;

                // Distance to the inner edge for the hollow effect
                float innerDist = length(max(absP - (corner - halfThickness * 2), 0.0)) - (radius - halfThickness * 2);

                // Determine if the pixel is part of the outline
                float outerShape = (dist < halfThickness) ? 1.0 : 0.0;
                float innerShape = (innerDist < -halfThickness) ? 1.0 : 0.0;
                float isOutline = outerShape * (1.0 - innerShape);

                // --- Gradient Effect ---
                float gradientFactor = dot(uv, normalize(_GradientDirection.xy));
                gradientFactor += _Time.y * _FlowSpeed;
                gradientFactor = frac(gradientFactor);
                fixed4 gradientColor = lerp(_GradientColor1, _GradientColor2, gradientFactor);

                // --- Pulsing Alpha Effect ---
                float pulseAlpha = sin(_Time.y * _PulseFrequency) * _PulseAmplitude + (1.0 - _PulseAmplitude);
                pulseAlpha = saturate(pulseAlpha);

                // --- Texture Effect ---
                // Sample the texture, applying tiling
                fixed4 sampledTexture = tex2D(_MainTex, uv * _TextureTiling);

                // Blend the sampled texture with the gradient color
                // We use the base color as an overall tint
                fixed4 blendedColor = lerp(gradientColor, sampledTexture * _Color, _TextureBlend);

                // Combine blended color with pulsing alpha
                fixed4 finalColor = blendedColor;
                finalColor.a *= pulseAlpha; // Apply pulsing to the alpha channel

                // Apply the outline mask to the final color
                return finalColor * isOutline;
            }
            ENDCG
        }
    }
}