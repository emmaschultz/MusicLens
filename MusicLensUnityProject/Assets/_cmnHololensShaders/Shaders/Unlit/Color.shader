Shader "Hololens Shaders/Color" {
	Properties{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Fade("Fade", Float) = 0
	}
	SubShader{
		Pass{

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			float4 vert(float4 v:POSITION) : SV_POSITION{
				return mul(UNITY_MATRIX_MVP, v);
			}

			fixed4 _Color;
			fixed _Fade;

			fixed4 frag() : SV_Target{
				return _Color * (1-_Fade);
			}

			ENDCG
		}
	}
}