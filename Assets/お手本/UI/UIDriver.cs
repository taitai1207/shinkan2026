using UnityEngine;

/// <summary>
/// UIのテスト用ドライバー
/// </summary>
public class UIDriver : MonoBehaviour
{
	[SerializeField] GameOverCanvasController GOCCon;
	bool done = false;
	private void Update()
	{
		if(!done && Time.frameCount > 200)
		{
			GOCCon.GameOverShow = true;
		}
	}
}
