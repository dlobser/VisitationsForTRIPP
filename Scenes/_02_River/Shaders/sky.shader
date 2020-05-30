Shader "Unlit/sky"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorA("Color A", color) = (1,1,1,1)
        _ColorB("Color B", color) = (1,1,1,1)
        _Data("Data",vector) = (1,1,1,1)
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
                fixed4 colb = tex2D(_MainTex,20*i.uv + float2(_Time.x*.15,_Time.x*.15));

                fixed4 cola = tex2D(_MainTex, i.uv + float2(_Time.x*.05,_Time.x*.05));
                fixed4 col = tex2D(_MainTex, i.uv+cola.xy*.05+colb*.004);
                // apply fog
                float l = pow(i.uv.y,_Data.x)*_Data.y+_Data.z;
                float ll = pow(i.uv.y,_Data.x*.25)*_Data.y*.85-_Data.w;
                float4 c = lerp(_ColorA,_ColorB,min(1,l));
                UNITY_APPLY_FOG(i.fogCoord, col);
                return lerp(c*col+c*.3,_ColorB,max(0,min(ll,1)));
            }
            ENDCG
        }
    }
}
