using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ObstacleManager obstacleManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstacleManager  = GetComponent<ObstacleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CreateObject());
    }

    IEnumerator CreateObject()
    {
        yield return new WaitForSeconds(1.0f);
        obstacleManager.Generate();
    }
}
