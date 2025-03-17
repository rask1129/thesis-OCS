using UnityEngine;

public class LineRendererRecorder : MonoBehaviour
{
    public LineRenderer lineRenderer; // LineRenderer���A�^�b�`

    void Start()
    {
        if (lineRenderer != null)
        {
            Debug.Log("LineRenderer initialized.");
        }
        else
        {
            Debug.LogError("LineRenderer not assigned!");
        }
    }

    void Update()
    {
        if (lineRenderer != null)
        {
            MonitorLineRenderer();
        }
    }

    private void MonitorLineRenderer()
    {
        // LineRenderer�̒��_�����擾
        int vertexCount = lineRenderer.positionCount;

        // �f�o�b�O�p�ɒ��_�����o�́i�K�v�ɉ����č폜�j
        Debug.Log($"LineRenderer vertex count: {vertexCount}");
    }
}
