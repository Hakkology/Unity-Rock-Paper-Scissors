Shader "UI/ForceFieldAdvanced"
{
    Properties
    {
        _MainTex ("Forcefield Texture", 2D) = "white" {}
        _GradientMaskTex ("Gradient Mask", 2D) = "white" {}
        _ColorBlue ("Mavi Renk", Color) = (0.2,0.8,1,1)
        _ColorRed ("Kirmizi Renk", Color) = (1,0.2,0.2,1)
        _Direction ("Yön", Float) = 0
        _Strength ("Görünürlük", Range(0,1)) = 1
        _Speed ("Hız", Float) = 1
        _GlowStrength ("Glow Gücü", Range(0,5)) = 1
        _Fade ("Fade", Range(0,1)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _GradientMaskTex;
            float4 _MainTex_ST;

            fixed4 _ColorBlue;
            fixed4 _ColorRed;
            float _Direction;
            float _Strength;
            float _Speed;
            float _GlowStrength;
            float _Fade;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float scroll = _Time.y * _Speed * _Direction;
                uv.y += scroll;

                // Forcefield base
                fixed4 fieldCol = tex2D(_MainTex, uv);

                // Glow effect (using derivative)
                float glow = fwidth(fieldCol.a) * _GlowStrength;
                fieldCol.rgb += glow;

                // Gradient alpha mask
                float gradientAlpha = tex2D(_GradientMaskTex, i.uv).r;

                // Directional Color
                fixed4 color = lerp(_ColorRed, _ColorBlue, saturate(_Direction * 0.5 + 0.5));
                fieldCol.rgb *= color.rgb;

                // Final alpha = texture alpha * gradient * fade * strength
                fieldCol.a *= gradientAlpha * _Fade * _Strength;

                return fieldCol;
            }
            ENDCG
        }
    }
}
