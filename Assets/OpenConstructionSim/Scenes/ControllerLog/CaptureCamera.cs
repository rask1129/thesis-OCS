using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CaptureCamera : MonoBehaviour
{
    public Camera mapCamera;
    public string saveDirectory = @"C:\Users\futol\Downloads\Simulator-system-main";



    public void CaptureAndSave()
    {
        // RenderTexture��p��
        RenderTexture renderTexture = new RenderTexture(1920, 1080, 24);
        mapCamera.targetTexture = renderTexture;

        // �J�����ɕ`�悳����
        Texture2D screenShot = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        mapCamera.Render();

        // RenderTexture����e�N�X�`�����擾
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, 1920, 1080), 0, 0);
        screenShot.Apply();

        // �ۑ��p�t�@�C�����𐶐��i�����Ɋ�Â��j
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"Map_{timeStamp}.png";
        string fullPath = Path.Combine(saveDirectory, fileName);

        // �ۑ���f�B���N�g�������݂��Ȃ��ꍇ�͍쐬
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(fullPath, bytes);

        mapCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        Debug.Log($"�摜���ۑ�����܂���: {fullPath}");
    }
}
