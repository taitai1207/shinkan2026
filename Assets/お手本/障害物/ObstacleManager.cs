using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] GameObject Obstacle;
    
    [Header("Generatrd")]
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
        }
	}

    void OnObstacleDeleted(object sender, GameObject controller)
    {
        GeneratedObjects.Remove(controller);
    }
}
