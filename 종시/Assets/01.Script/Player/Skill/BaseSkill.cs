using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// �⺻���� ��ų setting 
public class BaseSkill : MonoBehaviour
{
	// ĳ���� �Ŵ����� ���� ������ �����ϴ� ����
	protected CharacterManager _characterManager;

	// ��ų�� ��ٿ� �ð��� ���� ��� �ð��� ��Ÿ���� ����
	public float CooldownTime;
	public float CurrentTime;

	// ��ų�� ���� ��ٿ� ������ ��Ÿ���� ����
	public bool bIsCoolDown = false;

	// ĳ���� �Ŵ����� �ʱ�ȭ�ϴ� �޼���
	public void Init(CharacterManager characterManager)
	{
		_characterManager = characterManager;
	}

	// �� �����Ӹ��� ȣ��Ǵ� ������Ʈ �޼���
	public void Update()
	{
		// ���� ��ų�� ���� ��ٿ� ���̸� ��� �ð��� ���ҽ�Ű��, ��ٿ��� �������� Ȯ��
		if (bIsCoolDown)
		{
			CurrentTime -= Time.deltaTime;
			if (CurrentTime <= 0)
			{
				bIsCoolDown = false;
			}
		}
	}

	// ��ų�� ���� ��� �������� Ȯ���ϴ� �޼���
	public bool IsAvailable()
	{
		// ��ų�� ��ٿ� ���� �ƴϸ� ��� ����
		return !bIsCoolDown;
	}

	// ��ų�� Ȱ��ȭ�ϴ� ���� �޼���
	public virtual void Activate()
	{
		// ��ų�� Ȱ��ȭ�ϰ� ��ٿ��� ����
		bIsCoolDown = true;
		CurrentTime = CooldownTime;
	}

	// ��ٿ��� �ʱ�ȭ�ϴ� �޼���
	public void InitCoolDown()
	{
		bIsCoolDown = false;
		CurrentTime = 0;
	}
}
