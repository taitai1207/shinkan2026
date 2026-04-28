using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class GameManager : MonoBehaviour
{
    [SerializeField] ObstacleManager ObstacleManager;

    [Header("障害物")]
    [SerializeField] float ObstacleInterval;

    CancellationTokenSource CTS;

	private void Start()
	{
        GenerateObstaclesPermanentlyAsync(CTS.Token).Forget();
	}

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
