// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:1,dpts:2,wrdp:False,dith:2,ufog:False,aust:False,igpj:False,qofs:0,qpre:0,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1703071,fgcg:0.2173038,fgcb:0.3088235,fgca:1,fgde:0.003,fgrn:4,fgrf:1207,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9040,x:33659,y:32675,varname:node_9040,prsc:2|emission-4583-OUT;n:type:ShaderForge.SFN_Color,id:4225,x:32923,y:32791,ptovrint:False,ptlb:skyColor,ptin:_skyColor,varname:node_4225,prsc:2,glob:False,c1:0.1222427,c2:0.6881084,c3:0.875,c4:1;n:type:ShaderForge.SFN_Lerp,id:4583,x:33358,y:32520,varname:node_4583,prsc:2|A-9708-RGB,B-4225-RGB,T-9596-OUT;n:type:ShaderForge.SFN_Slider,id:3464,x:32617,y:33176,ptovrint:False,ptlb:node_3464,ptin:_node_3464,varname:node_3464,prsc:2,min:0,cur:3.692308,max:8;n:type:ShaderForge.SFN_TexCoord,id:3477,x:32617,y:32871,varname:node_3477,prsc:2,uv:0;n:type:ShaderForge.SFN_Power,id:2857,x:33224,y:32875,varname:node_2857,prsc:2|VAL-6641-OUT,EXP-3464-OUT;n:type:ShaderForge.SFN_Multiply,id:3158,x:33205,y:33061,varname:node_3158,prsc:2|A-3477-V,B-3464-OUT;n:type:ShaderForge.SFN_Clamp01,id:9596,x:33221,y:32702,varname:node_9596,prsc:2|IN-2857-OUT;n:type:ShaderForge.SFN_Add,id:6641,x:33219,y:33208,varname:node_6641,prsc:2|A-3158-OUT,B-281-OUT;n:type:ShaderForge.SFN_Slider,id:281,x:32808,y:33311,ptovrint:False,ptlb:node_281,ptin:_node_281,varname:node_281,prsc:2,min:-1,cur:-0.4957265,max:1;n:type:ShaderForge.SFN_Color,id:9708,x:32762,y:32616,ptovrint:False,ptlb:atmosColor,ptin:_atmosColor,varname:node_9708,prsc:2,glob:False,c1:0.8382353,c2:0.4199678,c3:0.09861591,c4:1;proporder:4225-3464-281-9708;pass:END;sub:END;*/

Shader "Aden/atmos" {
    Properties {
        _skyColor ("skyColor", Color) = (0.1222427,0.6881084,0.875,1)
        _node_3464 ("node_3464", Range(0, 8)) = 3.692308
        _node_281 ("node_281", Range(-1, 1)) = -0.4957265
        _atmosColor ("atmosColor", Color) = (0.8382353,0.4199678,0.09861591,1)
    }
    SubShader {
        Tags {
            "Queue"="Background"
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Front
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _skyColor;
            uniform float _node_3464;
            uniform float _node_281;
            uniform float4 _atmosColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_atmosColor.rgb,_skyColor.rgb,saturate(pow(((i.uv0.g*_node_3464)+_node_281),_node_3464)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
