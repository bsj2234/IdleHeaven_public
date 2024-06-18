using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    [SerializeField] Transform _target;
    private Vector3 _initialPos;
    private float _time;
    [SerializeField]private float _duration;

    private void Grab()
    {
        _initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        Debug.Assert(_target != null);
        //ó�� ��ġ���� Ÿ�� ��ġ���� �̵���Ŵ
        Vector3 tempPos = Vector3.Lerp(_initialPos, _target.position, _time / _duration);
        //ó���� ���� 0 �̾��ٰ� �߰��� 1 Ÿ����ġ������ 0
        // 0 ~ 1  -.5f abs

        float yTime = Mathf.Abs(_time - _duration * .5f);
        Vector3 yOffset = Vector3.Slerp(Vector3.zero, Vector3.up * 5f, yTime);

        _target.position = tempPos;

        _time += Time.fixedDeltaTime;
    }
}
