using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMaterialChanger : MonoBehaviour
{
    public string[] targetTags; // �K�{�^�O�̃��X�g
    public Material newMaterial; // �����𖞂������ꍇ�̃}�e���A��
    private Material originalMaterial; // ���̃}�e���A��
    private Renderer areaRenderer; // �G���A�I�u�W�F�N�g��Renderer
    private Dictionary<string, bool> tagPresence; // �^�O���Ƃ̃G���A�����݃t���O

    private void Start()
    {
        // �G���A�I�u�W�F�N�g��Renderer���擾���A���̃}�e���A����ۑ�
        areaRenderer = GetComponent<Renderer>();
        if (areaRenderer != null)
        {
            originalMaterial = areaRenderer.material;
        }
        else
        {
            Debug.LogError("Renderer�����̃I�u�W�F�N�g�ɑ��݂��܂���B");
        }

        // �^�O���Ƃ̑��݃t���O��������
        tagPresence = new Dictionary<string, bool>();
        foreach (string tag in targetTags)
        {
            tagPresence[tag] = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �G���A���ɓ������I�u�W�F�N�g�̃^�O���m�F
        if (tagPresence.ContainsKey(other.tag))
        {
            tagPresence[other.tag] = true; // �Y���^�O�̃I�u�W�F�N�g���G���A���ɑ���
            UpdateMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �G���A�O�ɏo���I�u�W�F�N�g�̃^�O���m�F
        if (tagPresence.ContainsKey(other.tag))
        {
            tagPresence[other.tag] = false; // �Y���^�O�̃I�u�W�F�N�g���G���A�O�Ɉړ�
            UpdateMaterial();
        }
    }

    private void UpdateMaterial()
    {
        // ���ׂẴ^�O���G���A���ɑ��݂��邩�m�F
        foreach (string tag in targetTags)
        {
            if (!tagPresence[tag])
            {
                // �������B���̏ꍇ�A���̃}�e���A���ɖ߂�
                areaRenderer.material = originalMaterial;
                return;
            }
        }

        // �����𖞂������ꍇ�A�V�����}�e���A����K�p
        areaRenderer.material = newMaterial;
    }
}
