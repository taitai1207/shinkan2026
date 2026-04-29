using UnityEngine;


public class NewMonoBehaviourScript : MonoBehaviour
{
    private GameManager gameManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void GameReset()
    {
        this.transform.position = new Vector3(-5, 0, 0);
    }
}
