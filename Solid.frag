#version 330 core

out vec4 outputColor;

in vec4 outColor;

void main()
{
    outputColor = vec4(outColor);
}