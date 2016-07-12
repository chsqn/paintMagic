Shader "Photoshop/DropShadow" {
    Properties {
        _MainTex("Base (RGB), Alpha (A)", 2D) = "" {}
		_Color("Color", Color) = (0,0,0,0.5)
		_OffsetX("OffsetX", Float) = 1
		_OffsetY("OffsetY", Float) = 1
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

			float4 _Color;
			float _OffsetX;
			float _OffsetY;
			sampler2D _MainTex;

			struct vin_vct
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f_vct {
				float4  pos : SV_POSITION;
				float2  uv : TEXCOORD0;
				fixed4  color : COLOR;
			};

			float4 _MainTex_ST;

			v2f_vct vert (vin_vct v) {
				v2f_vct o;
				v.vertex.x += _OffsetX;
				v.vertex.y += _OffsetY;
				o.color = v.color;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag (v2f_vct i) : COLOR {
				fixed4 ts = tex2D(_MainTex, i.uv);
				half4 o =  _Color;
				o.w *= i.color.w * ts.w;
				return o;
			}
			ENDCG
		}
    }
}
