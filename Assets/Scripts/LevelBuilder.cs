using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
	[SerializeField] private List<GameObject> levels;

	private void OnEnable()
	{
		BuildLevel();
	}

	private void BuildLevel() 
	{
		// Bütün level objelerini kapat
		foreach (GameObject level in levels)
		{
			level.SetActive(false);
		}

		// Güncel level hangisi ise onun karşılığı olan objeyi aktif hale getir
		levels[EventManager.Instance.GetSavedLevel() -1].SetActive(true);
	}
}
