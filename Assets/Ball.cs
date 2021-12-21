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
        // ���݂̑��x��擾
        Vector2 velocity = myRigidBody.velocity;
        // ������v�Z
        float clampedSpeed = Mathf.Clamp(velocity.magnitude, minSpeed, maxSpeed);
        // ���x��ύX
        myRigidBody.velocity = velocity.normalized * clampedSpeed;


        // �O�ɏo��΁A�Đ���
        // ���W�擾
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
