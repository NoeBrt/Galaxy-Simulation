Shader "Custom/StarShader"
{
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                
            };
            uniform float4 colorStart;
            uniform float4 colorEnd;
            uniform float divider;

            struct Star{
                float3 position;
                float3 velocity;
            };
            StructuredBuffer<Star> starBuffer;

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float speed : TEXCOORD0;

            };

            v2f vert (uint vertexID : SV_VertexID, appdata v)
            {
                v2f o;
                Star star = starBuffer[vertexID];
                o.vertex = TransformObjectToHClip(float4(star.position.xyz, 1.0));
                o.speed = length(star.velocity);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {       
                return lerp(colorStart, colorEnd, saturate(i.speed / divider));
                
            }
            ENDHLSL
        }
    }
}
