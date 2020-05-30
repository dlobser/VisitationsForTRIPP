// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/simple2" {
	Properties {
        _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (0.26,0.19,0.16,0.0)
		_Color2 ("Color2", Color) = (0.26,0.19,0.16,0.0)
		_Lgt1 ("Light 1", Vector) = (1.,0.,23.,0.)
		_Lgt2 ("Light 2", Vector) = (1.,0.,23.,0.)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
			
				struct v2f {
					float4 pos : SV_POSITION;
					float4 wPos : TEXCOORD0;
                    float2 uv : TEXCOORD1;
                    float3 color : COLOR;
				};
			
				float4 _Color;
				float4 _Lgt1;
				float4 _Color2;
				float4 _Lgt2;
				sampler2D _MainTex;
                float4 _MainTex_ST;
					
				float dist(float3 a, float3 b){
					float A = a.x-b.x;
					float B = a.y-b.y;
					float C = a.z-b.z;
					return sqrt(A*A+B*B+C*C);
				}
				float dist2(float a, float b){
					return sqrt(a-b);
				}
			
				v2f vert(appdata_full v) {
					v2f o;
					/*o.pos = UnityObjectToClipPos(v.vertex);
					o.wPos = mul(unity_ObjectToWorld, v.vertex);
					float d = max(1.,(dist(_Lgt1.z,o.wPos.z)*-1.)+125.);
					o.pos.x*=max(1.,d*.009);
					o.pos.y*=max(1.,d*.009);*/
					o.wPos =mul (unity_ObjectToWorld, v.vertex);// mul(unity_ObjectToWorld, v.vertex);
					float dis = max(1., (dist(_Lgt1.z, v.vertex.z)*-1.) + 125.);
					v.vertex.x *= max(1., dis*.009);
					v.vertex.y *= max(1., dis*.009);
                    
                    //float dis = max(.4,distance(_Lgt1, o.wPos)*.0010);
                    //float3 off = normalize((o.wPos-_Lgt1));
                    
					o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                    
                    
                    float d = distance(_Lgt1.xyz,o.wPos.xyz);
                    float p = max(0.,sin(max(0.,d))*6.);
                    float dd = d;
                    dd*=-1;
                    dd+=55;
                    dd+=p;
                    
                    //float db = dist(_Lgt1.xyz,IN.wPos.xyz);//float3(cos(_Lgt1.x*.1)*8.,sin(_Lgt1.y*.1)*8.,_Lgt1.z),IN.wPos.xyz);
                    float db = d;
                    db*=-1;
                    db+=8;
                    
                    float d2 = distance(_Lgt2.xyz,o.wPos.xyz);
                    d2*=-1;
                    d2+=19;
                    d2+=p*.8;
                    
                    //float4 pos = o.wPos;
                    float4 col1 = _Color* max(0.0,dd)*.03*float4(.5,.8,1.,1.);// * ((1.8+sin(_Time.y*10*6.28))*.2);
                    float4 col1b = _Color*max(0.0,db)*.1;
                    float4 col2 = _Color2*max(0.0,d2)*.05;
                    
                    o.color = (col1 + col2 + col1b*3);
                    
					return o;
				}
			
				
			
				
				float4 frag(v2f IN) : COLOR {
                    fixed4 cc = tex2D(_MainTex, IN.uv);

					//float d = distance(_Lgt1.xyz,IN.wPos.xyz);
					//float p = max(0.,sin(max(0.,d))*6.);
     //               float dd = d;
					//dd*=-1;
					//dd+=55;
					//dd+=p;
					
					////float db = dist(_Lgt1.xyz,IN.wPos.xyz);//float3(cos(_Lgt1.x*.1)*8.,sin(_Lgt1.y*.1)*8.,_Lgt1.z),IN.wPos.xyz);
					//float db = d;
     //               db*=-1;
					//db+=8;
					
					//float d2 = distance(_Lgt2.xyz,IN.wPos.xyz);
					//d2*=-1;
					//d2+=19;
					//d2+=p*.8;
					
					//float4 pos = IN.wPos;
					//float4 col1 = _Color* max(0.0,dd)*.03*float4(.5,.8,1.,1.);// * ((1.8+sin(_Time.y*10*6.28))*.2);
					//float4 col1b = _Color*max(0.0,db)*.1;
					//float4 col2 = _Color2*max(0.0,d2)*.05;
					return cc*IN.color.rgbr;// * (col1 + col2 + col1b*3);
				}
			ENDCG
		}
	}
}
