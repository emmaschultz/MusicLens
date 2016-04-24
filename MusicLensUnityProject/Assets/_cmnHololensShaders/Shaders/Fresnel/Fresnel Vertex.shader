Shader "Hololens Shaders/Fresnel Vertex Transparent"
{
	Properties
	{
		_Fade("Fade", Range(0,1)) = 1

		_Color("Color", Color) = (1,1,1,1)
		_Width("Rim Width", Range(0,2)) = .1
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }

		Pass{
			ZWrite On
			ColorMask 0
		}

		Blend SrcAlpha OneMinusSrcAlpha
		//Lighting Off 
		ZWrite Off
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
				fixed4 color : TEXCOORD1;
			};

			fixed4 _Color;
			half _Width;

			fixed _Fade;
			
			v2f vert (appdata_full v)
			{
				v2f o;

				//Calculate position to correctly draw the pixel
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				//Calculate Fresnel by comparing the dot product of the view angle to that of the normal.
				fixed3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				fixed dotProduct = 1 - saturate(dot(v.normal, viewDir));

				//Smoothly color the outline.
				o.color = smoothstep(1-_Width, 1.0, dotProduct) * _Color;
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{ 
				return i.color * (1 - _Fade);
			}
			ENDCG
		}
	}
}
