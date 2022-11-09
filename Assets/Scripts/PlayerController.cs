using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private const string CloudTag = "Cloud";
	private const string CoinTag = "Coin";

	[SerializeField] private Vector2 constraints;
	[SerializeField] private float movementSpeed;

	private Rigidbody2D rb;
	private new Camera camera;

	private void Awake()
	{
		camera = Camera.main;
	}

	private void Update()
	{
		var position = transform.position;
		Vector2 mousePos = Vector2.zero;
		bool shouldMove = false;

#if UNITY_ANDROID || UNITY_IPHONE
		if (Input.touchCount > 0)
		{
			var touch = Input.GetTouch(0);
			mousePos = camera.ScreenToWorldPoint(touch.position);
			shouldMove = true;
		}

#else
		if (Input.GetMouseButton(0))
		{
			mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
			shouldMove = true;
		}
#endif
		if (shouldMove)
		{
			Vector3 targetPosition =  new Vector3(Mathf.Clamp(mousePos.x, constraints.x, constraints.y), position.y, position.z);
			transform.position = Vector3.Lerp(position, targetPosition, movementSpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(CloudTag))
			HandleCloud(other);
		else if (other.CompareTag(CoinTag))
			HandleCoin(other);
	}

	private void HandleCloud(Collider2D other)
	{
		GameController.Instance.AddFail();
	}

	private void HandleCoin(Collider2D other)
	{
		GameController.Instance.AddCoin();
		Destroy(other.gameObject);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(constraints.x, transform.position.y, 0), new Vector3(constraints.y, transform.position.y, 0));
	}
}