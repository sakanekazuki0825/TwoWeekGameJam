// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        // �}�e���A���C���X�y�N�^�[�� Color �v���p�e�B�A�f�t�H���g�𔒂�
        _Color("Main Color", Color) = (1, 1, 1, 1)
    }
        SubShader
    {
        Pass
        {
            CGPROGRAM
#pragma vertex vert
#pragma fragment frag

            // ���_�V�F�[�_�[
            // ����́A "appdata" �\���̂̑���ɁA���͂��蓮�ŏ������݂܂�
            // ������ v2f �\���̂�Ԃ�����ɁA1 �̏o��
            // float4 �̃N���b�v�ʒu������Ԃ��܂�
        float4 vert(float4 vertex : POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(vertex);
    }

        // �}�e���A������̃J���[
    fixed4 _Color;

    // �s�N�Z���V�F�[�_�[�A���͕s�v
    fixed4 frag() : SV_Target
    {
        return _Color; // �P�ɕԂ��܂�
    }
        ENDCG
    }
    }
}
