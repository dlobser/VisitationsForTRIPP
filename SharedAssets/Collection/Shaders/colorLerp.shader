Shader "Unlit/colorLerp"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _ColorA("Color A",color) = (1,1,1,1)
        _ColorB("Color B",color) = (1,1,1,1)
        _ColorC("Color C",color) = (1,1,1,1)
        _ColorD("Color C",color) = (1,1,1,1)
        _Data("Data",vector) = (1,1,1,1)

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
        Cull Front
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
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float4 _ColorA;
            float4 _ColorB;
            float4 _ColorC;
            float4 _ColorD;
            float4 _Data;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
                float m = min(1,pow(1-distance(i.uv,float2(.5,.5)),10));
                float m2 = min(1,pow(distance(i.uv,float2(.5,.5)),1));
                m2 = (m2-.15)*5;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
                float strobe = ((sin(_Time.y*_Data.x)*.5)+1);
                float4 c = lerp(_ColorA,_ColorB*lerp(_ColorC,_ColorD,strobe),m);
				return c*(1-m2) ;
			}
			ENDCG
		}
	}
}
