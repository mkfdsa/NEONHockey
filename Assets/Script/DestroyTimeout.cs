using UnityEngine;
using System.Collections;

// �����ŏ�����
public class DestroyTimeout : MonoBehaviour
{
    public float timer = 2.0f;
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Destroy(gameObject, timer);
        }
    }
}