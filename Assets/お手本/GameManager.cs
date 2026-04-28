using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class GameManager : MonoBehaviour
{
    [SerializeField] ObstacleManager ObstacleManager;

    [Header("Obstacle")]
    [SerializeField] float ObstacleInterval;

    CancellationTokenSource CTS;

	private void Start()
	{
        GameStart();
	}

	#region GameManagement
    /// <summary>
    /// ゲーム開始時に呼ばれるメソッド
    /// </summary>
	void GameStart()
    {
		CTS = new();
		GenerateObstaclesPermanentlyAsync(CTS.Token).Forget();
	}

    /// <summary>
    /// GameOver時に呼ばれるメソッド
    /// </summary>
    public void GameOver(Bird bird)
    {
        this.bird = bird;
        // UIをゲームオーバー仕様にする
        //
    }

    Bird bird;
	/// <summary>
	/// ゲームリセット時に呼ばれるメソッド
	/// </summary>
	public void GameReset()
    {
        //リセット処理をする
        GameStart();
    }
	#endregion


	#region Obstacle
	async UniTask CreateObstacleAsync(CancellationToken token)
    {
        ObstacleManager.Generate();
        await UniTask.WaitForSeconds(ObstacleInterval);
    }

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
