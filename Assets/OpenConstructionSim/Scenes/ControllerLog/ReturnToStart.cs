using UnityEngine;

public class ReturnToStart : MonoBehaviour
{
    [SerializeField] private Collider startPointCollider; // �o���n�_��Collider�i�G�f�B�^�Ŏw��j
    [SerializeField] private GameObject targetObject;     // �V���x���J�[�Ȃǂ̑ΏۃI�u�W�F�N�g�i�G�f�B�^�Ŏw��j

    private bool hasExited = false;  // �o���n�_���o�����ǂ������L�^����t���O
    private bool hasStarted = false; // �ΏۃI�u�W�F�N�g�������ʒu�ɑ��݂��Ă��������L�^����t���O

    private void Start()
    {
        // �K�v�ȃR���|�[�l���g���ݒ肳��Ă��邩�m�F
        if (startPointCollider == null)
        {
            Debug.LogError("�o���n�_��Collider���ݒ肳��Ă��܂���BInspector�Ŏw�肵�Ă��������B");
        }

        if (targetObject == null)
        {
            Debug.LogError("�ΏۃI�u�W�F�N�g���ݒ肳��Ă��܂���BInspector�Ŏw�肵�Ă��������B");
        }
    }

    private void Update()
    {
        if (startPointCollider == null || targetObject == null)
            return;

        // �ΏۃI�u�W�F�N�g���o���n�_��Collider���ɑ��݂��邩�m�F
        if (startPointCollider.bounds.Contains(targetObject.transform.position))
        {
            if (!hasStarted)
            {
                // �����ʒu�ɑΏۃI�u�W�F�N�g�����݂���ꍇ
                Debug.Log("�ΏۃI�u�W�F�N�g�������ʒu�ɑ��݂��Ă��܂��B�Q�[���J�n���L�^���܂��B");
                hasStarted = true;
            }
            else if (hasExited)
            {
                // ���łɏo�����Ė߂��Ă����ꍇ
                Debug.Log("�ΏۃI�u�W�F�N�g���A�����܂����B�Q�[�����I�����܂��B");
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Unity�G�f�B�^�p
#endif
            }
        }
        else if (hasStarted)
        {
            // �ΏۃI�u�W�F�N�g���o���n�_�𗣂ꂽ�ꍇ
            hasExited = true;
            Debug.Log("�ΏۃI�u�W�F�N�g���o���n�_���o�����܂����B");
        }
    }
}
