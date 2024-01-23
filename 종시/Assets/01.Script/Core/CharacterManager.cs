using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


// BaseManager를 상속 받아서 특정 게임 캐릭터를 관리하는 클래스
public class CharacterManager : BaseManager
{
	// 플레이어를 나타냄
	[SerializeField]
	private BaseCharacter _player;
	// 어디서든 접근 할 수 있게끔 만드는 프로퍼티

	public BaseCharacter Player => _player;
	

	// virtual로 썼던 BaseManager의 코드를 재정의 하여, Player를 Init함
	public override void Init(GameManager gameManager)
	{
		base.Init(gameManager);
		_player.Init(this);
	}
}
