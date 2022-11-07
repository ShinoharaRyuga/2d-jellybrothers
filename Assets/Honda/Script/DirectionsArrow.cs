using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionsArrow : MonoBehaviour
{

    [SerializeField] GameObject[] _arrows =  new GameObject[2];//�\�����鏇�ԂɃI�u�W�F�N�g������
    [SerializeField]Player _player = default;

    private void Start()
    {
        for(int i = 1; i < _arrows.Length; i++)
        {
            _arrows[i].SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _player.ToString())
        {
            _arrows[0].SetActive(false);
            _arrows[1].SetActive(true);
            this.enabled = false;
        }
    }


    public enum Player
    { 
        Player1,
        Player2
    }

}
