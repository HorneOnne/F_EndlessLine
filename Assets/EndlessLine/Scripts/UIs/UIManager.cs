using UnityEngine;

namespace EndlessLine
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public UIOther uiOther;
        public UIMainMenu uiMainMenu;
        public UIGameplay UIGameplay;
        public UIGameOver uIGameOver;
 


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CloseAll();
            DisplayMainMenu(true);
        }

        public void CloseAll()
        {
            DisplayMainMenu(false);
            DisplayGameplayMenu(false);
            DisplayGameoverMenu(false);
        }

        public void DisplayOtherMenu(bool isActive)
        {
            uiMainMenu.DisplayCanvas(isActive);
        }

        public void DisplayMainMenu(bool isActive)
        {
            uiMainMenu.DisplayCanvas(isActive);
        }

        public void DisplayGameplayMenu(bool isActive)
        {
            UIGameplay.DisplayCanvas(isActive);
        }

        public void DisplayGameoverMenu(bool isActive)
        {
            uIGameOver.DisplayCanvas(isActive);
        }
    }
}
