using UnityEngine;

/// <summary>�x���g�R���x�A�̃A�j���[�V�������x�����߂� </summary>
[RequireComponent(typeof(Animator))]
public class SetBeltConveyorAnimationSpeed : MonoBehaviour
{
    /// <summary>�A�j���[�V�����X�s�[�h </summary>
    float _animationSpeed = 1f;

    public float AnimationSpeed
    {
        get { return _animationSpeed; }
        set
        {
            if (0 < value)
            {
                _animationSpeed = value;
            }
        }
    }

    Animator _anim => GetComponent<Animator>();

    private void Update()
    {
        _anim.speed = _animationSpeed;  //�f�o�b�O�p
    }

}
