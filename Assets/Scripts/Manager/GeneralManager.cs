using System;
using UnityEngine;

public enum GameStates 
{
   Prepare,
   GameStarted,
   DeathTimer,
   LevelCompleted,
   GameOver,
}

public class GeneralManager : MonoBehaviour
{
	public static float deathTimerLimit = 4;

	private static bool isQuitting;

	private static GameStates _gameState;
	public static GameStates GameState
	{
		get => _gameState;
	
		set
		{
			_gameState = value;

			if (_gameState == GameStates.LevelCompleted)
			{
				EventManager.Instance.LevelCompleted();
			}
			else if (_gameState == GameStates.GameOver) 
			{
				EventManager.Instance.GameOver();
			}
		}
	}

	private void OnEnable()
	{
		ResetGameState();
	}

	public static void ResetGameState() 
	{
		GameState = GameStates.Prepare;
	}

	public static void QuittingControl(Action targetMethod) 
	{
		if (isQuitting)
		{
			return ;
		}

		targetMethod();
	}

	private void OnApplicationQuit()
	{
		isQuitting = true;
	}
}
