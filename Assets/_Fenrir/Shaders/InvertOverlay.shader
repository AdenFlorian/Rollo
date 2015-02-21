// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:False,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:6,wrdp:False,dith:6,ufog:False,aust:False,igpj:True,qofs:0,qpre:4,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1703071,fgcg:0.2173038,fgcb:0.3088235,fgca:1,fgde:0.003,fgrn:4,fgrf:1207,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9314,x:33433,y:32769,varname:node_9314,prsc:2|diff-5306-RGB,emission-3031-OUT,alpha-8632-OUT;n:type:ShaderForge.SFN_SceneColor,id:9754,x:32864,y:32769,varname:node_9754,prsc:2|UVIN-590-UVOUT;n:type:ShaderForge.SFN_ScreenPos,id:590,x:32543,y:32696,varname:node_590,prsc:2,sctp:0;n:type:ShaderForge.SFN_OneMinus,id:3031,x:33094,y:32795,varname:node_3031,prsc:2|IN-9754-RGB;n:type:ShaderForge.SFN_Color,id:5306,x:32814,y:32418,ptovrint:False,ptlb:node_5306,ptin:_node_5306,varname:node_5306,prsc:2,glob:False,c1:0.986207,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:8632,x:33040,y:32989,varname:node_8632,prsc:2,v1:0.5;proporder:5306;pass:END;sub:END;*/

Shader "Aden/InvertOverlay" {
    Properties {
        _node_5306 ("node_5306", Color) = (0.986207,0,1,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay"
            "RenderType"="Opaque"
        }
        GrabPass{ }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
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
            uniform sampler2D _GrabTexture;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
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
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
/////// Vectors:
////// Lighting:
////// Emissive:
                float3 emissive = (1.0 - tex2D(_GrabTexture, i.screenPos.rg).rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,0.5);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
