using UnityEngine;
using UnityEngine.Events;

// Namespace que centraliza todos os eventos do jogo.
namespace UI.GameManagement
{
    //public static class PlayerEvents
    //{
    //    public static event UnityAction PlayerHit;
    //    public static void OnPlayerHit() => PlayerHit?.Invoke();

    //    public static event UnityAction PlayerDeath;
    //    public static void OnPlayerDeath() => PlayerDeath?.Invoke();
    //}

    public static class ScoreEvents
    {
        public static event UnityAction<int> OnScoreGained; // Manda
        public static void ScoreGained(int value) => OnScoreGained?.Invoke(value); // Recebe

        //public static event UnityAction<int> ChangeLevel;
        //public static void OnChangeLevel(int value) => ChangeLevel?.Invoke(value);
    }

    public static class GameplayEvents
    {
        public static event UnityAction<GameObject> OnEnemyDeath;
        public static void EnemyDeath(GameObject enemy) => OnEnemyDeath?.Invoke(enemy);

        public static event UnityAction OnGameOver;
        public static void GameOver() => OnGameOver?.Invoke();

        public static event UnityAction OnWinGame;
        public static void WinGame() => OnWinGame?.Invoke();

        public static event UnityAction<float> OnDash;
        public static void Dash(float dashTimer) => OnDash?.Invoke(dashTimer);

        public static event UnityAction<float, int> OnPowerUp;
        public static void PowerUp(float PowerUpTimer, int type) => OnPowerUp?.Invoke(PowerUpTimer, type);
        
        public static event UnityAction<WaveState> OnNextWave;
        public static void NextWave(WaveState wave) => OnNextWave?.Invoke(wave);

        public static event UnityAction OnBoss;
        public static void BossTime() => OnBoss?.Invoke();
    }

    public static class UtilityEvents
    {
        //    public static event UnityAction SaveGame;
        //    public static void OnSaveGame() => SaveGame?.Invoke();

        public static event UnityAction OnGamePause;
        public static void GamePause() => OnGamePause?.Invoke();

        public static event UnityAction OnGameResume;
        public static void GameResume() => OnGameResume?.Invoke();

        //    public static event UnityAction<bool> SFXToggle;
        //    public static void OnMuteSFXToggle(bool isMute) => SFXToggle?.Invoke(isMute);

        //    public static event UnityAction<bool> BGMusicToggle;
        //    public static void OnBGMusicToggle(bool isMute) => BGMusicToggle?.Invoke(isMute);
    }
}