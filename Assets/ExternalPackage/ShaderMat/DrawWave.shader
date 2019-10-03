// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/DrawWave" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_WaveTex ("Wave Texture", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_inverce ("inverce",Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		
		CGPROGRAM

		#pragma surface surf Standard alpha addshadow fullforwardshadows
		#pragma target 3.0

		sampler2D _WaveTex;

		struct Input {
			float2 uv_WaveTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _inverce;

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed2 readuv;
			if(_inverce < 0.5){
				readuv = IN.uv_WaveTex;
			}
			else{//裏面用の処理
				readuv = fixed2(1.0 - IN.uv_WaveTex.x,IN.uv_WaveTex.y);
			}

			fixed4 c = tex2D (_WaveTex, readuv);
			o.Albedo = fixed3(0.35,0.6,0.9);//WaterColor
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 0.1 + 0.5 * tex2D(_WaveTex, readuv).r;

			float size = 128.0;
			float pix = 1.0/size;

			float3 duv = float3(pix,pix, 0);
			float v1 = tex2D(_WaveTex, readuv - duv.xz).y;
			float v2 = tex2D(_WaveTex, readuv + duv.xz).y;
			float v3 = tex2D(_WaveTex, readuv - duv.zy).y;
			float v4 = tex2D(_WaveTex, readuv + duv.zy).y;
			o.Normal = normalize(float3(v1 - v2, v3 - v4, 0.3));
		}
		ENDCG
	}
	FallBack "Diffuse"
}
