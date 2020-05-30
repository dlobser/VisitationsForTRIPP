Shader "Unlit/2DSunLighthouseSimple"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _RampTex ("Texture", 2D) = "white" {}
        
        _Data("Data",vector) = (0,0,0,0)
        _Data2("Data2",vector) = (0,0,0,0)
        _TexData("Data",vector) = (1,1,0,0)
        _ColorA("Color",color) = (1,1,1,1)
        _ColorB("Color",color) = (1,1,1,1)
        _ColorC("Color",color) = (1,1,1,1)
        
        _Color("Color",Color) = (1,1,1,1)
        _Color2("Color2",Color) = (1,1,1,1)
        _Size("Size",Float) = 1
        _Radius("Radius",Float) = 1
        _Pow("Power",Float) = 1
        _Speed("Speed",Float) = 0

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
                float4 color : TANGENT;
			};

			sampler2D _MainTex;
            sampler2D _RampTex;
			float4 _MainTex_ST;
            float4 _Data;
            float4 _Data2;
            float4 _TexData;
            float4 _ColorA;
            float4 _ColorB;
            float4 _ColorC;
            
            float _Size;
            float _Radius;
            float _Pow;
            float4 _Color;
            float4 _Color2;
            float _Speed;
			
             float4 f4(float f){
                return float4(f,f,f,f);
            }
            
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
                o.wPos = mul (unity_ObjectToWorld, v.vertex).xyz;
                o.color = float4(0,0,0,0);
                
                float flash = (1+sin(_Time.y*6.28*10));
                float flash2 = (1+sin(_Time.y*2))*.5;
                flash*=flash2;
                float4 flash3 = lerp(_Color,_Color2,flash)*(1-v.uv.y)*5;
                o.color = flash3;
                o.color.a = flash;
                //o.color.r = flash2;
				return o;
			}
			
           
			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 r = tex2D(_RampTex, i.uv.yx + float2(_Time.y*_Speed,0));
                r*=i.color;
                
                fixed4 col = tex2D(_MainTex, i.uv * _TexData.xy + float2(0,_Time.x*_TexData.z*1.5));
				fixed4 col2 = tex2D(_MainTex, i.uv * _TexData.xy + float2(0,_Time.x*_TexData.z) + col.xy*_TexData.w);
                float bright = 1-(max(0,(i.uv.y+_Data.x)*_Data.y));
                float4 oc = (pow(col2,_Data.w))+pow(bright,2);//*c;
                oc*=bright;
                col2.r = (col2.r + col2.g + col2.b)/3;
               //float4 desatCol = lerp(oc.rrrr*float4(0,0,1,1),oc,.5);
				return r.g*col2.r*_ColorB+(oc.r*_ColorC)*(1-(r.b*col2.r))*(r.r*.1+1)*_Data2.x;//_Data2.x*(flash3+float4(oc.r,oc.g,oc.b,bright)*_ColorC + lerp(saturate((pow(1-i.uv.y,3))*5),-2,flash3*(1-i.uv.y)));
			}
			ENDCG
		}
	}
}
