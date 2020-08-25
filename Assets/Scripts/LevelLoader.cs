using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	[SerializeField] private List<GameObject> levels;


	private void OnEnable()
	{
		LoadLevel();

		Subscribe();
	}

	private void OnDisable()
	{
		GeneralManager.QuittingControl(Unsubscribe);
	}

	#region Event Subscribe

	private void Subscribe()
	{
		EventManager.Instance.LoadLevel += LoadLevel;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.LoadLevel -= LoadLevel;
	}

	#endregion

	private void LoadLevel() 
	{
		int currentLevelID = EventManager.Instance.GetSavedLevelID();

		if (currentLevelID > levels.Count)
		{
			currentLevelID = levels.Count;
		}

		Debug.Log(currentLevelID);

		Instantiate(levels[currentLevelID - 1], transform);
	}
}
