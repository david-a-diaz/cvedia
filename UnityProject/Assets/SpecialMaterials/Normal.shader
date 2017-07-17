// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Normal" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _ThermalAndEM;

		struct Input {
			float2 uv_MainTex;
			float3 normal;
			float3 world;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.normal = mul(float4(v.normal, 0.0), unity_WorldToObject).xyz; 
            o.world = mul(unity_ObjectToWorld, v.vertex);
        }

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 albedo = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float pixelIntensity = albedo.r + albedo.g + albedo.b;

		//Thermal
			float thermal = 0;

			//Night
			float3 fromPixelToCamera = normalize(IN.world - _WorldSpaceCameraPos);
			float night = 0.35 + pixelIntensity + clamp(dot(fromPixelToCamera, IN.normal), 0, 1);

			//EM
			float em = 0;

			// Metallic and smoothness come from slider variables
			o.Albedo = half3(thermal, night, em);
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = albedo.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
