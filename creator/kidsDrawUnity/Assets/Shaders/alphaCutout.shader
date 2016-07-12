Shader "Custom/cutOutAlpha" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_CutTex ("Cutout (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	_Illum ("Self Illumination", Range(0,20)) = 20.0
}
 
SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200
	Cull Off
	Lighting Off
 
CGPROGRAM
#pragma surface surf Lambert alpha
 
 
sampler2D _MainTex;
sampler2D _CutTex;
fixed4 _Color;
float _Cutoff;
float _Illum;
 
struct Input {
	float2 uv_MainTex;
};
 
void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	float ca = tex2D(_CutTex, IN.uv_MainTex).a;
	o.Albedo = c.rgb;
	o.Emission = c.rgb * _Illum;
	
	if (ca > _Cutoff)
	  o.Alpha = 0;
	else
	  o.Alpha = c.a;
	  
}



ENDCG
}
 
Fallback "Transparent/VertexLit"
}