// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:0,wrdp:False,dith:0,ufog:False,aust:False,igpj:True,qofs:0,qpre:0,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:2191,x:33692,y:32650,varname:node_2191,prsc:2|diff-7721-RGB,emission-7721-RGB,alpha-1905-OUT;n:type:ShaderForge.SFN_Tex2d,id:7721,x:33442,y:32595,ptovrint:False,ptlb:diffuse,ptin:_diffuse,varname:node_7721,prsc:2,tex:4fd35d1e6b705b44ea17e2d3646a1cb2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:3201,x:32230,y:32757,varname:node_3201,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:3687,x:32583,y:32768,varname:node_3687,prsc:2|A-6443-OUT,B-9839-OUT;n:type:ShaderForge.SFN_Pi,id:6443,x:32454,y:32702,varname:node_6443,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:2889,x:33332,y:32912,varname:node_2889,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9992-OUT;n:type:ShaderForge.SFN_Blend,id:1905,x:33509,y:32912,varname:node_1905,prsc:2,blmd:1,clmp:True|SRC-2889-R,DST-2889-G;n:type:ShaderForge.SFN_Power,id:3731,x:32954,y:32877,varname:node_3731,prsc:2|VAL-3291-OUT,EXP-7195-OUT;n:type:ShaderForge.SFN_Cos,id:3291,x:32750,y:32807,varname:node_3291,prsc:2|IN-3687-OUT;n:type:ShaderForge.SFN_Subtract,id:9839,x:32409,y:32816,varname:node_9839,prsc:2|A-3201-UVOUT,B-3033-OUT;n:type:ShaderForge.SFN_Vector1,id:3033,x:32237,y:32912,varname:node_3033,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:9992,x:33142,y:32909,varname:node_9992,prsc:2|A-3731-OUT,B-9183-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7195,x:32751,y:32969,ptovrint:False,ptlb:falloff,ptin:_falloff,varname:node_7195,prsc:2,glob:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:9183,x:32958,y:33043,ptovrint:False,ptlb:steepness,ptin:_steepness,varname:node_9183,prsc:2,glob:False,v1:4;proporder:7721-7195-9183;pass:END;sub:END;*/

Shader "Aden/skyHemiSphere" {
    Properties {
        _diffuse ("diffuse", 2D) = "white" {}
        _falloff ("falloff", Float ) = 4
        _steepness ("steepness", Float ) = 4
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Background"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Less
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _diffuse; uniform float4 _diffuse_ST;
            uniform float _falloff;
            uniform float _steepness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _diffuse_var = tex2D(_diffuse,TRANSFORM_TEX(i.uv0, _diffuse));
                float3 emissive = _diffuse_var.rgb;
                float3 finalColor = emissive;
                float2 node_2889 = (pow(cos((3.141592654*(i.uv0-0.5))),_falloff)*_steepness).rg;
                return fixed4(finalColor,saturate((node_2889.r*node_2889.g)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
