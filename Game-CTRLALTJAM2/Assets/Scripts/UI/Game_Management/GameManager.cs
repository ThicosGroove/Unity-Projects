using UnityEngine;
using UI.Audio;

public enum GameState
{
    GAMEPLAY,
    PAUSE,
    GAMEOVER,
    WIN
}

namespace UI.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState _state;

        // Singleton Pattern
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            _state = GameState.GAMEPLAY;
        }

        private void OnEnable()
        {
            UtilityEvents.OnGamePause += GamePaused;
            UtilityEvents.OnGameResume += GameResumed;
            GameplayEvents.OnGameOver += GameOver;
            GameplayEvents.OnWinGame += GameEnd;
        }

        private void OnDisable()
        {
            UtilityEvents.OnGamePause -= GamePaused;            
            UtilityEvents.OnGameResume -= GameResumed;
            GameplayEvents.OnGameOver -= GameOver;
            GameplayEvents.OnWinGame += GameEnd;
        }

        public void ChangeState(GameState state)
        {
            _state = state;
        }

        private void GamePaused()
        {
            ChangeState(GameState.PAUSE);

            Time.timeScale = 0;
        }

        private void GameResumed()
        {
            ChangeState(GameState.GAMEPLAY);

            Time.timeScale = 1;
        }

        private void GameOver()
        {
            GameObject.FindObjectOfType<AudioManager>().CleanUp();
            GameObject.FindObjectOfType<AudioManager>().PlayOneShot(FMODEvents.instance.defeatEnd, Vector3.zero);
            ChangeState(GameState.GAMEOVER);
        }

        private void GameEnd()
        {
            ChangeState(GameState.WIN);
        }
    }
}

