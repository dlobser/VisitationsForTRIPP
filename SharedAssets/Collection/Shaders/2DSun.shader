Shader "Unlit/2DSun"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Data("Data",vector) = (0,0,0,0)
        _TexData("Data",vector) = (1,1,0,0)
        _ColorA("Color",color) = (1,1,1,1)
        _ColorB("Color",color) = (1,1,1,1)
        _ColorC("Color",color) = (1,1,1,1)

	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
        Blend OneMinusDstColor One
        ZWrite Off
		LOD 100

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
            float4 _TexData;
            float4 _ColorA;
            float4 _ColorB;
            float4 _ColorC;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
                o.wPos = mul (unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
                fixed4 col = tex2D(_MainTex, i.uv * _TexData.xy + float2(0,_Time.x*_TexData.z*1.5));

				fixed4 col2 = tex2D(_MainTex, i.uv * _TexData.xy + float2(0,_Time.x*_TexData.z) + col.xy*_TexData.w);
                float bright = 1-min(1,max(0,(i.uv.y+_Data.x)*_Data.y));
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
                float4 c = lerp(_ColorA,_ColorB,bright);
                float4 oc = (pow(col2,_Data.w))+pow(bright,2)*c;
                float height = min(1,i.wPos.y*3+_Data.z);
                bright*=height;
                oc*=bright;
				return float4(oc.r,oc.g,oc.b,bright)*_ColorC;
			}
			ENDCG
		}
	}
}
