Shader "Photoshop/Pin Light TexVc" {
    Properties {
        _MainTex("Base (RGB), Alpha (A)", 2D) = "" {}
    }

    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off Lighting Off Cull Off Fog { Mode Off } Blend SrcAlpha OneMinusSrcAlpha
		LOD 110

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"
			#include "PhotoshopMathFP.hlsl"

			sampler2D _MainTex;

			struct vin_vct
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f_vct {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			float4 _MainTex_ST;

			v2f_vct vert (vin_vct v) {
				v2f_vct o;
				o.color = v.color;
				o.vertex = mul (UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX (v.uv, _MainTex);
				return o;
			}

			fixed4 frag (v2f_vct i) : COLOR {
				fixed4 ts = tex2D(_MainTex, i.uv);
				ts.xyz = BlendPinLightf(ts.xyz, i.color.xyz);
				ts.w *= i.color.w;
				return ts;
			}
			ENDCG
		}
    }
}
