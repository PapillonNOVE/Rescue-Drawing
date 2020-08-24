using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
	IntroScene,
	MainScene
}

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy dontDestroy;

	[SerializeField] private Scene scene;

	void Awake()
	{
		if (dontDestroy == null)
		{
			dontDestroy = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		SceneManager.LoadScene((int)scene);
	}
}
