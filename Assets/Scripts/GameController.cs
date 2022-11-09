using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController Instance;
	[SerializeField] private int maxFails = 5;
	[Header("Obstacles"), SerializeField] private float timeBetweenObstacles = 5;
	[SerializeField] private Transform obstacleSpawnPoint;
	[SerializeField] private Transform obstacleDespawnPoint;
	[SerializeField] private ObstacleController[] obstaclesPrefabs;
	[SerializeField] private Transform obstacleHolder;
	private int currentCoins;
	private int currentFails;
	private Coroutine spawnObstaclesCO;

	public const string BestScoreSaveName = "BestScore";

	private void Awake()
	{
		Instance = this;
		spawnObstaclesCO = StartCoroutine(SpawnObstacles());
		UIController.Instance.UpdateCoins(currentCoins);
		UIController.Instance.UpdateFails(currentFails, maxFails);
	}

	#region ObstacleManagment

	private IEnumerator SpawnObstacles()
	{
		while (true)
		{
			SpawnObstacle();
			yield return new WaitForSeconds(timeBetweenObstacles);
		}
	}

	private void SpawnObstacle()
	{
		var obstacle = Instantiate(obstaclesPrefabs.Random(), obstacleSpawnPoint.position, Quaternion.identity, obstacleHolder);
		obstacle.RespawnHeight = obstacleDespawnPoint.position.y;
		ObstacleController.All.Add(obstacle);
	}

	#endregion

	#region Game Managment

	// Master function
	public void AddCoin()
	{
		currentCoins++;
		UIController.Instance.UpdateCoins(currentCoins);
	}

	// Master function
	public void AddFail()
	{
		currentFails++;
		UIController.Instance.UpdateFails(currentFails, maxFails);
		if (currentFails >= maxFails)
			LooseGame();
	}

	private void LooseGame()
	{
		StopCoroutine(spawnObstaclesCO);

		var bestScore = PlayerPrefs.GetInt(BestScoreSaveName, 0);
		UIController.Instance.LooseGame(currentCoins, bestScore);

		if (currentCoins > bestScore)
			PlayerPrefs.SetInt(BestScoreSaveName, currentCoins);

		PlayerPrefs.Save();

		for (int i = 0; i < ObstacleController.All.Count; ++i)
			ObstacleController.All[i]?.DestroyObstacle();

		ObstacleController.All.Clear();
	}

	public void RestartGame()
	{
		spawnObstaclesCO = StartCoroutine(SpawnObstacles());
		currentFails = 0;
		currentCoins = 0;
		UIController.Instance.UpdateCoins(currentCoins);
		UIController.Instance.UpdateFails(currentFails, maxFails);
	}

	#endregion
}