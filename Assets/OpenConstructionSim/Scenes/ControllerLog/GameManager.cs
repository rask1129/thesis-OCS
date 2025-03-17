using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CaptureCamera captureCamera; // CaptureCamera �X�N���v�g���A�^�b�`

    void Start()
    {
        Debug.Log("�Q�[���J�n");
    }

    void OnApplicationQuit()
    {
        // �Q�[���I�����̏���
        Debug.Log("�Q�[���I��");

        // �X�N���[���V���b�g��ۑ�
        if (captureCamera != null)
        {
            captureCamera.CaptureAndSave();
        }
        else
        {
            Debug.LogWarning("CaptureCamera �X�N���v�g���ݒ肳��Ă��܂���I");
        }
    }
}
