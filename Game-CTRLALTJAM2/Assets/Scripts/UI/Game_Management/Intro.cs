using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utilities;

namespace UI.GameManagement
{
    public class Intro : MonoBehaviour
    {
        public Image introPanel;
        public Image logo;
        private Color _introColorLogo, _introColorPanel;
        public float _transparencyIndexLogo, _transparencyIndexPanel;

        private bool fade = false;
        public float _fadeOutCount = 0;

        private void Awake()
        {
            IntroSetup();
        }

        private void Update()
        {
            if(fade == false)
                FadeIn();
            else
                FadeOut();
        }

        private void IntroSetup()
        {
            _transparencyIndexLogo = 0;
            _transparencyIndexPanel = 1f;
            _introColorPanel = Color.black;
            _introColorLogo = logo.color;
            _introColorPanel.a = _transparencyIndexPanel;
            _introColorLogo.a = _transparencyIndexLogo;
            introPanel.color = _introColorPanel;
            logo.color = _introColorLogo;
        }

        private void FadeIn()
        {
            if (_transparencyIndexPanel > 0.45f)
            {
                _transparencyIndexPanel -= 0.1f * Time.deltaTime;
                var indexLogo = (_transparencyIndexPanel > 0.6f) ? _transparencyIndexLogo += 0.1f * Time.deltaTime : _transparencyIndexLogo += 0.3f * Time.deltaTime;
                _introColorPanel.a = _transparencyIndexPanel;
                _introColorLogo.a = _transparencyIndexLogo;
                introPanel.color = _introColorPanel;
                logo.color = _introColorLogo;
            }
            else
                fade = true;
        }

        private void FadeOut()
        {
            if (_transparencyIndexPanel < 1f)
            {
                _transparencyIndexPanel += 0.15f * Time.deltaTime;
                var indexLogo = (_transparencyIndexPanel < 0.6f) ? _transparencyIndexLogo -= 0.1f * Time.deltaTime : _transparencyIndexLogo -= 0.3f * Time.deltaTime;
                _introColorPanel.a = _transparencyIndexPanel;
                _introColorLogo.a = _transparencyIndexLogo;
                introPanel.color = _introColorPanel;
                logo.color = _introColorLogo;
            }
            else
                SceneManager.LoadScene(Constants.MENU_START_SCENE);
        }
    }
}