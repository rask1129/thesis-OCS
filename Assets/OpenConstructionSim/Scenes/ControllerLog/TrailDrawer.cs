using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private int pointIndex = 0;

    void Start()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer ���ݒ肳��Ă��܂���I");
            return;
        }

        // �ŏ��̒��_��ݒ�
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    void Update()
    {
        // ���݈ʒu�𒸓_�Ƃ��Ēǉ�
        pointIndex++;
        lineRenderer.positionCount = pointIndex + 1;
        lineRenderer.SetPosition(pointIndex, transform.position);
    }
}
