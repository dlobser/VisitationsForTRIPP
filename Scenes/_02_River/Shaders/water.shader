// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/water"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color A", color) = (1,1,1,1)
        _ColorA("Color A", color) = (1,1,1,1)
        _ColorB("Color B", color) = (1,1,1,1)
        _ColorC("Color B", color) = (1,1,1,1)
        _Data("Data",vector) = (1,1,1,1)
        _Center("center",vector) = (0,0,0,0)
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
                float3 wPos : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 wPos : NORMAL;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _ColorA;
            float4 _ColorB;
            float4 _ColorC;

            float4 _Data;

            float4 _Center;

            v2f vert (appdata v)
            {
                v2f o;
                float lump = (cos(v.uv.y*6.28)-1)*-.5;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 ov = mul(unity_ObjectToWorld,v.vertex);
                o.vertex += float4(0,sin(_Time.x*_Data.z+(ov.x+ov.z)*_Data.x)*lump*_Data.y,0,0);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.wPos = mul(unity_ObjectToWorld,v.vertex).xyz;
                
                //float lump = (cos(i.uv.y*6.28)-1)*-.5;
                float c = (1-(distance(_Center.xyz,o.wPos)+_Data.w))*_Center.w;
                o.color = (max(0,min(1,c))*_ColorC);
                o.color.a = lump;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv + float2(_Time.x*.2,0));
                fixed4 col2 = tex2D(_MainTex, i.uv+col.xy*.2+ float2(_Time.x,0));
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                //float lump = (cos(i.uv.y*6.28)-1)*-.5;
                float b = pow(col2,10);
                //float c = (1-(distance(_Center.xyz,i.wPos)+_Data.w))*_Center.w;
                return b+lerp(_ColorA,_ColorB,col2)*i.color.a+i.color;//*lump+(max(0,min(1,c))*col2*_ColorC);
            }
            ENDCG
        }
    }
}
