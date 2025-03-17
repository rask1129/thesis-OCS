using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerActionUI : MonoBehaviour
{
    public Text actionText; // UI�e�L�X�g�p�̕ϐ��i�C���X�y�N�^�[�Őݒ�j

    private bool isRightCrawlerReversed = false; // �E�N���[���[�̉�]���]�t���O
    private bool isLeftCrawlerReversed = false;  // ���N���[���[�̉�]���]�t���O

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // �X�e�B�b�N�̓��͂��擾
        Vector2 leftStick = gamepad.leftStick.ReadValue();
        Vector2 rightStick = gamepad.rightStick.ReadValue();

        // �g���K�[�iR2, L2�j�̒l���擾
        float rightTrigger = gamepad.rightTrigger.ReadValue();
        float leftTrigger = gamepad.leftTrigger.ReadValue();

        // �{�^���̓��͂��m�F
        if (gamepad.rightShoulder.wasPressedThisFrame)
        {
            isRightCrawlerReversed = !isRightCrawlerReversed; // �E�N���[���[�̉�]�����𔽓]
        }

        if (gamepad.leftShoulder.wasPressedThisFrame)
        {
            isLeftCrawlerReversed = !isLeftCrawlerReversed; // ���N���[���[�̉�]�����𔽓]
        }

        // ���͂ɉ������A�N�V�����̕�����𐶐�
        string actionMessage = "";

        // ���X�e�B�b�N��X��
        if (leftStick.x > 0)
        {
            actionMessage += "�E����\n";
        }
        else if (leftStick.x < 0)
        {
            actionMessage += "������\n";
        }

        // ���X�e�B�b�N��Y��
        if (leftStick.y > 0)
        {
            actionMessage += "�u�[���J\n";
        }
        else if (leftStick.y < 0)
        {
            actionMessage += "�u�[����\n";
        }

        // �E�X�e�B�b�N��X��
        if (rightStick.x > 0)
        {
            actionMessage += "�o�P�b�g�J\n";
        }
        else if (rightStick.x < 0)
        {
            actionMessage += "�o�P�b�g��\n";
        }

        // �E�X�e�B�b�N��Y��
        if (rightStick.y > 0)
        {
            actionMessage += "�u�[���J\n";
        }
        else if (rightStick.y < 0)
        {
            actionMessage += "�u�[����\n";
        }

        // �N���[���[�̓���
        if (rightTrigger > 0 && leftTrigger > 0) // R2��L2�������ɉ�����Ă���
        {
            if (!isRightCrawlerReversed && !isLeftCrawlerReversed)
            {
                actionMessage += "�O�i\n";
            }
            else if (isRightCrawlerReversed && !isLeftCrawlerReversed)
            {
                actionMessage += "�E��]\n";
            }
            else if (!isRightCrawlerReversed && isLeftCrawlerReversed)
            {
                actionMessage += "����]\n";
            }
            else if (isRightCrawlerReversed && isLeftCrawlerReversed)
            {
                actionMessage += "���\n";
            }
        }

        // �A�N�V�������b�Z�[�W��UI�ɕ\��
        actionText.text = actionMessage;
    }
}
