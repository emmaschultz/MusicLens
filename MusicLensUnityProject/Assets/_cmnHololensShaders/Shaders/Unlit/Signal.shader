Shader "Hololens Shaders/Signal" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)

		_SignalPoint("Signal Point", Vector) = (0,0,0,0)
		_SignalColor("Signal Color", Color) = (1,0,0,1)
		_SignalSpeed("Signal Speed", Float) = 1
		_SignalFrequency("Signal Frequency", Float) = 1

		_Fade("Fade", Range(0,1)) = 1
	}
		SubShader{
			Tags{ "Queue" = "Geometry" }

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
			};

			fixed4 _Color;
			fixed4 _SignalColor;
			float4 _SignalPoint;
			float _SignalSpeed;
			float _SignalFrequency;
			fixed _Fade;

			v2f vert(appdata_full v){
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.wpos = mul(_Object2World, v.vertex).xyz;
				o.vpos = v.vertex.xyz;

				return o;
			}

			//A custom, fast sin-like function for performance reasons.
			float MySin(float time) {
				float sina = (time % _SignalFrequency) / _SignalFrequency;
				return abs(1-2*sina);
			}

			fixed4 frag(v2f i) : COLOR{
				float dist = distance(i.wpos,_SignalPoint);
				float sina = MySin(dist % _SignalFrequency + (_Time.x) * _SignalSpeed);

				fixed4 col = lerp(_Color, _SignalColor, sina);
				col *= (1 - _Fade);

				return  col;
			}

			ENDCG
		}
	}
}
