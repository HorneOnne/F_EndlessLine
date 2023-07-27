using UnityEngine;
using UnityEngine.UI;

namespace EndlessLine
{
    public class UIMainMenu : EndlessLineCanvas
    {
        [SerializeField] private Animator tapToStartAnim;

        private void Start()
        {
         

        }
        public override void DisplayCanvas(bool isDisplay)
        {
            base.DisplayCanvas(isDisplay);
            tapToStartAnim.enabled = isDisplay;
        }

    }
}
