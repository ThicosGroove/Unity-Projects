using UnityEngine;
using UnityEngine.UI;

namespace UI.Audio
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        public void MusicVolumeChanged()
        {
            AudioManager.musicVolume = musicSlider.value;
        }

        public void SFXVolumeChanged()
        {
            AudioManager.sfxVolume = sfxSlider.value;
        }
    }
}