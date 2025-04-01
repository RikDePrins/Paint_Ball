Shader "Custom/PaintShaders/WhiteSpaceShader" {
    Properties {
        [HideInInspector] _DrawingTex("Drawing texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _NormalTex("Normal Map", 2D) = "bump" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Use the Standard lighting model with full forward shadows
        #pragma surface surf Standard fullforwardshadows

        // Target shader model 3.0 for better lighting support
        #pragma target 3.0

        sampler2D _DrawingTex;
        sampler2D _MainTex;
        sampler2D _NormalTex;

        struct Input {
            float2 uv_MainTex;
            float2 uv_NormalTex;
            float2 uv2_DrawingTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf(Input IN, inout SurfaceOutputStandard o) {
            // Sample the drawing texture using the second UV set
            fixed4 drawData = tex2D(_DrawingTex, IN.uv2_DrawingTex);
            // Sample the main texture and apply the color tint
            fixed4 mainData = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            // Blend the main texture and drawing texture based on the alpha of the drawing texture
            fixed4 c = lerp(mainData, drawData, drawData.a);
            // Set the alpha to the combined alpha values
            c.a = drawData.a + mainData.a;
            o.Albedo = c.rgb;

            // Sample and apply the normal map
            fixed3 normalMap = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
            // Amplify the normal effect under the paint
            normalMap.xy *= drawData.a;
            o.Normal = normalize(normalMap);

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Standard"
}
