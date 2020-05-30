Shader "Unlit/waves"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Data("_Data",vector) = (1,1,1,1)
        _Data2("_Data2",vector) = (1,1,1,1)
        _Color("_Color",color) = (1,1,1,1)
        _ColorB("Color B", color) = (0,0,0,0)
        _Tile("Tile",float) = 1
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
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 tangent : TANGENT;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Data;
            float4 _Data2;
            float4 _Color;
            float4 _ColorB;
            float _Tile;
            
            
             float3 reflRay(float3 dir, float3 norm) {
                float3 ray;
                float ldn = dot(dir, norm);
                float3 refl = 2. * ldn * norm - dir;
                return refl;
            }
            

            v2f vert (appdata v)
            {
                v2f o;
                float2 UV = v.uv;
                v.uv = float2(abs(v.uv.x-.5),v.uv.y);
                float w = sin(sin(v.uv.x*20)+v.uv.y*_Data.y*.3+_Time.y*_Data.x*.3);
                float wave = sin(sin(v.uv.x*40)+v.uv.y*_Data.y+_Time.y*_Data.x)*_Data.w;
                float yup = (cos(v.uv.x*3.14+(3.14*.5))-1.5)*-.5;
                float zup = (cos(v.uv.y*6.28)-1)*-.5;
                float3 wup = float3(0,zup*w*wave*yup+yup*3,pow(v.vertex.z,3)*5);
                o.vertex = UnityObjectToClipPos(v.vertex+wup+float3(0,cos(v.uv.x*3)*2-2,max(0,wup.y-4)*-.02)+
                float3(0,0,pow(abs(UV.x-.5),2)*-3));
                o.uv = TRANSFORM_TEX(UV, _MainTex);
                o.color = float4(v.vertex.x,v.vertex.y,v.vertex.z,wave*w*zup);
                
                float3 forward = mul((float3x3)unity_CameraToWorld, float3(0,0,1));
                o.normal = mul (unity_ObjectToWorld, v.normal).xyz;
                o.normal = reflRay(float3(0,0,1) ,o.normal*wup*10);
                UNITY_TRANSFER_FOG(o,o.vertex);
                float lll = saturate(sin(_Time.y+o.color.x*30)*.5+o.color.z*30+10);
                o.tangent = float3(lll,pow((1-abs(sin(lll+_Time.y*_Data2.y+v.uv.y*10)-.5)),2)*_Data2.z,(sin(_Time.y*6.28*10)*.2)+1);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {                
                float lll = i.tangent.x;
                
                fixed4 cc = tex2D(_MainTex, i.uv*2*_Tile+float2(0,_Time.x*.3))*.5+.5;
                fixed4 ccc = tex2D(_MainTex, i.normal.zy*_Tile+cc.xy*.2*lll+float2(i.color.w*.3,_Time.x*-.3)+lll);
                
                float wave = ((i.color.r+1)*.5);
                fixed4 oo = _Color*ccc*wave+cc*_Color*wave;
                
                float center = pow((1-abs((i.uv.x)-.5)),22)*ccc*_Data2.x*i.tangent.z;
             
                float warper = i.tangent.y*(1-center)*_Color;
                float4 l = lerp(float4(0,0,0,0),oo+center+warper*ccc,lll);

                return l*((oo+i.color.w+1)*.35);
            }
            ENDCG
        }
    }
}
