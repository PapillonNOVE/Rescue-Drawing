using System;
using UnityEngine;
using UnityEngine.Events;

public class GeneralManager : MonoBehaviour
{
	public static float deathTimerLimit = 4;

	private static bool isQuitting;

	public static bool isGameStarted = true;

	private static bool _isGameOver;
	public static bool IsGameOver
	{
		get => _isGameOver;

		set
		{
			_isGameOver = value;

			if (_isGameOver)
			{
				EventManager.Instance.DestroyBalls();
			}
		}
	}

	//public static void EventNullControl(UnityAction unityAction) 
	//{
	//	if (EventManager.Instance.uni)
	//	{

	//	}
	//}

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
