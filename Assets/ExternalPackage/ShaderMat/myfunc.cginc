//float3を4pixelのfixed3(色：RGBA32)に変換(VRChat技術メモ帳より引用)
//ix = 0,1,2,3
//render textureでColorモードをfloatにするとクラッシュするためこんなことをしている。
fixed3 pack(float3 xyz, uint ix) {
    uint3 xyzI = asuint(xyz);
    xyzI = (xyzI >> (ix * 8)) % 256;
    return (float3(xyzI) + 0.5) / 255.0;
}

//4pixelのfixed3(色：RGBA32)をfloat3に変換(VRChat技術メモ帳より引用)
//横に並んだ4pixelの中心uv座標を第二変数で取得
float3 unpack(sampler2D _Pos,float2 uv,float texWidth = 4.0) {
    //float texWidth = 4.0; // RenderTextureの横幅
    float3 e = float3(1.0/texWidth/2, 3.0/texWidth/2, 0);
    uint3 v0 = uint3(tex2Dlod(_Pos, float4(uv - e.yz,0,0)).xyz * 255.) << 0;
    uint3 v1 = uint3(tex2Dlod(_Pos, float4(uv - e.xz,0,0)).xyz * 255.) << 8;
    uint3 v2 = uint3(tex2Dlod(_Pos, float4(uv + e.xz,0,0)).xyz * 255.) << 16;
    uint3 v3 = uint3(tex2Dlod(_Pos, float4(uv + e.yz,0,0)).xyz * 255.) << 24;
    uint3 v = v0 + v1 + v2 + v3;
    return asfloat(v);
}

//カラーマップ関数
//T(0-1の間に規格化された変数)-->col
fixed3 t2colmap(float T){
    fixed3 col = fixed3(0,0,0);
	if(T<1.0/3.0) col = fixed3(0,0,3.0*T);
	else if(T<2.0/3.0) col = fixed3(0,3.0*T - 1.0,-3.0*T+2.0);
	else col = fixed3(3.0*T-2.0,-3.0*T+3.0,0);			
	return col;
}
//col-->T
float colmap2t(fixed3 col){
	float t;
	if(col.b==0&&col.r>0||col.g==1.0) t = col.r*(1.0/3.0) + 2.0/3.0;
	else if(col.g>0&&col.b>0||col.b==1.0) t = col.g*(1.0/3.0) + 1.0/3.0;
	else t = col.b*(1.0/3.0);
	return t;
}


//以下描画用の関数。適宜変更・追加。
int num2d(float n,float d){
  int m = int(fmod(n,d));
  return m;
}

float3 digital1(float2 p,float2 center){

  float3 color = float3(0.0f,0.0f,0.0f);
  float2 u = p - center;
  float x = u.x;
  float y = u.y;

  if(abs(y)<-abs(0.5f*x)+0.35f&&abs(y)<0.1f) color+=float3(1.0f,1.0f,1.0f);

  return color;
}
float3 digital2(float2 p,float2 center){

  float3 color = float3(0.0f,0.0f,0.0f);
  float2 u = p - center;
  float x = u.x;
  float y = u.y;

  if(abs(x)<-abs(2.0f*y)+0.35f*2.0f&&abs(x)<0.2f) color+=float3(1.0f,1.0f,1.0f);

  return color;
}
float3 digital3(float2 p,float2 center){

  float3 color = float3(0.0f,0.0f,0.0f);
  float2 u = p - center;
  float x = (u.y*1.1-u.x)/sqrt(2);
  float y = (u.y*1.1+u.x)/sqrt(2);

  if(abs(x)<-abs(y)+0.35f*1.4&&abs(x)<0.15f) color+=float3(1.0f,1.0f,1.0f);

  return color;
}
float3 digital4(float2 p,float2 center){

  float3 color = float3(0.0f,0.0f,0.0f);
  float2 u = p - center;
  float x = (u.y*1.1+u.x)/sqrt(2);
  float y = (u.y*1.1-u.x)/sqrt(2);

  if(abs(x)<-abs(y)+0.35f*1.4&&abs(x)<0.15f) color+=float3(1.0f,1.0f,1.0f);

  return color;
}
float3 alpha(float2 p,int code){
 float3 destColor = float3(0.0,0.0,0.0);

if(code == 47){// "/"
 destColor += digital3(p,float2(0.4,0.35));
 destColor += digital3(p,float2(-0.4,-0.35));
}
if(code == 72){//H
   destColor += digital2(p,float2(-0.7f,0.4f));
   destColor += digital2(p,float2(0.7f,0.4f));					
   destColor += digital2(p,float2(-0.7f,-0.4f));
   destColor += digital2(p,float2(0.7f,-0.4f));
   destColor += digital1(p,float2(0.0f,0.0f));
}
if(code == 75){//K
   destColor += digital2(p,float2(-0.7f,0.4f));
   destColor += digital2(p,float2(-0.7f,-0.4f));
   destColor += digital3(p,float2(0.4,0.35));
   destColor += digital1(p,float2(0.0f,0.0f));
   destColor += digital2(p,float2(0.7f,-0.4f));
}

if(code == 77){//M
   destColor += digital2(p,float2(-0.7f,0.4f));
   destColor += digital2(p,float2(0.7f,0.4f));					
   destColor += digital2(p,float2(-0.7f,-0.4f));
   destColor += digital2(p,float2(0.7f,-0.4f));
   destColor += digital3(p,float2(0.4,0.35));
   destColor += digital4(p,float2(-0.4,0.35));
}
if(code == 79){//O
   destColor += digital1(p,float2(0,0.8f));
   destColor += digital1(p,float2(0,-0.8f));
   destColor += digital2(p,float2(-0.7f,0.4f));
   destColor += digital2(p,float2(0.7f,0.4f));					
   destColor += digital2(p,float2(-0.7f,-0.4f));
   destColor += digital2(p,float2(0.7f,-0.4f));
}
if(code == 83){//S
   destColor += digital1(p,float2(0,0.0f));
   destColor += digital1(p,float2(0,0.8f));
   destColor += digital1(p,float2(0,-0.8f));
   destColor += digital2(p,float2(-0.7f,0.4f));
   destColor += digital2(p,float2(0.7f,-0.4f));
}
if(code == 86){//V
 destColor += digital2(p,float2(-0.7,0.4));
 destColor += digital2(p,float2(0.7,0.4));
 destColor += digital3(p,float2(0.4,-0.35));
 destColor += digital4(p,float2(-0.4,-0.35));
}

if(code == 88){//X
 destColor += digital3(p,float2(0.4,0.35));
 destColor += digital4(p,float2(-0.4,0.35));
 destColor += digital3(p,float2(-0.4,-0.35));
 destColor += digital4(p,float2(0.4,-0.35));
}
if(code == 89){//Y
 destColor += digital3(p,float2(0.4,0.35));
 destColor += digital4(p,float2(-0.4,0.35));
 destColor += digital2(p,float2(0,-0.4));
}
if(code == 90){//Z
 destColor += digital1(p,float2(0,0.8));
 destColor += digital3(p,float2(0.4,0.35));
 destColor += digital3(p,float2(-0.4,-0.35));
 destColor += digital1(p,float2(0,-0.8));
}

return destColor;
}

float3 circle(float2 p,float2 center,float radius,float a = 1.0,float b = 1.0){
  float3 color = float3(0.0f,0.0f,0.0f);
  float2 u = p - center;
  float x = u.x/a;
  float y = u.y/b;
  float r = sqrt(x*x+y*y);   
  
  if(abs(r)<radius) color+=float3(1.0f,1.0f,1.0f);

  return color;
 }

float3 dnumber(float2 p,int n){
   float3 col = float3(0.0f,0.0f,0.0f);
   if(n==0){
   col += digital1(p,float2(0,0.8f));
   col += digital1(p,float2(0,-0.8f));
   col += digital2(p,float2(-0.7f,0.4f));
   col += digital2(p,float2(0.7f,0.4f));					
   col += digital2(p,float2(-0.7f,-0.4f));
   col += digital2(p,float2(0.7f,-0.4f));
   }
   else if(n==1){
   col += digital2(p,float2(0.7f,0.4f));
   col += digital2(p,float2(0.7f,-0.4f));
   }

   else if(n==2){
   col += digital1(p,float2(0,0.0f));
   col += digital1(p,float2(0,0.8f));
   col += digital1(p,float2(0,-0.8f));
   col += digital2(p,float2(0.7f,0.4f));
   col += digital2(p,float2(-0.7f,-0.4f));
   }
   else if(n==3){
   col += digital1(p,float2(0,0.0f));
   col += digital1(p,float2(0,0.8f));
   col += digital1(p,float2(0,-0.8f));
   col += digital2(p,float2(0.7f,0.4f));					
   col += digital2(p,float2(0.7f,-0.4f));
   }
   else if(n==4){
   col += digital1(p,float2(0,0.0f));
   col += digital2(p,float2(-0.7f,0.4f));
   col += digital2(p,float2(0.7f,0.4f));					
   col += digital2(p,float2(0.7f,-0.4f));
   }
   else if(n==5){
   col += digital1(p,float2(0,0.0f));
   col += digital1(p,float2(0,0.8f));
   col += digital1(p,float2(0,-0.8f));
   col += digital2(p,float2(-0.7f,0.4f));
   col += digital2(p,float2(0.7f,-0.4f));
   }
   else if(n==6){
   col += digital1(p,float2(0,0.0f));
   col += digital1(p,float2(0,0.8f));
   col += digital1(p,float2(0,-0.8f));
   col += digital2(p,float2(-0.7f,0.4f));
   col += digital2(p,float2(-0.7f,-0.4f));
   col += digital2(p,float2(0.7f,-0.4f));
   }
   else if(n==7){
   col += digital1(p,float2(0,0.8f));
   col += digital2(p,float2(-0.7f,0.4f));
   col += digital2(p,float2(0.7f,0.4f));					
   col += digital2(p,float2(0.7f,-0.4f));
   }
   else if(n==8){
   col += digital1(p,float2(0,0.0f));
   col += digital1(p,float2(0,0.8f));
   col += digital1(p,float2(0,-0.8f));
   col += digital2(p,float2(-0.7f,0.4f));
   col += digital2(p,float2(0.7f,0.4f));					
   col += digital2(p,float2(-0.7f,-0.4f));
   col += digital2(p,float2(0.7f,-0.4f));
   }
   else if(n==9){
   col += digital1(p,float2(0,0.0f));
   col += digital1(p,float2(0,0.8f));
   col += digital1(p,float2(0,-0.8f));
   col += digital2(p,float2(-0.7f,0.4f));
   col += digital2(p,float2(0.7f,0.4f));					
   col += digital2(p,float2(0.7f,-0.4f));
   }
   return col;
}