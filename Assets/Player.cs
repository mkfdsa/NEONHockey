using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �ǉ�
    private Vector3 screenPoint;
    private Vector3 offset;

    public float positionY = 20;
    public float positionRow = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    public float speed;

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        transform.position = new Vector2(
            //�G���A�w�肵�Ĉړ�����
            Mathf.Clamp(transform.position.x + moveX, -1.8f, 1.8f), positionRow
        );
    }

    // �ǉ�
    void OnMouseDown()
    {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, positionY, screenPoint.z));
    }
    // �ǉ�
    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, positionY, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
    }
}