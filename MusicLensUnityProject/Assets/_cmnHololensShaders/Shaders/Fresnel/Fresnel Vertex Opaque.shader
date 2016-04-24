Shader "Hololens Shaders/Fresnel Vertex Opaque"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Tint("Texture Tint", Color) = (1,1,1,1)
		_Color("Rim Color", Color) = (1,1,1,1)
		_Width("Rim Width", Float) = .1

		_Fade("Fade", Range(0,1)) = 1
	}
	SubShader
	{
		Tags { "Queue"="Geometry" }

		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Tint;

			fixed4 _Color;
			float _Width;

			fixed _Fade;

			v2f vert (appdata_full v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				float dotProduct = 1 - saturate(dot(v.normal, viewDir));

				o.color = smoothstep(1-_Width, 1.0, dotProduct) * _Color;
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) * _Tint + i.color;
				
				return col * (1 - _Fade);
			}
			ENDCG
		}
	}
}
