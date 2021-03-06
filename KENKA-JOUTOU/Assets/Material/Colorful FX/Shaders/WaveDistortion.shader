// Colorful FX - Unity Asset
// Copyright (c) 2015 - Thomas Hourdel
// http://www.thomashourdel.com

Shader "Hidden/Colorful/Wave Distortion"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Params ("Amplitude (X) Waves (Y) ColorGlitch (Z) Phase (W)", Vector) = (0.6, 5, 0.35, 0.35)
    }

    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            Fog { Mode off }
            
            CGPROGRAM

                #pragma vertex vert_img
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #pragma exclude_renderers flash
                #pragma target 3.0
                #include "UnityCG.cginc"
                #include "./Colorful.cginc"

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _MainTex_TexelSize;
                half4 _Params;

                half4 frag(v2f_img i) : SV_Target
                {
                    half invPhase = 1.0 - _Params.w;
                    
                    half2 o = i.uv * sin(_Params.w * _Params.x) - half2(0.5, 0.5);
                    half theta = acos(dot(o, half2(1.0, 0.0))) * _Params.y;
                    half disp = (exp(cos(theta)) - 2.0 * cos(4.0 * theta) + pow(sin((2.0 * theta - PI) / 24.0), 5.0)) / 10.0;

                    #if UNITY_UV_STARTS_AT_TOP
                    if (_MainTex_TexelSize.y < 0)
                        disp = -disp;
                    #endif

                    half strDisp = _Params.w * disp;
                    half r = tex2D(_MainTex, StereoScreenSpaceUVAdjust(i.uv + strDisp * (1.0 - _Params.z), _MainTex_ST)).r;
                    half g = tex2D(_MainTex, StereoScreenSpaceUVAdjust(i.uv + strDisp, _MainTex_ST)).g;
                    half b = tex2D(_MainTex, StereoScreenSpaceUVAdjust(i.uv + strDisp * (1.0 + _Params.z), _MainTex_ST)).b;
                    half4 srcColor = half4(r, g, b, 1.0);
                    half4 dstColor = tex2D(_MainTex, StereoScreenSpaceUVAdjust(i.uv + invPhase * disp, _MainTex_ST));

                    return srcColor * invPhase + dstColor * _Params.w;
                }

            ENDCG
        }
    }

    FallBack off
}
