Shader "Custom/URP_PulsingGlow"
{
    Properties
    {
        _BaseMap ("Base Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (0.6, 0, 1, 1)
        _GlowIntensity ("Glow Intensity", Range(0,5)) = 1
        _GlowSpeed ("Glow Speed", Range(0,10)) = 3
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Name "FORWARD"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            sampler2D _BaseMap;
            float4 _BaseMap_ST;
            float4 _GlowColor;
            float _GlowIntensity;
            float _GlowSpeed;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS);
                output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float4 baseColor = tex2D(_BaseMap, input.uv);

                // Calcula un pulso oscilante
                float pulse = (sin(_Time.y * _GlowSpeed) + 1.0) * 0.5;

                // Agrega brillo morado pulsante
                float4 glow = _GlowColor * pulse * _GlowIntensity;

                // Suma el glow al color base
                float4 finalColor = saturate(baseColor + glow);

                return finalColor;
            }

            ENDHLSL
        }
    }

    FallBack "Universal Forward"
}
