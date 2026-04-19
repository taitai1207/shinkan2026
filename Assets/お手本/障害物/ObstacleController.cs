using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] float Speed;
	[SerializeField] bool isSimulating;

	[Header("Setting")]
	[SerializeField] Rigidbody2D RB;

	/// <summary>
	/// ã‚«ãƒ¡ãƒ©ã®ä¸¡ç«¯
	/// </summary>
	/// <remarks>
	/// index : 0 => å·¦ç«¯, 1 => å³ç«¯
	/// </remarks>
	public static float[] CameraEndPointByWorldPosition => new float[] { Camera.main.ViewportToWorldPoint(new(0, 0, 0)).x, Camera.main.ViewportToWorldPoint(new(1, 0, 0)).x };
	/// <summary> é€Ÿåº¦ </summary>
	Vector2 Velocity => new(Speed, 0);

	private void Update()
	{
		if (!isSimulating) return; //ƒvƒŒƒC’†‚Å‚È‚¯‚ê‚Î‰½‚à‚µ‚È‚¢

		//“®ìŠÇ—
		rigidbody.linearVelocity = Velocity;
	}
}
