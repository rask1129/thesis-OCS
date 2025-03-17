using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // �t�@�C������ɕK�v
using System;

public class PathWithLinearInterpolation : MonoBehaviour
{
    public List<Collider> taskColliders;
    public LineRenderer lineRenderer;
    public float lineWidth = 0.1f;
    public Color gizmoLineColor = Color.green;

    private List<Vector3> pathPoints = new List<Vector3>();
    private List<Vector3> smoothPathPoints = new List<Vector3>();

    // �ۑ���f�B���N�g�����w��
    public string saveDirectory = @"C:\Users\futol\Downloads\Simulator-system-main";

    void Start()
    {
        if (lineRenderer != null)
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }

        // �o�H�𐶐�
        GeneratePath();
        GenerateSmoothPath();

        // LineRenderer�Ōo�H��`��
        if (lineRenderer != null)
        {
            DrawPathWithLineRenderer();
        }

        // �o�H��ۑ�
        SavePathToCSV();
    }

    void OnDrawGizmos()
    {
        if (taskColliders == null || taskColliders.Count < 2)
        {
            return;
        }

        GeneratePath();
        GenerateSmoothPath();
        DrawPathWithGizmos();
    }

    void GeneratePath()
    {
        pathPoints.Clear();

        foreach (var collider in taskColliders)
        {
            if (collider != null)
            {
                pathPoints.Add(collider.bounds.center);
            }
        }
    }

    void GenerateSmoothPath()
    {
        smoothPathPoints.Clear();

        if (pathPoints.Count < 2) return;

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Vector3 p0 = pathPoints[i];
            Vector3 p1 = pathPoints[i + 1];

            for (int j = 0; j <= 10; j++)
            {
                float t = j / 10.0f;
                smoothPathPoints.Add(Vector3.Lerp(p0, p1, t));
            }
        }
    }

    void DrawPathWithGizmos()
    {
        Gizmos.color = gizmoLineColor;

        for (int i = 0; i < smoothPathPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(smoothPathPoints[i], smoothPathPoints[i + 1]);
        }
    }

    void DrawPathWithLineRenderer()
    {
        if (smoothPathPoints.Count < 2)
        {
            Debug.LogWarning("���炩�Ȍo�H��`�悷��ɂ�2�ȏ�̃|�C���g���K�v�ł��B");
            return;
        }

        lineRenderer.positionCount = smoothPathPoints.Count;

        for (int i = 0; i < smoothPathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, smoothPathPoints[i]);
        }
    }

    // �o�H��CSV�ɕۑ�
    void SavePathToCSV()
    {
        try
        {
            // �f�B���N�g�������݂��Ȃ��ꍇ�͍쐬
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            // �t�@�C�����𐶐�
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"SmoothPath_{timestamp}.csv";
            string fullPath = Path.Combine(saveDirectory, fileName);

            // �t�@�C���ɏ�������
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("X,Y,Z"); // �w�b�_�[�s
                foreach (var point in smoothPathPoints)
                {
                    writer.WriteLine($"{point.x},{point.y},{point.z}");
                }
            }

            Debug.Log($"Path saved to: {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save path to CSV: {e.Message}");
        }
    }
}
