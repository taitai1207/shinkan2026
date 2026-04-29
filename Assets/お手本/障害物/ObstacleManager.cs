using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] GameObject Obstacle;

    [Header("Position")]
    [SerializeField] float GapCenterRange;
    [SerializeField] float GapSizeAtFirst;
    //[SerializeField] int GapSizeDecayHalfTime;
    
    [Header("Generated")]
    /// 今生きている障害物の一覧
    [SerializeField] List<GameObject> GeneratedObjects;

    int _GeneratedObstacleCount;
	/// <summary>
	/// 障害物の生成数をカウント
	/// 複雑なバランス調整をしたい人は活用してください
	/// </summary>
	public int GeneratedObstacleCount
    {
        private set => _GeneratedObstacleCount = value; get => _GeneratedObstacleCount;
	}

    /// <summary>
    /// 障害物生成
    /// </summary>
    public void Generate()
    {
		// 障害物を作る
		GameObject obj = GameObject.Instantiate(Obstacle);
        GeneratedObjects.Add(obj);

        // コントローラーの設定
        ObstacleSetController controller = obj.GetComponent<ObstacleSetController>();
        if (controller != null)
        {
            controller.manager = this;
            controller.Destroyed += OnObstacleDeleted;

            //位置調整
            float position = Random.Range(-GapCenterRange, GapCenterRange);
            float size = GapSizeAtFirst;
            controller.PostionSetUp(position, size);
		}
        GeneratedObstacleCount++;
	}

	/// <summary>
	/// 障害物が消えたときに呼ばれる
	/// 「今生きている障害物の一覧」から対象を削除
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="controller"></param>
	void OnObstacleDeleted(object sender, GameObject controller)
    {
        GeneratedObjects.Remove(controller);
    }

    /// <summary>
    /// ゲームを一時停止
    /// </summary>
	public void GameStop()
    {
        foreach(GameObject obj in GeneratedObjects)
        {
			ObstacleSetController controller = obj.GetComponent<ObstacleSetController>();
            if (controller != null)
            {
				controller.isSimulating = false;
            }
		}
    }

    /// <summary>
    /// 一時停止状態から再開
    /// </summary>
	public void GameRestart()
    {
		foreach (GameObject obj in GeneratedObjects)
		{
			ObstacleSetController controller = obj.GetComponent<ObstacleSetController>();
			if (controller != null)
			{
				controller.isSimulating = true;
			}
		}
	}

    /// <summary>
    /// ゲームをリセット
    /// 障害物を全部消す
    /// </summary>
    public void GameReset()
    {
        GeneratedObstacleCount = 0;
		foreach (GameObject obj in GeneratedObjects)
		{
			Destroy(obj);
		}
	}
}
