Shader "Warcool/Skybox/Gradient"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (1, 1, 1, 1)
        _BottomColor ("Bottom Color", Color) = (1, 1, 1, 1)
        
        [Space(10)]
        _Offset ("Offset", Range(-1, 1)) = 0
        _Width ("Width", Range(0, 1)) = 1
        
        [Space(10)]
        [Toggle] _ViewSpace ("View Space", int) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }

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
                float height : TEXCOORD0;
            };

            fixed4 _TopColor;
            fixed4 _BottomColor;
            
            float _Offset;
            float _Width;
            int _ViewSpace;

            v2f vert (appdata v)
            {
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                float4x4 mat = _ViewSpace ? UNITY_MATRIX_MV : UNITY_MATRIX_M;
                o.height = normalize(mul(mat, float4(v.vertex.xyz, 0)) - _WorldSpaceCameraPos.xyz).y;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float height = i.height;
                float value = (height - _Offset) / _Width;
                value = saturate(value * 0.5 + 0.5);
                value = smoothstep(0, 1, value);
                
                return lerp(_BottomColor, _TopColor, value);
            }
            ENDCG
        }
    }
}
