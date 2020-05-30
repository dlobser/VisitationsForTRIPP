Shader "Unlit/waves3"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Data("_Data",vector) = (1,1,1,1)
        _Color("_Color",color) = (1,1,1,1)
        _ColorB("Color B", color) = (0,0,0,0)
        _Tile("Tile",float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent""Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        //ZTest Off
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Data;
            float4 _Color;
            float4 _ColorB;
            float _Tile;

            v2f vert (appdata v)
            {
                v2f o;
                v.uv = float2(abs((v.uv.x-.5)*1.5),v.uv.y);
                float w = sin(sin(v.uv.x*20)+v.uv.y*_Data.y*.3+_Time.y*_Data.x*.3);
                float wave = sin(sin(v.uv.x*40)+v.uv.y*_Data.y+_Time.y*_Data.x)*_Data.w;
                float yup = (cos(v.uv.x*3.14+(3.14*.5))-1.5)*-.5;
                float zup = (cos(v.uv.y*6.28)-1)*-.5;
                o.vertex = UnityObjectToClipPos(v.vertex+float3(0,zup*w*wave*yup+yup*3+ cos(v.uv.x*3)*10-10,0));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = float4(wave*w*zup,zup*w*wave*yup+yup*3,cos(v.uv.x*3)*1-cos(v.uv.y*6.28)*.5,0);
                o.normal = v.normal;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
            
                //     float w = sin(sin(i.uv.x*20)+i.uv.y*_Data.y*.3+_Time.y*_Data.x*.3);
                //float wave = sin(sin(i.uv.x*40)+i.uv.y*_Data.y+_Time.y*_Data.x)*_Data.w;
                //float yup = (cos(i.uv.x*3.14+(3.14*.5))-1.5)*-.5;
                //float zup = (cos(i.uv.y*6.28)-1)*-.5;
                //float wup = zup*w*wave*yup+yup*3;
                
                // sample the texture
                fixed4 cc = tex2D(_MainTex, i.uv*_Tile);
                cc += tex2D(_MainTex, i.uv*_Tile*3+cc.rg+float2(0,_Time.x));
                cc*=.5;
                cc+=.5;
                fixed4 col = ((i.color.x+1)*.5)*_Color;
                col += lerp(float4(0,0,0,0),_ColorB,pow(i.uv.y+cc.y*.1,2));
                //float mult = cos(i.uv.x*3);
                //mult *= 1-cos(i.uv.y*6.28)*.5;
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //clip(i.color.x-_Data.z);
                col.a = max(0,i.color.y*.25-_Data.z);
                return i.color.z*col;
            }
            ENDCG
        }
    }
}
