using UnityEngine;
namespace IdleHeaven
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] Player _player;


        private void Start()
        {
            _player = FindObjectOfType<Player>();

            
        }

        public Player GetPlayer()
        {
            return _player;
        }

        //�÷��̾ ������ �� ���������� �ݺ����� ����

        //
    }
}