using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // �؂�ւ������J�����̔z��
    private int currentCameraIndex = 0;

    void Start()
    {
        // �����ݒ�ōŏ��̃J�������A�N�e�B�u�ɂ���
        SetCameraActive(currentCameraIndex);
    }

    void Update()
    {
        // Enter�L�[�ŃJ�����؂�ւ�
        if (Input.GetKeyDown(KeyCode.Return)) // Enter�L�[�ɕύX
        {
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            SetCameraActive(currentCameraIndex);
        }
    }

    private void SetCameraActive(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
    }
}
