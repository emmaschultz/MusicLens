Shader "" {
	Properties{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex("Texture", 2D) = "white" {}
		_Fade("Fade", Range(0,1)) = 0
	}
		SubShader{
		Tags{ "RenderType" = "Geometry" }
		Cull Off
		Pass{
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD1;
		//float light : TEXCOORD0;
		fixed4 col : TEXCOORD0;

	};

	sampler2D _MainTex;
	float4 _MainTex_ST;

	fixed4 _Color;
	fixed _Fade;

	v2f vert(appdata_full v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

		fixed4 light = v.color;

		o.col = _Color * light * (1 - _Fade);

		return o;
	}


	fixed4 frag(v2f i) : SV_Target{
		fixed4 tex = tex2D(_MainTex, i.uv);
		return i.col * tex;
	}

		ENDCG
	}
	}
}