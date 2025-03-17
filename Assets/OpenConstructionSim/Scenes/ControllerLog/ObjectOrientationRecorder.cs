using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class ObjectOrientationRecorder : MonoBehaviour
{
    public Transform targetObject; // �L�^�Ώۂ̃I�u�W�F�N�g
    public string saveDirectory = @"C:\Users\futol\Downloads\Simulator-system-main"; // �ۑ���f�B���N�g��
    public float samplingInterval = 0.01f; // �T���v�����O�Ԋu�i10ms = 0.01�b�j

    private List<Quaternion> recordedRotations = new List<Quaternion>(); // �L�^���ꂽ��]���
    private List<float> recordedTimestamps = new List<float>(); // �L�^���ꂽ�^�C���X�^���v
    private float lastSampleTime = 0f; // �Ō�ɃT���v�����O��������
    private DateTime startTime; // ���s�J�n����

    void Start()
    {
        // ���s�J�n�������L�^
        startTime = DateTime.Now;

        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned!");
        }
    }

    void Update()
    {
        // �T���v�����O�Ԋu�𖞂����ꍇ�̂݋L�^
        if (Time.time - lastSampleTime >= samplingInterval && targetObject != null)
        {
            RecordOrientation();
            lastSampleTime = Time.time; // �Ō�ɃT���v�����O�������Ԃ��X�V
        }
    }

    private void RecordOrientation()
    {
        // �I�u�W�F�N�g�̉�]���L�^
        recordedRotations.Add(targetObject.rotation);
        recordedTimestamps.Add(Time.time); // �^�C���X�^���v���L�^
    }

    private void OnApplicationQuit()
    {
        SaveOrientationsToCSV();

        // ���s���Ԃ��v�Z���ă��O�ɏo��
        TimeSpan elapsedTime = DateTime.Now - startTime;
        Debug.Log($"Execution Time: {elapsedTime.TotalSeconds} seconds");
    }

    private void SaveOrientationsToCSV()
    {
        try
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"RecordedOrientations_{timestamp}.csv";
            string fullPath = Path.Combine(saveDirectory, fileName);

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("Time,X,Y,Z,W"); // �w�b�_�[����������
                for (int i = 0; i < recordedRotations.Count; i++)
                {
                    Quaternion rotation = recordedRotations[i];
                    float time = recordedTimestamps[i];
                    writer.WriteLine($"{time},{rotation.x},{rotation.y},{rotation.z},{rotation.w}");
                }
            }

            Debug.Log($"Orientations saved to: {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save orientations to CSV: {e.Message}");
        }
    }
}
