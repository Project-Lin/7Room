Shader "Unlit/Toon_simple"  
{  
    Properties  
    {  
        _BaseMap ("BaseMap", 2D) = "white"{} 
        _ShadowMap ("ShadowMap",  2D) = "white"{} 
        _ILMMap("ILM Map",2D) ="white"{}  
        _InnerLineMap("InnerLine Map",2D) ="white"{}  
        _ToonTheshold ("陰影大小", Range(0,1)) = 0.5  
        _ToonHardness ("陰影硬邊", Range(0,100)) = 60  
        _SpecColor ("高光顏色", Color) = (1,1,1,1)  
        _SpecSize ("高光大小",Range(0,1)) = 0.1  
        _InnerLineColor ("內描線顏色", Color) = (1,1,1,1)  
        _OutlineWidth("外描線寬度",Range(0,10)) = 1  
        _OutlineDistanceScale("外描線距離縮放",Range(0,100)) = 10  
        _OutlineColor("外描線顏色",Color) = (1,1,1,1)  

                  
          
}  
    SubShader  
    {  
        Tags { "RenderType"="Opaque" }  
  
        Pass  
        {   
            
            CGPROGRAM  
            
            #pragma vertex vert  
            #pragma fragment frag  
            //#pragma multi_compile_fwdbase  
              
  
  
              
            #include "UnityCG.cginc"  
            #include "AutoLight.cginc"
  
            struct appdata  
            {  
                float4 vertex : POSITION;  
                float2 texcoord0 : TEXCOORD0;  //UV1  
                float2 texcoord1 : TEXCOORD1;  //UV2  
                float3 normal : NORMAL;  
                float4 color : COLOR;  
                                  
            };  
  
            struct v2f  
            {  
                float4 pos : SV_POSITION;  
                float4 uv : TEXCOORD0;  
                float3 pos_world : TEXCOORD1;  
                float3 normal_world :TEXCOORD2;  
                float4 vertex_color :TEXCOORD3;  
                            };  
  
            sampler2D _BaseMap;
            sampler2D _ShadowMap;
            sampler2D _ILMMap;  
            sampler2D _InnerLineMap;  
  
            float _ToonTheshold ;  
            float _ToonHardness ;  
            float4 _SpecColor ;  
            float _SpecSize ;  
  
            float4 _InnerLineColor ;  
  
            float4 _RimLightDir;  
            float4 _RimLightColor;  
            float _RimLightIntensity;  
  
  
  
            v2f vert (appdata v)  
            {
                v2f o;  
                o.pos = UnityObjectToClipPos(v.vertex);  
                o.pos_world = mul(unity_ObjectToWorld, v.vertex);  
                o.normal_world = mul(unity_ObjectToWorld, float4(v.normal, 0.0)).xyz;  
                o.uv = float4(v.texcoord0, v.texcoord1);  
                o.vertex_color=v.color;  
                return o;  
            }  
            fixed4 frag (v2f i) : SV_Target  
            {  
                half2 uv1 = i.uv.xy;
                half2 uv2 = i.uv.zw;  
                  
                //向量  
                float3 normalDir = normalize(i.normal_world);  
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);  
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.pos_world.xyz);  
                //Base貼圖  
                half4 base_map =tex2D(_BaseMap, uv1);  
                half3 base_color = base_map.rgb;  
                //Shadow貼圖  
                half4 shadow_map = tex2D(_ShadowMap, uv1);  
                half3 shadow_color = shadow_map.rgb;  
                //ILM貼圖  
                half4 ilm_map = tex2D(_ILMMap, uv1);  
                //float spec_intensity = ilm_map.r;  //高光強度  
                float diffuse_control = ilm_map.g;  //固定陰影  
                //float spec_size = ilm_map.b;  //高光大小控制  
                  
                //內描線  
                half4 innerLine_map = tex2D(_InnerLineMap, uv2);  
                float inner_line = innerLine_map.r;  //內描線強度  
                half3 inner_line_color = lerp(half3(1,1,1),_InnerLineColor.r,inner_line);  

                  
                //漫反射  
                half NdotL = dot(normalDir, lightDir);  
                half half_lambert = NdotL * 0.5 + 0.5;  
                half labmbert_term =half_lambert * diffuse_control;  
                half toon_diffuse =saturate((labmbert_term- _ToonTheshold)*_ToonHardness);  
                half3 Final_diffuse = lerp(shadow_color,base_color,toon_diffuse);

  
                //高光  
                // float NdotV=(dot(normalDir,viewDir)+1.0)*0.5;  
                // float spec_term = NdotV;  
                // half toon_spec =saturate((spec_term- (1-spec_size*_SpecSize))*500);  
                // half3 spec_color = (_SpecColor.rgb*(base_color));  
                // half3 Final_spec =toon_spec * spec_color * spec_intensity;  
                
  
  
                //輸出  
                half3 Final_color = (Final_diffuse)*inner_line_color;  
                Final_color = sqrt(max(exp2(log2(max(Final_color,0.0))*2.2),0.0));;  
                return float4( Final_color, 1.0);  
            }  
            ENDCG  
        }  
          
        Pass  
        {  
            
            Cull Front  
            CGPROGRAM
            #pragma vertex vert  
            #pragma fragment frag  
  
              
  
            #include "UnityCG.cginc"  
            #include "AutoLight.cginc"  
  
            struct appdata  
            {  
                float4 vertex : POSITION;  
                float2 texcoord0 : TEXCOORD0;  //UV1  
                float2 texcoord1 : TEXCOORD1;  //UV2  
                float3 normal : NORMAL;  
                float4 color : COLOR;  
  
                float4 tangent : TANGENT;  
                                  
            };  
  
            struct v2f  
            {  
                float4 pos : SV_POSITION;  
                float2 uv : TEXCOORD0;  
                float3 pos_world : TEXCOORD1;  
                float3 normal_world :TEXCOORD2;  
                float4 vertex_color :TEXCOORD3;
                
                
            };  
  
  
            sampler2D _ILMMap;  
            float _OutlineWidth;  
            float4 _OutlineColor;
            float _OutlineDistanceScale;


            //外描線 頂點色R通道
            v2f vert (appdata v)  
            {  
                v2f o;  
                float3 pos_view = UnityObjectToViewPos(v.vertex);  
                // float3 normal_world = UnityObjectToWorldNormal(v.normal);  
                float3 normal_world = UnityObjectToWorldNormal(v.tangent.xyz);  
                float3 normal_dir = mul((float3x3)UNITY_MATRIX_V,normal_world) ;

                float3 pos_world = mul(unity_ObjectToWorld, v.vertex);  


                 float distance = length(_WorldSpaceCameraPos - pos_world);

                 // 根據距離計算外描線寬度的縮放因子
                 float scaleFactor = saturate(distance / (_OutlineDistanceScale*0.1));

                 // 應用縮放因子到外描線寬度
                 float outlineWidth = _OutlineWidth * scaleFactor;

                
                pos_view  += normal_dir * outlineWidth*0.001 * v.color.a;    
                o.pos = mul(UNITY_MATRIX_P,float4(pos_view,1.0));  
                o.uv = v.texcoord0;  
                o.vertex_color=v.color;
                 
  
                return o;   
            }  
  
            fixed4 frag (v2f i) : SV_Target  
            {  
                return _OutlineColor; 

            }

                
            ENDCG  
        }  
  
    }    FallBack "Diffuse"  
}

