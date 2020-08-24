using Constants;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private Slider drawLimitBar;
	[SerializeField] private Button retryButton;

	private void Start()
	{
		OnClickAddListener();
	}

	private void OnEnable()
	{
		UpdateLevelText();
		Subscribe();
	}

	private void OnDisable()
	{
		GeneralManager.QuittingControl(Unsubscribe);
	}

	#region Event Subscribe

	private void Subscribe()
	{
		EventManager.Instance.SetDrawLimitBarMaxValue += SetDrawLimitBarMaxValue;
		EventManager.Instance.UpdateDrawLimitBar += UpdateDrawLimitBar;
		EventManager.Instance.UpdateLevelText += UpdateLevelText;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.SetDrawLimitBarMaxValue -= SetDrawLimitBarMaxValue;
		EventManager.Instance.UpdateDrawLimitBar -= UpdateDrawLimitBar;
		EventManager.Instance.UpdateLevelText -= UpdateLevelText;
	}

	#endregion

	private void OnClickAddListener() 
	{
		retryButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
	}

	private void UpdateLevelText() 
	{
		levelText.SetText(EventManager.Instance.GetSavedLevel().ToString());
	}

	private void SetDrawLimitBarMaxValue(float currentMaxValue) 
	{
		drawLimitBar.maxValue = currentMaxValue;
	}

	private void UpdateDrawLimitBar(float currentValue) 
	{
		drawLimitBar.value = currentValue;
	}
}
