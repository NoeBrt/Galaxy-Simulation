// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/NBody Particles" 
{
	Properties 
	{
		_SpriteTex ("Base (RGB)", 2D) = "white" {}
		_Size ("Size", Range(0, 3)) = 0.5
	}

	SubShader 
	{
		Pass
		{
			Tags { "RenderType"="Transparent" }
			Blend One One
			ZWrite Off
			LOD 200
		
			CGPROGRAM
				#pragma target 5.0
				#pragma vertex vertex_main
				#pragma fragment fragment_main
				#pragma geometry geometry_main
				#include "UnityCG.cginc" 

				// Data structures and variables

				struct VertexShaderIn
				{
					float3 body;
					float3 vel;
				};

				struct GeometryShaderIn
				{
					float4 pos		: POSITION;
				};

				struct FragmentShaderIn
				{
					float4	pos		: POSITION;
					float2  tex0	: TEXCOORD0;
				};

				float _Size;
				float4x4 _VP;
				Texture2D _SpriteTex;
				SamplerState sampler_SpriteTex;
				StructuredBuffer<VertexShaderIn> bodies;

				// Shader programs

				//GeometryShaderIn vertex_main(appdata_base v)
				//{
				//	GeometryShaderIn output;
				//	output.pos =  mul(unity_ObjectToWorld, v.vertex);
				//	return output;
				//}

				GeometryShaderIn vertex_main(uint id : SV_VertexID, uint inst : SV_InstanceID)
				{
					float3 pos = bodies[id].body;
					float mass = 1.0f;

					GeometryShaderIn output;
					output.pos.xyz =  mul(unity_ObjectToWorld, pos);
					output.pos.w = mass;
					return output;
				}

				[maxvertexcount(4)]
				void geometry_main(point GeometryShaderIn p[1], inout TriangleStream<FragmentShaderIn> triStream)
				{
					float3 up = float3(0, 1, 0);
					float3 look = _WorldSpaceCameraPos - p[0].pos;
					//look.y = 0;
					look = normalize(look);
					float3 right = cross(up, look);
					
					float halfS = 0.5f * _Size * p[0].pos.w;
							
					float4 v[4];
					v[0] = float4(p[0].pos + halfS * right - halfS * up, 1.0f);
					v[1] = float4(p[0].pos + halfS * right + halfS * up, 1.0f);
					v[2] = float4(p[0].pos - halfS * right - halfS * up, 1.0f);
					v[3] = float4(p[0].pos - halfS * right + halfS * up, 1.0f);

				float4x4 vp = mul(_VP, unity_ObjectToWorld);
					FragmentShaderIn pIn;
					pIn.pos = mul(vp, v[0]);
					pIn.tex0 = float2(1.0f, 0.0f);
					triStream.Append(pIn);

					pIn.pos =  mul(vp, v[1]);
					pIn.tex0 = float2(1.0f, 1.0f);
					triStream.Append(pIn);

					pIn.pos =  mul(vp, v[2]);
					pIn.tex0 = float2(0.0f, 0.0f);
					triStream.Append(pIn);

					pIn.pos =  mul(vp, v[3]);
					pIn.tex0 = float2(0.0f, 1.0f);
					triStream.Append(pIn);
				}

				float4 fragment_main(FragmentShaderIn input) : COLOR
				{
					return _SpriteTex.Sample(sampler_SpriteTex, input.tex0);
				}

			ENDCG
		}
	} 
}
