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
                // Here, the color is set to white, but you can change it as you like.
                
               float colorVal = saturate(i.speed / 30.0);
             return fixed4(colorVal, 0.0, 1-colorVal, 1.0);
                //return fixed4(0.0, 0.20, 1.0, 1.0);
            }
            ENDCG
        }
    }
}
