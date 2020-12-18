Shader "Custom/OutLineLit"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Normal Map" , 2D) = "bump" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _OutLineColor ("OutLine Color", Color) = (1,1,1,1)
        _OutLineWidth ("OutLine Width", Range(1.0,5.0)) = 1.05
        _Metallic ("Metallic", Range(0,1)) = 0.0
 
    } 
    SubShader {
            Tags { "Queue" = "Transparent" }
          
          Pass {
            ZWrite off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
         
            struct v2f {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
            };
            
            float _OutLineWidth;
            fixed4 _OutLineColor;
            
            v2f vert (appdata_base v)
            {
                v.vertex *= _OutLineWidth;
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = _OutLineColor;
                return o;
            }
            
            fixed4 frag (v2f i) : COLOR { return _OutLineColor; }
            ENDCG
        }
        
          Cull Off
          ZWrite On
          CGPROGRAM
          #pragma surface surf Standard fullforwardshadows
          
          struct Input {
              float2 uv_MainTex;
              float2 uv_BumpMap;
              float3 worldPos;
          };
          sampler2D _MainTex;
          sampler2D _BumpMap;
          float _Amount;
          half _Glossiness;
                
          void surf (Input IN, inout SurfaceOutputStandard o) {
          
              o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
              o.Smoothness = _Glossiness;
              o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
          }
          ENDCG
          
        } 
        
   
        Fallback "Diffuse"
}
