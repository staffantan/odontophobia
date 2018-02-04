Shader "Custom/Standard 2 Sided" {
	Properties {
		//[Header(First Material)]
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[Normal]_NormalTex ("Normal (RGB)", 2D) = "bump" {}
		
		//_GlossinessTex ("Smoothness Map (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		
		//_MetallicTex ("Metallic Map (RGB)", 2D) = "white" {}
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull Off
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalTex;
		
		
		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalTex;
		};

		half _Glossiness;
		half _Metallic;
		
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			
			half3 normal = UnpackScaleNormal(tex2D(_NormalTex, IN.uv_NormalTex), 1);
			
			o.Normal = normal;
			
			// Metallic and smoothness come from slider variables
			half metallic = _Metallic;
			
			o.Metallic = metallic;
			
			half glossiness = _Glossiness;
			
			o.Smoothness = glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	
	FallBack "Diffuse"
	//CustomEditor "NormalVariationGUI"
}