using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float force = 5.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // Rigidbodyを取得
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Vector2 upForce = new Vector2(0.0f, force); // 力の方向と大きさ
            rigidbody.AddForce(upForce, ForceMode2D.Impulse);
        }
    }

	public void GameReset()
	{
		throw new NotImplementedException();
	}

	void OnCollisionEnter(Collision collision)
    {
        //ゲームオーバーになった時の処理を呼んでいます
        Debug.Log("gameover");
        //GameOver();
    }
}
