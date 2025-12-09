Shader "Custom/CubemapURP" {
    Properties {
        _Tint("Tint Color", Color) = (1, 1, 1, 1)
        _Exposure("Exposure", Range(0, 8)) = 1.0
        [NoScaleOffset] _Cubemap("Cubemap", Cube) = "white" {}
    }
    
    SubShader {
        Tags { 
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Geometry"
        }
        
        Cull Front
        ZWrite On
        
        Pass {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            TEXTURECUBE(_Cubemap);
            SAMPLER(sampler_Cubemap);
            
            CBUFFER_START(UnityPerMaterial)
                half4 _Tint;
                half _Exposure;
            CBUFFER_END
            
            struct Attributes {
                float4 positionOS : POSITION;
            };
            
            struct Varyings {
                float4 positionCS : SV_POSITION;
                float3 viewDir : TEXCOORD0;
            };
            
            Varyings vert(Attributes input) {
                Varyings output;
                
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                output.positionCS = vertexInput.positionCS;
                
                float3 worldPos = vertexInput.positionWS;
                output.viewDir = normalize(worldPos - _WorldSpaceCameraPos);
                
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target {
                half4 cubemapColor = SAMPLE_TEXTURECUBE(_Cubemap, sampler_Cubemap, input.viewDir);
                half3 finalColor = cubemapColor.rgb * _Tint.rgb * _Exposure;
                return half4(finalColor, 1.0);
            }
            ENDHLSL
        }
    }
}