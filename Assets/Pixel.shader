// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Shader "Unlit/Pixao"
//{
//    Properties
//    {
//        _MainTex ("Texture", 2D) = "white" {}
//    }
//    SubShader
//    {
//        Tags { "RenderType"="Opaque" }
//        LOD 100
//
//        Pass
//        {
//            CGPROGRAM
//            #pragma vertex vert
//            #pragma fragment frag
//            // make fog work
//            #pragma multi_compile_fog
//
//            #include "UnityCG.cginc"
//
//            struct appdata
//            {
//                float4 vertex : POSITION;
//                float2 uv : TEXCOORD0;
//            };
//
//            struct v2f
//            {
//                float2 uv : TEXCOORD0;
//                UNITY_FOG_COORDS(1)
//                float4 vertex : SV_POSITION;
//            };
//
//            sampler2D _MainTex;
//            float4 _MainTex_ST;
//
//            v2f vert (appdata v)
//            {
//                v2f o;
//                o.vertex = UnityObjectToClipPos(v.vertex);
//                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//                UNITY_TRANSFER_FOG(o,o.vertex);
//                return o;
//            }
//
//            fixed4 frag (v2f i) : SV_Target
//            {
//                // sample the texture
//                fixed4 col = tex2D(_MainTex, i.uv);
//                // apply fog
//                UNITY_APPLY_FOG(i.fogCoord, col);
//                return col;
//            }
//            ENDCG
//        }
//    }
//}


Shader "Unlit/Grab Pixelization Shader"
{
    Properties 
    {
        _PixelSize ("Pixel Size", Range(0, 1.0)) = 100
    }
     
    SubShader
    {
        Tags { "Queue" = "Transparent+1" }
         
        GrabPass {}
         
        Pass
        {           
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
             
            #include "UnityCG.cginc"
             
            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;
            fixed _PixelSize;
             
            struct appdata
            {
                fixed4 vertex : POSITION;
            };
             
            struct v2f
            {
                fixed4 vertex : SV_POSITION;
                fixed4 uv : TEXCOORD0;
            };
             
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = ComputeScreenPos(o.vertex);
                 
                return o;
            }
             
            fixed4 frag(v2f i) : COLOR
            {
                fixed4 uv = i.uv;
                 
                if(_PixelSize != 0)
                {               
                    uv.xy = fixed2((int)(uv.x / _PixelSize), (int)(uv.y / _PixelSize)) * _PixelSize;
                }
                 
                fixed4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(uv));
                 
                return col;
            }
            ENDCG
        }

    }
}