using Constants;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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
		EventManager.Instance.GetSavedLevelID += GetSavedLevelID;
		EventManager.Instance.SaveLevelID += SaveLevelID;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.GetSavedLevelID -= GetSavedLevelID;
		EventManager.Instance.SaveLevelID -= SaveLevelID;
	}

	#endregion

	private int GetSavedLevelID()
	{
		if (PlayerPrefs.HasKey(PlayerPrefsConstants.LEVEL))
		{
			return PlayerPrefs.GetInt(PlayerPrefsConstants.LEVEL);
		}

		return 1;
	}

	private void SaveLevelID() 
	{
		PlayerPrefs.SetInt(PlayerPrefsConstants.LEVEL, GetSavedLevelID() + 1);
	}
}

