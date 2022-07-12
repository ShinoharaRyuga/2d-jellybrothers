using Photon.Pun;
using UnityEngine;


/// <summary>�e�M�~�b�N�̕\���E��\����ύX���� </summary>
[RequireComponent(typeof(BoxCollider2D))]
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
    [SerializeField, Header("���̐F")] DoorColor _doorColor = DoorColor.Red;
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

        //�h�A�̐F��ύX����
        if (_doorColor == DoorColor.Red)
        {
            _spriteRenderer.color = Color.red;
        }
        else if (_doorColor == DoorColor.Blue)
        {
            _spriteRenderer.color= Color.blue;
        }
    }

    /// <summary>
    /// �{�^���������ꂽ���ɌĂ΂��
    /// <para>�I�u�W�F�N�g�̏�Ԃɂ���ĕ\���E��\����ύX����</para>
    /// </summary>
    [PunRPC]
    void ChangeActive()
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
    void ActiveFalse()
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
    void ActiveTrue()
    {
        _bc2D.enabled = true;
        _spriteRenderer.enabled = true;
        _isCurrentActive = true;
    }

    public enum DoorColor
    {
        Red,
        Blue,
    }
}
