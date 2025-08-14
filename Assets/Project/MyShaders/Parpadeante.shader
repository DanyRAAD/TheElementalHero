Shader "Custom/BlinkShader"
{
    Properties
    {
        _Color("Base Color", Color) = (0.2,0.2,0.2,1)
        _EmissionColor("Emission Color", Color) = (1,1,0,1)
        _Active("Active", Float) = 0
        _BlinkSpeed("Blink Speed", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float4 _EmissionColor;
            float _Active;
            float _BlinkSpeed;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                
                float onOff = _Active;

                
                onOff = onOff * (sin(_Time.y * _BlinkSpeed) > 0 ? 1 : 0);

                return lerp(_Color, _EmissionColor, onOff);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
