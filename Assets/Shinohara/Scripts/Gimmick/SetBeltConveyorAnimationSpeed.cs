using UnityEngine;

/// <summary>ベルトコンベアのアニメーション速度を決める </summary>
[RequireComponent(typeof(Animator))]
public class SetBeltConveyorAnimationSpeed : MonoBehaviour
{
    /// <summary>アニメーションスピード </summary>
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
        _anim.speed = _animationSpeed;  //デバッグ用
    }

}
