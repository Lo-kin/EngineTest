#version 330 core

layout(location = 0) in vec3 aPosition;  

layout(location = 1) in vec4 aColor;

layout(location = 2) in vec2 vUv;

out vec4 outColor;
out vec2 fUv;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

void main(void)
{
    gl_Position = uProjection * uView * uModel * vec4(aPosition , 1.0); 
	outColor = aColor;
	fUv = vUv;
}