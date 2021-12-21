using UnityEngine;
using System.Collections;

// é©ìÆÇ≈è¡Ç¶ÇÈ
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