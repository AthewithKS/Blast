Shader "SyntyStudios/Prototype_Object" {
	Properties {
		_BaseColor ("BaseColor", Vector) = (0.06228374,0.8320726,0.9411765,0)
		_Grid ("Grid", 2D) = "white" {}
		_GridScale ("GridScale", Float) = 5
		_Falloff ("Falloff", Float) = 50
		_OverlayAmount ("OverlayAmount", Range(0, 1)) = 1
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}