Shader "RyanShader/Rs_PopArrow" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _ColorBG ("ColorBG", Color) = (0,0.04054837,0.1254902,1)
        _Emission ("Emission", Float ) = 1
        _GridDensity ("GridDensity", Float ) = 16
        _ArrowAmount ("ArrowAmount", Float ) = 2
        _Speed ("Speed", Float ) = 0.5
        [MaterialToggle] _UseAlpha ("UseAlpha", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        ZTest Always
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _ArrowAmount;
            uniform float _Speed;
            uniform float _GridDensity;
            uniform float4 _ColorBG;
            uniform float _Emission;
            uniform fixed _UseAlpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };
            VertexOutput vert (VertexInput v) {
                UNITY_SETUP_INSTANCE_ID(v);
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float2 node_2697 = floor(i.uv0 * _GridDensity) / (_GridDensity - 1);
                float4 node_6465 = _Time;
                float node_3054 = (1.0 - frac(((_ArrowAmount*(node_2697.r-abs((node_2697.g-0.5))))+(node_6465.g*_Speed))));
                float node_8891 = saturate((node_3054*step(length((frac((i.uv0*_GridDensity))*2.0+-1.0)),0.75)*node_3054));
                clip(lerp( 1.0, node_8891, _UseAlpha ) - 0.5);
                float3 emissive = (lerp(_ColorBG.rgb,_Color.rgb,node_8891)*_Emission);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
   
}
