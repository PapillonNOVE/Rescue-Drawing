using Constants;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	private int _level;
	public int Level
	{
		get => _level;

		set 
		{
			_level = value;

			PlayerPrefs.SetInt(PlayerPrefsConstants.LEVEL, _level);

			  EventManager.Instance.UpdateLevelText?.Invoke();
		}
	}

	private void OnEnable()
	{
		Subscribe();
	}

	private void OnDisable()
	{
		GeneralManager.QuittingControl(Unsubscribe);
	}

	#region Event Subscribe

	private void Subscribe()
	{
		EventManager.Instance.GetSavedLevel += GetSavedLevel;
		EventManager.Instance.SaveLevel += SaveLevel;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.GetSavedLevel -= GetSavedLevel;
		EventManager.Instance.SaveLevel -= SaveLevel;
	}

	#endregion


	private int GetSavedLevel()
	{
		if (PlayerPrefs.HasKey(PlayerPrefsConstants.LEVEL))
		{
			return PlayerPrefs.GetInt(PlayerPrefsConstants.LEVEL);
		}

		return 1;
	}

	private void SaveLevel() 
	{
		PlayerPrefs.SetInt(PlayerPrefsConstants.LEVEL, GetSavedLevel() + 1);

		EventManager.Instance.UpdateLevelText?.Invoke();
	}

	//private void LevelSaveControl()
	//{
	//	if (PlayerPrefs.HasKey(PlayerPrefsConstants.LEVEL))
	//	{
	//		Level = PlayerPrefs.GetInt(PlayerPrefsConstants.LEVEL);
	//	}
	//	else
	//	{
	//		Level = 1;
	//		PlayerPrefs.SetInt(PlayerPrefsConstants.LEVEL, Level);
	//	}
}

