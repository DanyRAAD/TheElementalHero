Shader "Custom/LavaBubbles"
{
    Properties
    {
        _MainTex ("Lava Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1, 0.3, 0.0, 1)
        _DistortionStrength ("Bubble Distortion", Range(0, 0.1)) = 0.03
        _BubbleSpeed ("Bubble Speed", Range(0, 10)) = 3
        _EmissionStrength ("Emission", Range(0, 5)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _DistortionStrength;
            float _BubbleSpeed;
            float _EmissionStrength;

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
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);
                float2 offset = i.uv - center;
                float radius = length(offset);

                // Burbujeo radial con tiempo
                float distortion = sin(radius * 30 - _Time.y * _BubbleSpeed) * _DistortionStrength;
                float2 distortedUV = i.uv + normalize(offset) * distortion;

                fixed4 texColor = tex2D(_MainTex, distortedUV) * _Color;
                texColor.rgb += texColor.rgb * _EmissionStrength;
                return texColor;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
