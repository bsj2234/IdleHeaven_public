using UnityEngine;

public class ItemAcquirer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Test Trigger {other.gameObject.name}");
        //���� �Ǵ��ұ�
        if (other.TryGetComponent(out DroppedItem droppedItem))
        {
            if (droppedItem.GetAcquirer() == transform.parent)
            {
                Debug.Log("ItemAcquired");
                Destroy(droppedItem.gameObject);
            }
        }
    }
}
