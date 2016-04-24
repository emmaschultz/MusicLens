Shader "" {
	Properties{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Fade("Fade", Range(0,1)) = 0
	}
		SubShader{
		Tags{ "RenderType" = "Geometry" }

		Pass{
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

	struct v2f {
		float4 pos : SV_POSITION;
		fixed4 col : TEXCOORD0;
	};

	fixed4 _Color;
	fixed _Fade;

	v2f vert(appdata_full v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		fixed4 light = v.color;

		o.col = _Color * light * (1 - _Fade);

		return o;
	}


	fixed4 frag(v2f i) : SV_Target{
		return i.col;
	}

		ENDCG
	}
	}
}