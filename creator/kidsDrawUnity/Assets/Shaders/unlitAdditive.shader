Shader "Mobile/Sprite Additive" {
    Properties {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    }
    
    Category {
        Tags {
            "Queue" = "Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
        }
        
        Blend SrcAlpha One
        Cull Off
        Lighting Off
        ZWrite Off
        
        BindChannels {
            Bind "Color", color
            Bind "Vertex", vertex
            Bind "TexCoord", texcoord
        }
        
        Fog {
            Mode Off
        }
        
        SubShader {
            Pass {
                SetTexture [_MainTex] {
                    combine texture * primary
                }
            }
        }
    }
}