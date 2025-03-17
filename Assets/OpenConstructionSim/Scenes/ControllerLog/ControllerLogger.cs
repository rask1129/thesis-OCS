using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerLogger : MonoBehaviour
{
    private string logFilePath;

    void Start()
    {
        // �w��̃f�B���N�g���Ƀ��O�t�@�C����ۑ�
        string directoryPath = @"C:\Users\futol\Downloads\Simulator-system-main"; ;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath); // �f�B���N�g�������݂��Ȃ��ꍇ�͍쐬
        }
        logFilePath = Path.Combine(directoryPath, "ControllerLog.txt");

        // �t�@�C�������쐬
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        logFilePath = Path.Combine(directoryPath, $"ControllerLog_{timestamp}.txt");

        LogMessage("=== Controller Log Started ===");
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // ����̎擾
        Vector2 leftStick = gamepad.leftStick.ReadValue();
        Vector2 rightStick = gamepad.rightStick.ReadValue();
        float leftTrigger = gamepad.leftTrigger.ReadValue();
        float rightTrigger = gamepad.rightTrigger.ReadValue();

        bool l1Button = gamepad.leftShoulder.isPressed; // L1�{�^��
        bool r1Button = gamepad.rightShoulder.isPressed; // R1�{�^��

        // ���O���b�Z�[�W�̐���
        string logMessage = $"Time: {Time.time:F2}, " +
                            $"�u�[���㉺�E����: {leftStick}, �A�[���㉺�E�o�P�b�g�J��: {rightStick}, " +
                            $"�N���[���[�O�i�i���j: {leftTrigger:F2}, �N���[���[�O�i�i�E�j: {rightTrigger:F2}, " +
                            $"�N���[���[�t�](��): {l1Button}, �N���[���[�t�](�E): {r1Button}";

        // ���O���L�^
        LogMessage(logMessage);
    }

    private void LogMessage(string message)
    {
        Debug.Log(message); // Unity�̃R���\�[���ɏo��
        File.AppendAllText(logFilePath, message + "\n"); // �t�@�C���ɒǋL
    }

    void OnApplicationQuit()
    {
        LogMessage("=== Controller Log Ended ===");
    }
}
