Shader "Hololens Shaders/SignalLit" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)

		_SignalPoint("Signal Point", Vector) = (0,0,0,0)
		_SignalColor("Signal Color", Color) = (1,0,0,1)
		_SignalSpeed("Signal Speed", Float) = 1
		_SignalFrequency("Signal Frequency", Float) = 1

		_Fade("Fade", Range(0,1)) = 1
	}
		SubShader{
			Tags{ "Queue" = "Geometry" "LightMode" = "Vertex" }

			LOD 100

			Pass{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos	: POSITION;
				float3 wpos : TEXCOORD0;
				float3 vpos	: TEXCOORD1;
				float3 col : TEXCOORD2;
			};

			fixed4 _Color;
			fixed4 _SignalColor;
			float4 _SignalPoint;
			float _SignalSpeed;
			float _SignalFrequency;
			fixed _Fade;

			fixed3 light(float3 vertex, fixed3 normal) {
				fixed3 col;
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

				col = (diffuseLight * falloff + amb.xyz);
				return col;
			}

			v2f vert(appdata_full v){
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.wpos = mul(_Object2World, v.vertex).xyz;
				o.vpos = v.vertex.xyz;
				o.col = light(v.vertex.xyz, normalize(v.normal));

				return o;
			}

			//A custom, fast sin-like function for performance reasons.
			float MySin(float time) {
				float sina = (time % _SignalFrequency) / _SignalFrequency;
				return abs(1-2*sina);
			}

			float3 frag(v2f i) : COLOR{
				float dist = distance(i.wpos,_SignalPoint);
				float sina = MySin(dist + (_Time.x % _SignalFrequency) * _SignalSpeed);

				float3 col = i.col * lerp(_Color, _SignalColor, sina);
				col *= (1 - _Fade);

				return  col;
			}

			ENDCG
		}
	}
}
