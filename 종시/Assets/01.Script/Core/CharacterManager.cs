using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


// BaseManager�� ��� �޾Ƽ� Ư�� ���� ĳ���͸� �����ϴ� Ŭ����
public class CharacterManager : BaseManager
{
	// �÷��̾ ��Ÿ��
	[SerializeField]
	private BaseCharacter _player;
	// ��𼭵� ���� �� �� �ְԲ� ����� ������Ƽ

	public BaseCharacter Player => _player;
	

	// virtual�� ��� BaseManager�� �ڵ带 ������ �Ͽ�, Player�� Init��
	public override void Init(GameManager gameManager)
	{
		base.Init(gameManager);
		_player.Init(this);
	}
}
