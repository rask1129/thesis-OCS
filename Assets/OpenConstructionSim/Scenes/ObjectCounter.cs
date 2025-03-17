using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCounter : MonoBehaviour
{
    public string[] objectTags; // �J�E���g�Ώۂ̃^�O�̔z��
    public Text uiText; // UI�ɕ\������e�L�X�g
    public Collider areaCollider; // �G���A��\��Collider

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    // UI���X�V���郁�\�b�h
    void UpdateUI()
    {
        if (uiText != null)
        {
            string displayText = "�I�u�W�F�N�g�̌�:\n";
            foreach (string tag in objectTags)
            {
                int count = CountObjectsInArea(tag, areaCollider);
                displayText += $"{tag}: {count}\n";
            }
            uiText.text = displayText;
        }
        else
        {
            Debug.LogWarning("UI Text �R���|�[�l���g���ݒ肳��Ă��܂���B");
        }
    }

    // �w�肳�ꂽ�^�O�̃I�u�W�F�N�g�̌����G���A���Ōv�Z���郁�\�b�h
    int CountObjectsInArea(string tag, Collider area)
    {
        if (area == null)
        {
            Debug.LogWarning("�G���ACollider���ݒ肳��Ă��܂���B");
            return 0;
        }

        // �G���A��Bounds���擾
        Bounds bounds = area.bounds;

        // �w�肳�ꂽ�^�O�����S�I�u�W�F�N�g���擾
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        int count = 0;

        // �e�I�u�W�F�N�g���G���A���ɂ��邩���`�F�b�N
        foreach (GameObject obj in objects)
        {
            if (bounds.Contains(obj.transform.position))
            {
                count++;
            }
        }

        // ����Ԃ�
        return count;
    }
}
