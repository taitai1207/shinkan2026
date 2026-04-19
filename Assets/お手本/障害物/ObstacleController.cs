using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] float Speed;
	[SerializeField] bool isSimulating;

	[Header("Setting")]
	[SerializeField] Rigidbody2D RB;

	Vector2 Velocity => new(Speed, 0);

	private void Update()
	{
		if (!isSimulating) return; //ƒvƒŒƒC’†‚Å‚È‚¯‚ê‚Î‰½‚à‚µ‚È‚¢

		//“®ìŠÇ—
		rigidbody.linearVelocity = Velocity;
	}
}
