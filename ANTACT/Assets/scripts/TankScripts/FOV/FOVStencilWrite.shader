Shader "Custom/FOVStencilWrite_Debug_Fixed"
{
    SubShader
    {
        Tags { "Queue" = "Geometry" }
        Pass
        {
            Cull Off
            ColorMask RGBA
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha // <-- 여기 추가!!

            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(1, 1, 0, 0); // 알파 0.3 적용!
            }
            ENDCG
        }
    }
}
