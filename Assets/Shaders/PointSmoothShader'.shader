Shader "Custom/PointSmoothShader"
{
     Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TransparencyAmount ("Max Transparency", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha // Enable transparency

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _TransparencyAmount;
            float _MoveSpeed; // Set this from C# script

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Reduce alpha based on movement speed
                float transparency = lerp(1.0, _TransparencyAmount, saturate(_MoveSpeed));

                col.a *= transparency; // Apply transparency effect

                return col;
            }
            ENDCG
        }
    }
}

