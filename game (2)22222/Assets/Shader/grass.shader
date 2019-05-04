Shader "Custom/grass" {
	Properties{
		_Color("Main Color",Color)=(1,1,1,1)
		_Ambient("环境光",Color)=(0.3,0.3,0.3,0.3)
		_Emission("自发光",Color)=(1,1,1,1)
	}
	SubShader{
		Tags{"Queue"="Transparent"}
		pass{
			Blend SrcAlpha OneMinusSrcAlpha

			Material{
				DIFFUSE[_Color]
				AMBIENT[_Ambient]
				EMISSION[_Emission]
			}
			Lighting on
		}
	}
}
