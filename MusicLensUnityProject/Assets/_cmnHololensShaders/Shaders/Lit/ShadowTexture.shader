Shader "Hololens Shaders/ShadowTexture" {
	Properties{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex("Texture", 2D) = "white" {}
		_Fade("Fade", Range(0,1)) = 0
	}
	SubShader{
		Tags{ "RenderType" = "Geometry" "LightMode" = "Vertex" }

		Pass{
			CGPROGRAM

	#pragma vertex vert
	#pragma fragment frag

	#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 col : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			fixed4 _Color;
			fixed _Fade;

			fixed3 light(float3 vertex, fixed3 normal) {
				fixed3 col = _Color;
				fixed3 lightDirection;
				fixed falloff;
				if (unity_LightPosition[0].w == 0.0) {
					falloff = 2;
					lightDirection = normalize(mul
						(unity_LightPosition[0], UNITY_MATRIX_IT_MV).xyz);
				}
				else {
					lightDirection = normalize(mul(unity_LightPosition[0],
						UNITY_MATRIX_IT_MV).xyz - vertex);
					falloff = 1.0 / (length(mul(unity_LightPosition[0], UNITY_MATRIX_IT_MV).xyz - vertex)) * 0.5;
				}

				fixed3 diffuseLight = unity_LightColor[0].xyz * max(dot(normal, lightDirection), 0);

				fixed4 amb = UNITY_LIGHTMODEL_AMBIENT;
				col *= (diffuseLight * falloff + amb.xyz);
				return col;
			}

			v2f vert(appdata_full v){ 
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				o.col.xyz = light(v.vertex.xyz, normalize(v.normal));

				return o;
			}


			fixed4 frag(v2f i) : SV_Target{
				fixed4 tex = tex2D(_MainTex, i.uv);
				return i.col * tex  * (1 - _Fade);
			}

			ENDCG
		}
	}
}