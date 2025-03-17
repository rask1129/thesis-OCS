using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FisheyeCamera : MonoBehaviour
{
    public Material fisheyeMaterial;  // ������ʂ�K�p����}�e���A��
    [Range(180f, 360f)]
    public float fieldOfView = 180f;  // ����p�̒���

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (fisheyeMaterial != null)
        {
            // ����p���V�F�[�_�ɓn��
            fisheyeMaterial.SetFloat("_Fov", fieldOfView);
            Graphics.Blit(src, dest, fisheyeMaterial);
        }
        else
        {
            // �V�F�[�_���ݒ肳��Ă��Ȃ��ꍇ�͂��̂܂ܕ`��
            Graphics.Blit(src, dest);
        }
    }

    void Update()
    {
        // ����p���X���C�_�[�ȂǂŒ����ł���悤��
        if (fisheyeMaterial != null)
        {
            fisheyeMaterial.SetFloat("_Fov", fieldOfView);
        }
    }
}
