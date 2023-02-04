using System;
using UnityEngine;
using Wrecking_Clone.GamePlay;
using Wrecking_Clone.UI;

namespace Wrecking_Clone.Core
{
    public class GameManager : MonoBehaviour
    {
        public static bool isGamePlay = false;

        public static GameManager instance;

        public void StartGame()
        {
            isGamePlay = true;
        }

        UIManager uIManager;
        private void Awake()
        {
            instance = this;
            isGamePlay = false;
            uIManager = FindObjectOfType<UIManager>();
        }
        public void DestroyEnemyParent(EnemyParent enemyParent)
        {
            Destroy(enemyParent.transform.gameObject, 3f);
        }

        public void Lose()
        {
            //EndLevel();
            uIManager.ShowLoseUI();
        }
        public void Win()
        {
            //EndLevel();
            uIManager.ShowWinUI();
        }

        private void EndLevel()
        {
            throw new NotImplementedException();
        }


        void Update()
        {

        }
    }
}