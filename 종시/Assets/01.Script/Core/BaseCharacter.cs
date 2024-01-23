using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour�� ��ӹ޾Ƽ� ���ӿ��� ���� ���̽� ĳ���� Ŭ����
public class BaseCharacter : MonoBehaviour
{
	// ĳ���� �Ŵ����� ���� ������ �����ϴ� ���� ����
	private CharacterManager _characterManager;

	// CharacterManager �Ӽ� ����
	public CharacterManager CharacterManager => _characterManager;

	/*
   
    public CharacterManager CharacterManager
    {
        get { return _characterManager; }
    }
    */

	// ���� �޼���: ��ӹ޴� Ŭ�������� �������� �� �ִ� �ʱ�ȭ �޼���
	public virtual void Init(CharacterManager characterManager)
	{
		// ĳ���� �Ŵ����� ���޵� ���� ����
		_characterManager = characterManager;
	}
}
