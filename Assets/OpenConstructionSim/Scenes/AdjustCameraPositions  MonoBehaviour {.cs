using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraPositions : MonoBehaviour
{
    public Camera leftEyeCamera;   // ���ڗp�J����
    public Camera rightEyeCamera;  // �E�ڗp�J����
    public float xOffset = 0.1f;   // x���W�ɉ�����I�t�Z�b�g

    void Start()
    {
        if (leftEyeCamera != null && rightEyeCamera != null)
        {
            // ���ڃJ�����̈ʒu���X�V
            Vector3 leftEyePosition = leftEyeCamera.transform.position;
            leftEyeCamera.transform.position = new Vector3(leftEyePosition.x - xOffset, leftEyePosition.y, leftEyePosition.z);

            // �E�ڃJ�����̈ʒu���X�V
            Vector3 rightEyePosition = rightEyeCamera.transform.position;
            rightEyeCamera.transform.position = new Vector3(rightEyePosition.x + xOffset, rightEyePosition.y, rightEyePosition.z);
        }
        else
        {
            Debug.LogError("���ڂ܂��͉E�ڂ̃J�������ݒ肳��Ă��܂���B�C���X�y�N�^�[�ŃJ������ݒ肵�Ă��������B");
        }
    }
}
