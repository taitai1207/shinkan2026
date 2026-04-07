using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbodyを取得
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            Vector3 force = new Vector3(0.0f, 0.0f, 10.0f); // 力の方向と大きさ
            rb.AddForce(force);
        }
        
    }
}
