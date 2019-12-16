Shader "Custom/RimLighting"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_BorderColor("BorderColor" ,Color) = (1,1,1,1)
		_BorderColorIntensity("BorderIntensity" ,Range(0,1)) = 1

	   _OutlineThickness("OutlineThickness", Range(0,10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldNormal;
			float3 viewDir;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed4 _BorderColor;
		float _BorderColorIntensity;
		float _OutlineThickness;

		float clamp(float val, float min, float max)
		{
			if (val >max)
			{
				return max;
			}
			else if (val < max)
			{
				return min;
			}
			return val;
		}

		float getmaxVal(float val, float max)
		{
			if (val <max)
			{
				return 0;
			}
			return max;
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//  o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;


		//	float4 rimCol = float4 ( smoothstep(0.1,1, (1 - dot(normalize(IN.worldNormal), normalize(IN.viewDir)))) *_BorderColor.rgb*_BorderColorIntensity,1);
			//float4 rimCol = smoothstep(1-_BorderColorIntensity, 1, dot(IN.worldNormal, IN.viewDir));
			//rimCol *= _BorderColor;

			float4 rimCol = float4(getmaxVal(1 -dot(IN.viewDir, IN.worldNormal), 1 - _BorderColorIntensity) * _BorderColor.rgb,1);
			
			float3 newpos = IN.worldPos + IN.worldNormal*_OutlineThickness;
			IN.worldPos = newpos;
			
			//o.Albedo = rimCol.rgb*5;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
