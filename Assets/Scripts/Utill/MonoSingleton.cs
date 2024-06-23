using UnityEngine;
using UnityEngine.Assertions;

//�̱���
//�ϳ��� �ν��Ͻ��� �����ϴ� Ŭ����
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance { get; set; }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                Assert.IsNotNull(instance, "There is no instancable object");
            }
            Assert.IsNotNull(instance, "There is no instancable object");
            return instance;
        }
    }
}