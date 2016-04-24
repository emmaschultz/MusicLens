Shader "3DText/ShadowColor" {
	Properties{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_ShadowIntensity("ShadowIntensity", Float) = 1
		_Ambient("Ambiance", Color) = (1,1,1,1)
		_Fade("Fade", Range(0,1)) = 0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" "LightMode" = "Vertex" }

		Pass{
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

	struct v2f {
		float4 pos : SV_POSITION;
		//float light : TEXCOORD0;
		fixed4 col : TEXCOORD0;
	};

	float _ShadowIntensity;
	fixed4 _Color;
	fixed4 _Ambient;
	fixed _Fade;

	v2f vert(appdata_full v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		fixed3 lightDirection = normalize(mul(unity_LightPosition[0],
			UNITY_MATRIX_IT_MV).xyz - v.vertex.xyz);

		float light = saturate(.5 + .5*dot(normalize(v.normal), lightDirection)) * _ShadowIntensity;

		o.col = _Color * (light + (1 - light)*_Ambient) * (1 - _Fade);

		return o;
	}


	fixed4 frag(v2f i) : SV_Target{
		return i.col;
	}

		ENDCG
	}
	}
}