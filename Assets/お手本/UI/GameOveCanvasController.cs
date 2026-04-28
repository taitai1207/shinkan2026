using UnityEngine;

public class GameOveCanvasController : MonoBehaviour
{
	[SerializeField] Canvas Canvas;
	public bool GameOverShow
	{
		set
		{
			Canvas.enabled = value;
		}
	}

	private void Start()
	{
		Canvas.worldCamera = Camera.main;
	}
}
