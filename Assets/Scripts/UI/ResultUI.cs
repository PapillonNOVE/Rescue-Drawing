using Constants;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI resultText;
	[SerializeField] private Image resultImage;
	[SerializeField] private Button nextButton;
	[SerializeField] private Button retryButton;

	[Header("Result Panel Sprite")]
	[SerializeField] private Sprite successfulSprite;
	[SerializeField] private Sprite failedSprite;

	private float resultImageFirstPosY;

	private void Start()
	{
		OnClickAddListener();
	}

	private void OnEnable()
	{
		resultImageFirstPosY = resultImage.rectTransform.anchoredPosition.y;

		Subscribe();
	}

	private void OnDisable()
	{
		GeneralManager.QuittingControl(Unsubscribe);
	}

	#region Event Subscribe

	private void Subscribe()
	{
		EventManager.Instance.LevelCompleted += SetSuccessfulResultImage;
		EventManager.Instance.GameOver += SetFailedResultImage;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.LevelCompleted -= SetSuccessfulResultImage;
		EventManager.Instance.GameOver -= SetFailedResultImage;
	}

	#endregion

	private void OnClickAddListener()
	{
		//TODO: Sonraki Level´a geçmeyi sağlayacak Eventi çağır
		//nextButton.onClick.AddListener();

		retryButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
	}

	private void SetSuccessfulResultImage()
	{
		resultImage.sprite = successfulSprite;
		resultText.SetText(ResultMessageConstants.COMPLETED);
		nextButton.gameObject.SetActive(true);

		StartCoroutine(ShowResults());
	}

	private void SetFailedResultImage()
	{
		resultImage.sprite = failedSprite;
		resultText.SetText(ResultMessageConstants.FAILED);
		retryButton.gameObject.SetActive(true);

		StartCoroutine(ShowResults());
	}

	private IEnumerator ShowResults() 
	{
		Tween tween = resultImage.rectTransform.DOAnchorPos3DY(0, 1f);

		yield return tween.WaitForCompletion();
	}

	private void HideResults() 
	{
		resultImage.rectTransform.DOAnchorPos3DY(resultImageFirstPosY, 1f);
		nextButton.gameObject.SetActive(false);
		retryButton.gameObject.SetActive(false);
	}
}
