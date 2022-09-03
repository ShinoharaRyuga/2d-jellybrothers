using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDateRespawnPoint : MonoBehaviour
{
    [SerializeField, Tooltip("リスポーンポイント更新後のテクスチャ")] Sprite updateSprite = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = updateSprite;
        }
    }
}
