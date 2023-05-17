using UnityEngine;

namespace UI.GameManagement
{
    public enum WaveState
    {
        WAVE_1 = 1, WAVE_2, WAVE_3, WAVE_4, BOSS
    }

    public class WaveManager : MonoBehaviour
    {
        public WaveState _wave;

        private void Start()
        {
            _wave = WaveState.WAVE_1;
            CallNextWave();
        }


        public void NextWave()
        {
            _wave++;

            CheckCurrentWave();
        }

        private void CheckCurrentWave()
        {
            if (_wave != WaveState.BOSS)
            {
                CallNextWave();
                return;
            }

            CallBoss();
        }

        private void CallNextWave()
        {
            GameplayEvents.NextWave(_wave);
        }

        private void CallBoss()
        {
            GameplayEvents.BossTime();
        }
    }

}

