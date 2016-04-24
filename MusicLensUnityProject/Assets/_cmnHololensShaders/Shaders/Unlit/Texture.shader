Shader "Hololens Shaders/Texture" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Fade("Fade", Float) = 0
	}
	SubShader{
		Pass{

			CGPROGRAM
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD1;
				fixed4 col : TEXCOORD0;
			};

			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;
			fixed _Fade;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata_full v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target{
				fixed4 tex = tex2D(_MainTex, i.uv);
				return tex * _Color * (1-_Fade);
			}

			ENDCG
		}
	}
}