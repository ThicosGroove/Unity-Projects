using System;
using System.Collections;
using UnityEngine;
using GameEvents;

public enum CurrentLevelState
{
    LEVEL_1 = 1,
    LEVEL_2 = 2,
    LEVEL_3 = 3,
    LEVEL_4 = 4,
    LEVEL_5,
    LEVEL_6,
    LEVEL_7,
    LEVEL_8,
    LEVEL_9,
    LEVEL_10,
    LEVEL_11,
    LEVEL_12,
    LEVEL_MAX
}

[DefaultExecutionOrder(-1)]
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] LevelSO[] levelData;

    private CurrentLevelState currentLevelState = CurrentLevelState.LEVEL_1;

    public int currentLevel;

    public float lerpToNextLevel;

    [Header("Current Variables Apply")]
    public float current_obstacleSpeed;
    public float current_obstacleSpawnDelay;
    public float current_playerSlideSpeed;
    public float current_playerJumpSpeed;
    public float current_playerRollingSpeed;
    public float current_caminhaoMulti;

    [Header("Set Score per Level")]
    public int changeToLevel_2;
    public int changeToLevel_3;
    public int changeToLevel_4;
    public int changeToLevel_5;
    public int changeToLevel_6;
    public int changeToLevel_7;
    public int changeToLevel_8;
    public int changeToLevel_9;
    public int changeToLevel_10;
    public int changeToLevel_11;
    public int changeToLevel_12;
    public int changeToLevel_13;


    private float previousSpeed;

    private void OnEnable()
    {
        UtilityEvents.GamePause += StopMovement;
        UtilityEvents.GameResume += ResumeMovement;
    }

    private void OnDisable()
    {
        UtilityEvents.GamePause -= StopMovement;
        UtilityEvents.GameResume -= ResumeMovement;
    }

    private void Start()
    {
        if (GamePlayManager.Instance.isNormalMode == true)
        {
            current_obstacleSpeed = levelData[0].obstacle_Speed;
            current_obstacleSpawnDelay = levelData[0].obstacle_Spawn_Delay;
            current_playerSlideSpeed = levelData[0].player_Slide_Speed;
            current_playerJumpSpeed = levelData[0].player_Jump_Speed;
            current_playerRollingSpeed = levelData[0].player_Roll_Speed;
            current_caminhaoMulti = levelData[0].speedMulti;

        }
        else
        {
            current_obstacleSpeed = levelData[1].obstacle_Speed;
            current_obstacleSpawnDelay = levelData[1].obstacle_Spawn_Delay;
            current_playerSlideSpeed = levelData[1].player_Slide_Speed;
            current_playerJumpSpeed = levelData[1].player_Jump_Speed;
            current_playerRollingSpeed = levelData[1].player_Roll_Speed;
            current_caminhaoMulti = levelData[1].speedMulti;
        }
    }

    public void UpdateLevel(CurrentLevelState newLevel)
    {
        currentLevelState = newLevel;

        StartCoroutine(SettingUpCurrentLevel((int)currentLevelState));
    }

    private IEnumerator SettingUpCurrentLevel(int level)
    {
        currentLevel = level;
        Debug.LogWarning($"Mudou level {currentLevel}");

        ScoreEvents.OnChangeLevel(currentLevel);

        current_obstacleSpeed = levelData[currentLevel].obstacle_Speed;
        current_obstacleSpawnDelay = levelData[currentLevel].obstacle_Spawn_Delay;
        current_playerSlideSpeed = levelData[currentLevel].player_Slide_Speed;
        current_playerJumpSpeed = levelData[currentLevel].player_Jump_Speed;
        current_playerRollingSpeed = levelData[currentLevel].player_Roll_Speed;
        current_caminhaoMulti = levelData[currentLevel].speedMulti;
        yield return null;
    }

    private void StopMovement()
    {
        previousSpeed = current_obstacleSpeed;
        current_obstacleSpeed = 0;
    }

    private void ResumeMovement()
    {
        current_obstacleSpeed = previousSpeed;
    }

}
