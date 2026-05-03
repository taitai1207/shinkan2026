using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private GameManager gameManager;
    private float force = 5.0f;
    private float cooldownExpiry = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // Rigidbodyを取得
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameManager.IsGameOver && Time.time > cooldownExpiry)
        {
            Vector2 upForce = new Vector2(0.0f, force); // 力の方向と大きさ

            rigidbody.linearVelocity = new Vector2(0.0f, 0.0f);
            rigidbody.AddForce(upForce, ForceMode2D.Impulse);
            cooldownExpiry = Time.time + 0.2f;
        }
    }

	public void GameReset()
	{
		this.transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
        rigidbody.linearVelocity = new Vector2(0.0f, 0.0f);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //ゲームオーバー済みなら呼ばない
        if (gameManager.IsGameOver) return;

        Debug.Log("Game Over!");
        //ゲームオーバーになった時の処理を呼んでいます
        gameManager.GameOver(this);
    }
}
