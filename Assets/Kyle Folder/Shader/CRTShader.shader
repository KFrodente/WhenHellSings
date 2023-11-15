// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/crtShader"
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
 
    SubShader {
        Pass {
            ZTest Always Cull Off ZWrite Off Fog { Mode off }
 
            CGPROGRAM
 
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"
            #pragma target 3.0
 
            struct v2f 
            {
                float4 pos      : POSITION;
                float2 uv       : TEXCOORD0;
                float4 scr_pos : TEXCOORD1;
            };
 
            uniform sampler2D _MainTex;
            uniform float _VertsColor;
            uniform float _VertsColor2;
 
            v2f vert(appdata_img v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
                o.scr_pos = ComputeScreenPos(o.pos);
                return o;
            }
 
            half4 frag(v2f i): COLOR
            {
                half4 color = tex2D(_MainTex, i.uv);
                float2 ps = i.scr_pos.xy *_ScreenParams.xy / i.scr_pos.w;
                int pp = (uint)ps.y % 3;
                float4 outcolor = float4(0, 0, 0, 1);
                
                if (pp == 1) outcolor.r = color.r;
                else if (pp == 2) outcolor.g = color.g;
                else outcolor.b = color.b;

                return outcolor;
            }
 
            ENDCG
        }
    }
    FallBack "Diffuse"
}
