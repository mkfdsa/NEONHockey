using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    public float speedX = 10;
    public float speedY = 10;

    public float minSpeed = 10;
    public float maxSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = this.gameObject.GetComponent<Rigidbody2D>();

        Vector2 force = new Vector2(minSpeed, minSpeed);

        myRigidBody.AddForce(force);
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の速度を取得
        Vector2 velocity = myRigidBody.velocity;
        // 速さを計算
        float clampedSpeed = Mathf.Clamp(velocity.magnitude, minSpeed, maxSpeed);
        // 速度を変更
        myRigidBody.velocity = velocity.normalized * clampedSpeed;


        // 外に出れば、再生成
        // 座標取得
        if (transform.position.x > 5)
        {
            transform.position = new Vector3(0, 0, 0);
        }
        else if (transform.position.x < -5)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
