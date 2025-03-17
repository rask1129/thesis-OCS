using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    public Material material1; // �͈͓��ɃI�u�W�F�N�g������Ƃ��̃}�e���A��
    public Material material2; // �͈͓��ɃI�u�W�F�N�g�����Ȃ��Ƃ��̃}�e���A��
    public string targetTag = "TargetObject"; // ���m����^�O
    private Renderer planeRenderer; // ���ʃI�u�W�F�N�g��Renderer
    private bool isObjectInRange = false; // �I�u�W�F�N�g���͈͓��ɂ��邩�ǂ���

    void Start()
    {
        // ���ʃI�u�W�F�N�g��Renderer���擾
        planeRenderer = GetComponent<Renderer>();
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        // �I�u�W�F�N�g�̗L���ɉ����ă}�e���A�����X�V
        planeRenderer.material = isObjectInRange ? material1 : material2;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �w�肵���^�O�̃I�u�W�F�N�g���������ꍇ
        if (other.CompareTag(targetTag))
        {
            isObjectInRange = true;
            UpdateMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �w�肵���^�O�̃I�u�W�F�N�g���o���ꍇ
        if (other.CompareTag(targetTag))
        {
            isObjectInRange = false;
            UpdateMaterial();
        }
    }
}
