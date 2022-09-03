using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDateRespawnPoint : MonoBehaviour
{
    [SerializeField, Tooltip("���X�|�[���|�C���g�X�V��̃e�N�X�`��")] Sprite updateSprite = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = updateSprite;
        }
    }
}
