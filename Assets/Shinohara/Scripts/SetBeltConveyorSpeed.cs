using UnityEngine;

/// <summary>�x���g�R���x�A�̈ړ����x�����߂� </summary>
public class SetBeltConveyorSpeed : MonoBehaviour
{
   float _speed = 1f;

    public float Speed 
    {
        get { return _speed; }
        set 
        {
            if (0 < value)
            {
                _speed = value;
            }
        }
    }

    Animator _anim => GetComponent<Animator>();

    private void Update()
    {
        _anim.speed = _speed;
    }
}
