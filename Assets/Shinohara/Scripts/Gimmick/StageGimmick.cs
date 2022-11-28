using Photon.Pun;
using UnityEngine;


/// <summary>���E���E����M�~�b�N�̕\���E��\����ύX���� </summary>
[RequireComponent(typeof(BoxCollider2D), typeof(PhotonView), typeof(SpriteRenderer))]
public class StageGimmick : MonoBehaviour
{
    ///�@<summary>
    ///�@<para>�Q�[���J�n���̃I�u�W�F�N�g���</para>
    ///�@<para>�Q�[���J�n���̃I�u�W�F�N�g��Ԃ�ݒ肷��̂͏�Ԃɂ���ă{�^���̏�����ς����</para>
    ///�@�{�^���������ꂽ��...
    ///�@<para>true = �M�~�b�N�I�u�W�F�N�g���\���ɂ��� ��@�h�A���J����</para>
    ///�@<para>false = �M�~�b�N�I�u�W�F�N�g��\���ɂ���@��@������o��������</para>
    /// </summary>
    [SerializeField, Header("�Q�[���J�n���̃I�u�W�F�N�g���")] bool _isStartActive = true;
    [SerializeField, Header("�I�u�W�F�N�g�̐F")] ObjectColor _objectColor = ObjectColor.Red;
    /// <summary>���݂̃I�u�W�F�N�g��� </summary>
    bool _isCurrentActive = true;
    PhotonView _view => GetComponent<PhotonView>();
    BoxCollider2D _bc2D => GetComponent<BoxCollider2D>();
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    public PhotonView View { get => _view; }
    /// <summary>�Q�[���J�n���̃A�N�e�B�u��� </summary>
    public bool IsStartActive { get => _isStartActive; }

    private void OnValidate()
    {
        //�Q�[���J�n���̃M�~�b�N��Ԃ�ύX����
        ChangeObjectStauts();

        //�I�u�W�F�N�g�̐F��ύX����
        switch (_objectColor)
        {
            case ObjectColor.Red:
                _spriteRenderer.color = Color.red;
                break;
            case ObjectColor.Blue:
                _spriteRenderer.color = Color.blue;
                break;
            case ObjectColor.White:
                _spriteRenderer.color = Color.white;
                break;
        }
    }

    private void Start()
    {
        ChangeObjectStauts();
    }

    /// <summary>
    /// �{�^���������ꂽ���ɌĂ΂��
    /// <para>�I�u�W�F�N�g�̏�Ԃɂ���ĕ\���E��\����ύX����</para>
    /// </summary>
    [PunRPC]
    public void ChangeActive()
    {
        if (_isCurrentActive) //��\���ɂ���
        {
            ActiveFalse();
        }
        else //�\������
        {
            ActiveTrue();
        }
    }  

    /// <summary>
    /// ���������p�{�^���������ꂽ���ɌĂ΂��@(PartnerButton.cs)
    /// <para>�I�u�W�F�N�g���\���ɂ���</para>
    /// </summary>
    [PunRPC]
    public void ActiveFalse()
    {
        _bc2D.enabled = false;
        _spriteRenderer.enabled = false;
        _isCurrentActive = false;
    }

    /// <summary>
    /// ���������p�{�^���������ꂽ���ɌĂ΂�� (PartnerButton.cs)
    ///  <para>�I�u�W�F�N�g��\������</para>
    /// </summary>
    [PunRPC]
    public void ActiveTrue()
    {
        _bc2D.enabled = true;
        _spriteRenderer.enabled = true;
        _isCurrentActive = true;
    }

    /// <summary>�I�u�W�F�N�g�̏�Ԃ�ύX����</summary>
    void ChangeObjectStauts()
    {
        if (_isStartActive)
        {
            _bc2D.enabled = true;
            _spriteRenderer.enabled = true;
            _isCurrentActive = true;
        }
        else
        {
            _bc2D.enabled = false;
            _spriteRenderer.enabled = false;
            _isCurrentActive = false;
        }
    }

    /// <summary>�I�u�W�F�N�g�̐F </summary>
    public enum ObjectColor
    {
        Red,
        Blue,
        White,
    }
}
