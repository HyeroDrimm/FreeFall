using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	public static List<ObstacleController> All = new();
	[SerializeField] private float movementSpeed = 0.5f;
	private float respawnHeight;

	public float RespawnHeight { get => respawnHeight; set => respawnHeight = value; }


	private void Update()
	{
		if (transform.position.y >= respawnHeight)
		{
			DestroyObstacle();
		}

		//rb.MovePosition(transform.position + movementVector3);
		transform.position += Vector3.up * (movementSpeed * Time.deltaTime);
	}

	public void DestroyObstacle()
	{
		All.Remove(this);
		Destroy(gameObject);
	}
}