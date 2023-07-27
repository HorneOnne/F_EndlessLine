using UnityEngine;
using UnityEngine.UI;

namespace EndlessLine
{
    public class UIOther : EndlessLineCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button soundFXBtn;

        [Header("Image Object")]
        [SerializeField] private GameObject unmuteObject;
        [SerializeField] private GameObject muteObject;

        private void Start()
        {
            UpdateSoundFXUI();

            soundFXBtn.onClick.AddListener(() =>
            {
                ToggleSFX();
            });
        }

        private void OnDestroy()
        {
            soundFXBtn.onClick.RemoveAllListeners();
        }

        private void ToggleSFX(bool updateUI = true)
        {
            SoundManager.Instance.MuteSoundFX(SoundManager.Instance.isSoundFXActive);
            SoundManager.Instance.isSoundFXActive = !SoundManager.Instance.isSoundFXActive;

            if (updateUI)
                UpdateSoundFXUI();
        }

        private void UpdateSoundFXUI()
        {
            if (SoundManager.Instance.isSoundFXActive)
            {
                unmuteObject.SetActive(true);
                muteObject.SetActive(false);
            }
            else
            {
                unmuteObject.SetActive(false);
                muteObject.SetActive(true);
            }
        }
    }
}
