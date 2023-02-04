using UnityEngine;
using UnityEngine.SceneManagement;
using Wrecking_Clone.Core;

namespace Wrecking_Clone.UI
{
     class UIManager : MonoBehaviour
    {
        public GameObject losePanel;
        public GameObject winPanel;

        public void TouchToStartButton()
        {
            GameManager.instance.StartGame();
        }
        public  void ResetButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public  void ShowLoseUI()
        {
            losePanel.SetActive(true);
        }

        public void ShowWinUI()
        {
            winPanel.SetActive(true);
        }
    }
}