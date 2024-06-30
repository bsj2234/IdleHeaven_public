using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private bool Looping;
    [SerializeField] private Player _player;

    public int CurrentWaveIndex = 0;

    public List<Wave> Waves => _waves;

    private void Start()
    {
        foreach (Wave wave in _waves)
        {
            wave.OnWaveCompleted += HandleOnWaveCompleted;
        }
    }

    private void HandleOnWaveCompleted()
    {
        Waves[CurrentWaveIndex].gameObject.SetActive(false);

        if (Looping )
        {
            // �ٵ� ���̺� ���� �÷��̾ �ٽ� �������ϰ� �����ϰ� �ؾ� �ϴµ�
            // ���⿡�� ���� �÷��̾� ����(ü�� ���� ) ���̺갡 ���µǾ�� ����
            Waves[CurrentWaveIndex].ResetWave();
            Waves[CurrentWaveIndex].gameObject.SetActive(true);
            _player.ResetPlayer();
            return;
            
        }

        CurrentWaveIndex++;
        if(CurrentWaveIndex >= Waves.Count)
        {
            Debug.Log("Stage Completed");

            return;
        }
        Waves[CurrentWaveIndex].gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        Waves[CurrentWaveIndex].gameObject.SetActive(true);
    }

    public void OnPlayerDead(Attack attacker, Health player)
    {
        Looping = true;
        if(CurrentWaveIndex > 0 )
        {
            Waves[CurrentWaveIndex].ResetWave();
            player.ResetDead();
            _player.ResetPlayer();
            CurrentWaveIndex--;
        }
        else
        {
            //�������� ������ �� ���������� ���ư���
            Waves[CurrentWaveIndex].ResetWave();
            player.ResetDead();
            _player.ResetPlayer();
        }
    }
}
