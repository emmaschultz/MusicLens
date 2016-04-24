Shader "Unlit/MusicFade"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_XPosition ("X Position", Float) = 0
		_DistanceFade ("Distance Fade", Range(.01,.99)) = 1
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }

		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag

	#include "UnityCG.cginc"


		struct v2f
		{
			float4 pos : SV_POSITION;
			fixed3 wpos : TEXCOORD0;
			float2 uv : TEXCOORD1;
		};

		float _XPosition;
		float _DistanceFade;

		fixed _Fade;
		sampler2D _MainTex;
		float4 _MainTex_ST;

		v2f vert(appdata_full v)
		{
			v2f o;

			//Calculate position to correctly draw the pixel
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

			o.wpos = mul(_Object2World, v.vertex).xyz;

			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 tex = tex2D(_MainTex, i.uv);
			float dist = abs(i.wpos.x - _XPosition);

			float fade = saturate(lerp(1, 0, dist / _DistanceFade) * 2);
			float4 color = tex * fade;// (1 - (1 - _DistanceFade)*dist);
			//color.a = 1-fade;
			return color;
		}
		ENDCG
		}
	}
}
