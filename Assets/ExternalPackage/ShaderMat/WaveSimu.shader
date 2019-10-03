Shader "Unlit/WaveSimu"
{
	Properties
	{
		_PrevTex ("t-dt Texture", 2D) = "white" {}
		_InterTex ("Interactive Texture", 2D) = "white" {}
		_v ("velociy",Range (0,5)) = 5.0
		_k1 ("attenation1",Range (0.8,1)) = 0.998//減衰係数1
		_k2 ("attenation2",Range (0,1)) = 0.1//減衰係数2
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "myfunc.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv  : TEXCOORD0;
			};

			sampler2D _PrevTex;
			sampler2D _InterTex;
			float _v;
			float _k1;
			float _k2;
			
			float4 _Color;

			v2f vert(appdata_base v)
			{
				v2f o;
				float3 n = UnityObjectToWorldNormal(v.normal);
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = v.texcoord.xy;//UV座標
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
			    fixed3 col = fixed3(0,0,0);

				float size = 128.0;//size of render texture
				float pix = 1.0/size;

				float x = i.uv.x;
				float y = i.uv.y;
				float v = _v*0.05;
				float k1 = _k1*1.0;
				float k2 = _k2*1.0;

				fixed3 pvcol = tex2D(_PrevTex,float2(x,y));//1F前
				fixed3 pvcolxp = tex2D(_PrevTex,float2(x+pix,y));
				fixed3 pvcolxm = tex2D(_PrevTex,float2(x-pix,y));
				fixed3 pvcolyp = tex2D(_PrevTex,float2(x,y+pix));
				fixed3 pvcolym = tex2D(_PrevTex,float2(x,y-pix));
				
				float pv2u = pvcol.g;//2F前
				float pvu = pvcol.r;//1F前
				float pvuxp = pvcolxp.r;
				float pvuxm = pvcolxm.r;
				float pvuyp = pvcolyp.r; 
				float pvuym = pvcolym.r;
				
				float dt = 1.0/90.0;//固定時間差
				float dx = pix*1.0;
				float dy = pix*1.0;
			
			
			    //ノイマン境界条件を課す(du/dx=0,du/dy=0 at boundary)
				//refrect at boundary
				if(x<pix) pvuxm = pvu;
			    if(x>1-pix) pvuxp = pvu;
			    if(y<pix) pvuym = pvu;
	            if(y>1-pix) pvuyp = pvu;
			
			/*
			//固定端境界条件を課す
			//not refrect at boundary
				if(x<pix) pvuxm = 0.0;
				if(x>1-pix) pvuxp = 0.0;
			    if(y<pix) pvuym = 0.0;
	            if(y>1-pix) pvuyp = 0.0;
		     */
				float u = 0.0;//Wave Height
				if(_Time.y<1.0){//Initial Condition
					/*
					if((x-0.5)*(x-0.5)+(y-0.5)*(y-0.5)<0.05*0.05) u = 1.0;
					else u = 0.0;
					*/
					u = 0.0;
				}
				else{//Wave Function 
					u = (2*pvu - pv2u 
					  +((dt*dt*v*v)/(dx*dx))*(pvuxm-2*pvu+pvuxp)
					  +((dt*dt*v*v)/(dy*dy))*(pvuym-2*pvu+pvuyp))*k1
					  -k2*dt*(pvu-pv2u);
				}
			    col = fixed3(u,pvcol.r,0);//u:現在/pvcol.r:1F前
				fixed3 colinter = tex2D(_InterTex,float2(x,y));
				col += fixed3(length(colinter),0.0,0.0);
				return fixed4(col,1.0);
			}
			ENDCG
		}
	}
}
