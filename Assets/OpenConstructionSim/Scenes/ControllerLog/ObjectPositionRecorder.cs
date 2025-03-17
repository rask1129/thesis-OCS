using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class ObjectPositionRecorder : MonoBehaviour
{
    public GameObject targetObject; // �L�^�Ώۂ̃I�u�W�F�N�g
    private string saveDirectory = @"C:\Users\futol\Downloads\Simulator-system-main\�����f�[�^\����";
    public float samplingInterval = 0.01f; // �T���v�����O�Ԋu�i10ms = 0.01�b�j

    private float lastSampleTime = 0f; // �Ō�ɃT���v�����O��������
    private List<string> recordedData = new List<string>(); // �L�^���ꂽ�f�[�^�i�����񃊃X�g�j
    private DateTime startTime; // ���s�J�n����
    private string executionLogFileName = "ExecutionLog_sato.txt"; // ���s���Ԃ��L�^����t�@�C����

    void Start()
    {
        // ���s�J�n���Ԃ��L�^
        startTime = DateTime.Now;

        // �w�b�_�[�����X�g�ɒǉ�
        recordedData.Add("X,Y,Z");

        // �^�[�Q�b�g�I�u�W�F�N�g���ݒ肳��Ă��Ȃ��ꍇ�A�x�����o��
        if (targetObject == null)
        {
            Debug.LogWarning("No target object specified. Position recording will not proceed.");
        }
        else
        {
            Debug.Log("Position recorder started for object: " + targetObject.name);
        }

        // ���s�J�n���Ԃ����O�t�@�C���ɋL�^
        LogExecutionTime("Application started");
    }

    void Update()
    {
        // �^�[�Q�b�g�I�u�W�F�N�g���ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        if (targetObject == null)
            return;

        // �T���v�����O�Ԋu�𖞂����Ă���ꍇ�̂݋L�^
        if (Time.time - lastSampleTime >= samplingInterval)
        {
            RecordPosition();
            lastSampleTime = Time.time; // �Ō�ɃT���v�����O�������Ԃ��X�V
        }
    }

    private void RecordPosition()
    {
        // �^�[�Q�b�g�I�u�W�F�N�g�̌��ݍ��W���擾
        Vector3 position = targetObject.transform.position;

        // ���W���t�H�[�}�b�g���ă��X�g�ɒǉ�
        string dataLine = $"{position.x:F3},{position.y:F3},{position.z:F3}";
        recordedData.Add(dataLine);

        Debug.Log($"Recorded for {targetObject.name}: {dataLine}");
    }

    private void OnApplicationQuit()
    {
        SaveToCSV();

        // ���s�I�����Ԃ����O�t�@�C���ɋL�^
        LogExecutionTime("Application ended");
    }

    private void SaveToCSV()
    {
        try
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"ObjectPosition_{timestamp}.csv";
            string fullPath = Path.Combine(saveDirectory, fileName);

            // ���X�g���̃f�[�^���t�@�C���ɏ����o��
            File.WriteAllLines(fullPath, recordedData);

            Debug.Log($"Position data saved to: {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save position data: {e.Message}");
        }
    }

    private void LogExecutionTime(string eventDescription)
    {
        try
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string logFilePath = Path.Combine(saveDirectory, executionLogFileName);

            // ���s���Ԃ��v�Z
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // ���O���b�Z�[�W���쐬
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {eventDescription}. Elapsed time: {elapsedTime.TotalSeconds:F2} seconds";

            // �t�@�C���ɒǋL
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);

            Debug.Log(logMessage);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to log execution time: {e.Message}");
        }
    }
}
