// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/VignetteTransparent"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Color("Color",color) = (1,1,1,1)
        _Data("Data",vector)= (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent""Queue"="Transparent" }
		LOD 100
        Blend One One
        ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
                float3 wPos : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Data;
            float4 _Color;
            
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;//TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
                o.wPos = mul (unity_ObjectToWorld, v.vertex).xyz;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));
                float d = (distance(i.uv,float2(.5,.5))+_Data.x)*_Data.y;
                d = max(0,min(1,(1-d)*_Data.w));
				UNITY_APPLY_FOG(i.fogCoord, col);
                float height = min(1,i.wPos.y+_Data.z);
				return col*d*_Color*height;
			}
			ENDCG
		}
	}
}
