using System;
using UnityEngine;

public class ObstacleSetController : MonoBehaviour
{
	[SerializeField] float Speed;
	[SerializeField] bool isSimulating;

	[Header("Setting")]

	[SerializeField] GameObject UpperObstacle;
    [SerializeField] GameObject DownerObstacle;

	private Rigidbody2D RBUpper;
	private Rigidbody2D RBDowner;

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

	private void Start()
	{
		//出現直後に場所を調整
		transform.position = new(CameraEndPointByWorldPosition[1] * 1.1f, 0, 0);
		if (CameraEndPointByWorldPosition[0] > 0) Debug.Log("カメラ左端が0以上にあります");
	}

	// Update is called once per frame
	void Update()
    {
		if (!isSimulating) return; //プレイ中でなければ何もしない

		//スピード管理
		RBUpper.linearVelocity = Velocity;
		RBDowner.linearVelocity = Velocity;

		//消滅調整
		if (transform.position.x < CameraEndPointByWorldPosition[0] * 1.1f)
		{
			Destroyed.Invoke(this, gameObject);
			Destroy(gameObject);
		}
	}
}
