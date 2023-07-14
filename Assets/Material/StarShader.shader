Shader "Custom/StarShader"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
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
            uniform int simulationType;

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

             v2f vert (uint vertexID : SV_VertexID)
            {
                v2f o;
                Star star = starBuffer[vertexID];
                o.vertex = UnityObjectToClipPos(float4(star.position, 1.0));
                o.speed = length(star.velocity);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                float4 color;
    
                if (simulationType == 0){
                    color = lerp(float4(0.0, 0.0, 1.0, 1.0),float4(1.0, 1.0, 1.0, 1.0),  saturate(i.speed / 200.0));

                }else if(simulationType==1){
                    color = lerp(float4(0.0, 0.0, 1.0, 1.0),float4(1.0, 0.05, 0.0, 1.0),  saturate(i.speed / 30.0));
                }else if(simulationType==2){
                color = lerp(float4(0.0, 0.0, 1.0, 1.0),float4(1.0, 0.5, 0.0, 1.0),  saturate(i.speed / 30.0));
                }
           
                  
                    return color;
           
            }
            ENDCG
        }
    }
}
