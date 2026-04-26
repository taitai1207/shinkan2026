using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] GameObject Obstacle;
    
    [Header("Generated")]
    [SerializeField] List<GameObject> GeneratedObjects;

    public void Generate()
    {
		GameObject obj = GameObject.Instantiate(Obstacle);
        GeneratedObjects.Add(obj);

        ObstacleSetController controller = obj.GetComponent<ObstacleSetController>();
        if (controller != null)
        {
            controller.manager = this;
            controller.Destroyed += OnObstacleDeleted;
            controller.PostionSetUp();
		}
	}

    void OnObstacleDeleted(object sender, GameObject controller)
    {
        GeneratedObjects.Remove(controller);
    }

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

    public void GameReset()
    {
		foreach (GameObject obj in GeneratedObjects)
		{
			Destroy(obj);
		}
	}
}
