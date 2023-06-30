Shader "Custom/Star"
{
	SubShader
	{
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		Cull Off
		ZWrite Off
		Blend One One

		Pass
		{
			CGPROGRAM

			#pragma target 5.0

			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_instancing

			#include "UnityCG.cginc"

			uniform StructuredBuffer position : register(t1);

			uniform int offset;
			uniform float4 color = float4(0.45f, 0.5f, 0.75f, 0.5f);

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float4 vertex : VERTEX;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			// Vertex shader
			v2f vert(appdata v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.pos = UnityObjectToClipPos(v.vertex);
#ifdef UNITY_INSTANCING_ENABLED
				o.pos += float4(position[unity_InstanceID + offset], 0.0f);
#endif
				o.vertex = v.vertex;

				return o;
			}

			// Pixel shader
			fixed4 frag(v2f i) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				float dist = distance(i.vertex, float4(0.0, 0.0, 0.0, 0.0));
				float multiplier = 0.1 / pow(dist, 100.0);

				if (multiplier > 1)
					multiplier = 1;

				return color * multiplier;
			}
			ENDCG
	}
	}
}