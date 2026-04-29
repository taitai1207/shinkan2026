using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class GameManager : MonoBehaviour
{
    [Header("Obstacle")]
    [SerializeField] ObstacleManager ObstacleManager;
    [SerializeField] float ObstacleInterval;

    [Header("UI")]
    [SerializeField] GameOverCanvasController GOCController;

    CancellationTokenSource CTS;

    private bool isgameOver;

	private void Start()
	{
        GameStart();
		isgameOver = false;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{}
	}
	
	#region GameManagement
    /// <summary>
    /// ゲーム開始時に呼ばれるメソッド
    /// </summary>
	void GameStart()
    {
        StartCreateObstacle();
	}

    /// <summary>
    /// GameOver時に呼ばれるメソッド
    /// </summary>
    public void GameOver(Bird bird)
    {
        this.bird = bird;
        StopCreateObstacle();
        GOCController.GameOverShow = true;
    }

    Bird bird;
	/// <summary>
	/// ゲームリセット時に呼ばれるメソッド
	/// </summary>
	public void GameReset()
    {
        //リセット処理をする
        GOCController.GameOverShow = false;
        isgameOver = false;
        ObstacleManager.GameReset();
        bird.GameReset();

		GameStart();
    }
	#endregion


	#region Obstacle
    /// <summary>
    /// 障害物の生成を開始
    /// </summary>
    void StartCreateObstacle()
    {
		CTS = new CancellationTokenSource();
		GenerateObstaclesPermanentlyAsync(CTS.Token).Forget();
        ObstacleManager.GameRestart();
	}

    /// <summary>
    /// 障害物の生成を中止
    /// </summary>
    void StopCreateObstacle()
    {
        CTS.Cancel();
        CTS = null;
        ObstacleManager.GameStop();
    }

	/// <summary>
	/// 障害物を1つ作る
	/// </summary>
	async UniTask CreateObstacleAsync(CancellationToken token)
    {
        ObstacleManager.Generate();
        await UniTask.WaitForSeconds(ObstacleInterval);
    }

    /// <summary>
    /// 障害物を作り続ける
    /// </summary>
    async UniTask GenerateObstaclesPermanentlyAsync(CancellationToken token)
    {
        try
        {
            while (true) await CreateObstacleAsync(token);
        }
        catch (OperationCanceledException)
        {
            // CancellationTokenSourceのは握り潰してOK
        }
        catch
        {
            throw;
        }
    }

	#endregion
}
