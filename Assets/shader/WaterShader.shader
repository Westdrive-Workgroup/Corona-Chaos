Shader "Unlit/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint" , Color) = (0.0,0.0,0.75,1.0)
        _Direction ("Direction" , Vector) = (0.0,0.0,0.0,0.0)
        _Frequency ("Frequency", Range(0.0,20.0)) = 0.0
        _Steepness ("Steepness" , Range(0.0,10.0)) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Steepness , _Frequency;
            float4 _Direction;
            float4 _Color;
            v2f vert (appdata v)
            {
                v2f o;
                float3 position = v.vertex.xyz;
                float4 direction = normalize(_Direction);
                float defaultWaveLength = 2 * UNITY_PI;
                float waveLength = defaultWaveLength / _Frequency;
                float phase = sqrt(9.8 / waveLength); 
                float displacement = waveLength * (dot(direction,position) - (phase * _Time.y));
                float amplitude = _Steepness / waveLength;
                
                position.x += direction.x * (amplitude * cos(displacement));
                position.y = amplitude * sin(displacement);
                position.z += direction.z * (amplitude * cos(displacement));
                v.vertex.xyz = position;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) + _Color;
                col.a = _Color.a;
                return col;
            }
            ENDCG
        }
    }
}
