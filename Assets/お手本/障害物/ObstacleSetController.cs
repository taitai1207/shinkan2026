using System;
using UnityEngine;

public class ObstacleSetController : MonoBehaviour
{
	[Header("Obstacle Property")]
	[SerializeField] float GapPosition;
	[SerializeField] float GapSize;
	[SerializeField] float Speed;

	[Header("Game Property")]
	public bool isSimulating;

	[Header("Setting")]

	[SerializeField] GameObject UpperObstacle;
    [SerializeField] GameObject DownerObstacle;

	[SerializeField] Rigidbody2D RB;

	[HideInInspector] public ObstacleManager manager;

	public event EventHandler<GameObject> Destroyed;


	/// <summary>
	/// カメラの両端
	/// </summary>
	/// <remarks>
	/// index : 0 => 左端, 1 => 右端
	/// </remarks>
	public static float[] CameraEndPointByWorldPosition => new float[] { Camera.main.ViewportToWorldPoint(new(0, 0, 0)).x, Camera.main.ViewportToWorldPoint(new(1, 0, 0)).x };
	/// <summary> 速度 </summary>
	Vector2 Velocity => new(Speed, 0);

	private void Awake()
	{

	}

	public void PostionSetUp()
	{
		//出現直後に場所を調整
		//x座標
		transform.position = new(CameraEndPointByWorldPosition[1] * 1.1f, 0, 0);
		if (CameraEndPointByWorldPosition[0] > 0) Debug.Log("カメラ左端が0以上にあります"); // 不要だと思うけど一応

		//各障害物のy座標調整
		float upperSize = UpperObstacle.transform.localScale.y, downerSize = DownerObstacle.transform.localScale.y;
		UpperObstacle.transform.localPosition = new(0, GapPosition + GapSize / 2 + upperSize / 2);
		DownerObstacle.transform.localPosition = new(0, GapPosition - GapSize / 2 - downerSize / 2);
	}

	// Update is called once per frame
	void Update()
    {
		if (!isSimulating)
		{
			RB.linearVelocity = Vector2.zero; //プレイ中でなければ止める
		}
		else
		{
			//スピード管理
			RB.linearVelocity = Velocity;

			//消滅調整
			if (transform.position.x < CameraEndPointByWorldPosition[0] * 1.1f)
			{
				Destroy(gameObject);
			}
		}
	}

	private void OnDestroy()
	{
		Destroyed?.Invoke(this, gameObject);
	}
}
