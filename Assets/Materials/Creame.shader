Shader "Custom/Creame"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    
  SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        static const float PI = 3.14159265f;

        struct Input {
            float2 uv_MainTex;
        };
        
        float wave(float theta,float3 v){
            float t = tan(theta/180 * PI);
            float x1 = v.x - v.z/t;
            float x2 = 1/t + x1;
            float s = sqrt((v.x*v.x + v.z*v.z)/(1 + (x2 - x1)*(x2 - x1)));
            return sin(_Time*100 + s * 100);
        }

        void vert(inout appdata_full v, out Input o )
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            float amp = 0.005*sin(_Time*100+ v.vertex.x * 100) + 0.005*sin(_Time*100 + v.vertex.z * 100) + 0.01*wave(45,v.vertex.xyz);
            //float amp = 0.01*sin(_Time*100 + v.vertex.x * 100) + 0.01*sin(_Time*100 + v.vertex.z * 100);
            v.vertex.xyz = float3(v.vertex.x, v.vertex.y+amp, v.vertex.z);            
            //v.normal = normalize(float3(v.normal.x+offset_, v.normal.y, v.normal.z));
        }

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
