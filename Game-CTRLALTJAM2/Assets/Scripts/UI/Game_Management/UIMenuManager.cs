using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using UI.Audio;

namespace UI.GameManagement
{
    public class UIMenuManager : MonoBehaviour
    {
        private GameObject _menuButtons;

        public void ClickStartButton()
        {
            _menuButtons = GameObject.FindGameObjectWithTag("PlayGameButton");
            GameObject.FindAnyObjectByType<AudioManager>().PlayOneShot(FMODEvents.instance.menuPlay, _menuButtons.transform.position);
        
            SceneManager.LoadScene(Constants.GAME_SCENE_1);
        }

        public void ClickOptionsButton()
        {
            _menuButtons = GameObject.FindGameObjectWithTag("OptionsButton");
            GameObject.FindAnyObjectByType<AudioManager>().PlayOneShot(FMODEvents.instance.menuConfirm, _menuButtons.transform.position);
            SceneManager.LoadScene(Constants.OPTIONS_SCENE);
        }

        public void ClickCreditButton()
        {
            _menuButtons = GameObject.FindGameObjectWithTag("CreditsButton");
            GameObject.FindAnyObjectByType<AudioManager>().PlayOneShot(FMODEvents.instance.menuConfirm, _menuButtons.transform.position);
            SceneManager.LoadScene(Constants.CREDIT_SCENE);
        }

        public void ClickQuitButton()
        {
            _menuButtons = GameObject.FindGameObjectWithTag("QuitButton");
            GameObject.FindAnyObjectByType<AudioManager>().PlayOneShot(FMODEvents.instance.menuConfirm, _menuButtons.transform.position);

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
                Application.Quit();
        }

        public void ClickBackButton()
        {
            _menuButtons = GameObject.FindGameObjectWithTag("BackButton");
            GameObject.FindAnyObjectByType<AudioManager>().PlayOneShot(FMODEvents.instance.menuConfirm, _menuButtons.transform.position);
            SceneManager.LoadScene(Constants.MENU_START_SCENE);
        }
    }
}