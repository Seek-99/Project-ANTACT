Shader "Custom/DarkenWithStencil"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,0.7)
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType" = "Transparent" } // 중요!
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Stencil
        {
            Ref 1
            Comp NotEqual   // FOV에서 찍은 영역은 무시
            Pass Keep
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
