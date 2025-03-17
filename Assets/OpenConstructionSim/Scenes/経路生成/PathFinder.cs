using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Terrain terrain;
    public Vector3 startPoint; // �X�^�[�g�n�_
    public Vector3 goalPoint;  // �S�[���n�_
    public List<List<Vector3>> nodeGroups; // �����̃m�[�h�Q

    private List<Vector3> path = new List<Vector3>(); // �o�H���X�g

    // �m�[�h�Q�͈̔�
    public Vector2[] nodeGroup1Range; // ��1�m�[�h�Q
    public Vector2[] nodeGroup2Range; // ��2�m�[�h�Q
    public Vector2[] nodeGroup3Range;
    public Vector2[] nodeGroup4Range;
    public Vector2[] nodeGroup5Range;
    public Vector2[] nodeGroup6Range;
    public Vector2[] nodeGroup7Range;
    public Vector2[] nodeGroup8Range;
    public Vector2[] nodeGroup9Range;
    public Vector2[] nodeGroup10Range;
    public Vector2[] nodeGroup11Range;
    public Vector2[] nodeGroup12Range;
    public Vector2[] nodeGroup13Range;
    public Vector2[] nodeGroup14Range;
    public Vector2[] nodeGroup15Range;
    // �ǉ��̃m�[�h�Q������΂����ɒ�`

    void Start()
    {
        nodeGroups = new List<List<Vector3>>();

        // �m�[�h�Q�𐶐�
        GenerateNodeGroup(nodeGroup1Range);
        GenerateNodeGroup(nodeGroup2Range);
        // �K�v�ɉ����đ��̃m�[�h�Q��ǉ�

        GeneratePath();
    }

    // �m�[�h�Q���w�肳�ꂽ�͈͂Ɋ�Â��Đ���
    void GenerateNodeGroup(Vector2[] range)
    {
        List<Vector3> nodesInGroup = new List<Vector3>();

        // �͈͂Ɋ�Â��ăm�[�h�𐶐�
        for (float x = range[0].x; x <= range[1].x; x += 0.1f) // 0.1 Unity�P�� (10cm)
        {
            for (float z = range[0].y; z <= range[1].y; z += 0.1f)
            {
                // �������擾
                float y = terrain.SampleHeight(new Vector3(x, 0, z));

                // �m�[�h���쐬���ă��X�g�ɒǉ�
                Vector3 node = new Vector3(x, y, z);
                nodesInGroup.Add(node);
            }
        }

        nodeGroups.Add(nodesInGroup);
    }

    void GeneratePath()
    {
        // �ŏ��̃X�^�[�g�n�_���o�H�ɒǉ�
        path.Add(startPoint);
        Vector3 currentPoint = startPoint;

        // ���ׂẴm�[�h�Q��ʉ߂���܂ŌJ��Ԃ�
        foreach (var nodeGroup in nodeGroups)
        {
            // ���݂̒n�_����ł��߂��m�[�h��I��
            Vector3 closestNode = GetClosestNode(currentPoint, nodeGroup);
            path.Add(closestNode);

            // �I�񂾃m�[�h�����̃X�^�[�g�n�_�Ƃ��Đݒ�
            currentPoint = closestNode;
        }

        // �Ō�ɃS�[���n�_�ɍł��߂��m�[�h��ǉ�
        Vector3 closestGoalNode = GetClosestNode(currentPoint, new List<Vector3> { goalPoint });
        path.Add(closestGoalNode);
    }

    // ���݂̈ʒu����m�[�h�Q���ōł��߂��m�[�h��I��
    Vector3 GetClosestNode(Vector3 currentPoint, List<Vector3> nodeGroup)
    {
        Vector3 closestNode = nodeGroup[0];
        float closestDistance = Vector3.Distance(currentPoint, closestNode);

        foreach (var node in nodeGroup)
        {
            float distance = Vector3.Distance(currentPoint, node);
            if (distance < closestDistance)
            {
                closestNode = node;
                closestDistance = distance;
            }
        }

        return closestNode;
    }

    // �o�H�̉���
    void OnDrawGizmos()
    {
        if (path.Count > 1)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }
    }
}
