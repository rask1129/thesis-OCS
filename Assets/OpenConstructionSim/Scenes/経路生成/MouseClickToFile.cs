using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveClickCoordinates : MonoBehaviour
{
    public Camera orthographicCamera;  // ���s���e�J����
    public Terrain terrain;           // �Ώۂ�Terrain
    private string filePath = @"C:\Users\futol\Downloads\Simulator-system-main\click_positions.txt";

    void Start()
    {
        // �f�B���N�g���m�F�ƍ쐬
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // �t�@�C�������݂��Ȃ��ꍇ�̓w�b�_�[��ǉ�
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Click Positions (x, z)\n");
        }
    }

    void Update()
    {
        // ���N���b�N�܂��̓^�b�v�����m
        if (Input.GetMouseButtonDown(0))
        {
            // �X�N���[�����W���擾
            Vector3 screenPosition = Input.mousePosition;

            // �X�N���[�����W��Ray�ɕϊ�
            Ray ray = orthographicCamera.ScreenPointToRay(screenPosition);

            // Raycast��Terrain�Ƃ̌�_�����o
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 worldPosition = hit.point;

                // x, z���W���擾
                float x = worldPosition.x;
                float z = worldPosition.z;

                // �e�L�X�g�t�@�C���ɕۑ�
                SavePositionToFile(x, z);

                Debug.Log($"�N���b�N���ꂽ�ʒu: x = {x}, z = {z}");
            }
        }
    }

    // ���W���t�@�C���ɕۑ�
    private void SavePositionToFile(float x, float z)
    {
        try
        {
            string data = $"{x}, {z}\n";
            File.AppendAllText(filePath, data);
        }
        catch (IOException ex)
        {
            Debug.LogError($"�t�@�C���������݃G���[: {ex.Message}");
        }
    }
}
