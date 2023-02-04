using UnityEngine;
using Wrecking_Clone.Core;
namespace Wrecking_Clone.GameMechanic
{
    public class EnemyManager : MonoBehaviour
    {
        private bool isWin;
        public const int AtackTurnSpeed = 100;

        private void Awake()
        {

            isWin = false;
        }

        void Update()
        {
            if (transform.childCount <= 0 && !isWin)
            {
                GameManager.instance.Win();
                isWin = true;
            }
#if UNITY_EDITOR
            //TEST
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayGame();
            }
#endif
        }
        public void PlayGame()
        {
            GameManager.isGamePlay = true;
        }
    }
}