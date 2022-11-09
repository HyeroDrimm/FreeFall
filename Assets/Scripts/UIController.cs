using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
	public static UIController Instance;
	[SerializeField] private TMP_Text failsNumberText;
	[SerializeField] private TMP_Text coinsNumberText;

	[Header("Lost Game"), SerializeField] private GameObject lostCanvas;
	[SerializeField] private TMP_Text currentScoreText;
	[SerializeField] private TMP_Text bestScoreText;

	private void Awake()
	{
		Instance = this;
	}

	public void UpdateFails(int currentFails, int maxFails)
	{
		failsNumberText.text = $"{currentFails}/{maxFails}";
	}

	public void UpdateCoins(int currentCoins)
	{
		coinsNumberText.text = $"{currentCoins}";
	}

	public void LooseGame(int currentScore, int bestScore)
	{
		currentScoreText.text = $"{currentScore}";
		bestScoreText.text = $"{bestScore}";

		lostCanvas.SetActive(true);
	}

	public void OnRestartPressed()
	{
		lostCanvas.SetActive(false);
		GameController.Instance.RestartGame();
	}
}