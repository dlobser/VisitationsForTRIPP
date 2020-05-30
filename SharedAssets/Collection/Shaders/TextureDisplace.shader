Shader "Unlit/TextureDisplace"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Disp("Displace",2D) = "white" {}
        _Amount("Amount",float) = 0
        _ColorA("Color A",color) = (1,1,1,1)
        _ColorB("Color B",color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
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
                float4 normal :NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
                float4 pos : COLOR;

			};

			sampler2D _MainTex;
            sampler2D _Disp;
			float4 _MainTex_ST;
            float _Amount;
            float4 _ColorA;
            float4 _ColorB;
			
			v2f vert (appdata v)
			{
				v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                fixed4 col = tex2Dlod(_Disp, float4(o.uv.x,o.uv.y+_Time.x,0,0));

				o.vertex = UnityObjectToClipPos(v.vertex + v.normal * _Amount * col.r);
				UNITY_TRANSFER_FOG(o,o.vertex);
                o.normal = v.normal;
                o.pos = v.vertex;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col1 = tex2D(_MainTex, i.uv+_Time.x*.2);
                fixed4 col2 = tex2D(_MainTex, .5*i.uv-_Time.x*.2);
                col1/=col2;
                fixed4 oc = lerp(_ColorA,_ColorB,col1);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
                float4 g = pow(i.normal.r,1)*_ColorA;
				return g+2*(1-i.normal.r)*oc;
			}
			ENDCG
		}
	}
}
