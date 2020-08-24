using System;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
	// Game
	public UnityAction SetFreeBalls;
    public UnityAction DestroyBalls;

    // Level
    public Func<int> GetSavedLevel;
    public UnityAction SaveLevel;

    // UI
    public UnityAction<float> SetDrawLimitBarMaxValue;
    public UnityAction<float> UpdateDrawLimitBar;
    public UnityAction UpdateLevelText;

    // Result
    public UnityAction GameOver;
    public UnityAction PlayAgain;
    public UnityAction LevelCompleted;
}
