using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] Transform Acquirer;

    public void SetAcquirer(Transform acquirer)
    {
        Acquirer = acquirer;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Test Trigger {other.gameObject.name}");
        //���� �Ǵ��ұ�
        if(other.TryGetComponent(out ItemAcquirer acquirer))
        {
            Debug.Log("ItemAcquired");
            Destroy(gameObject);
        }
    }
}
