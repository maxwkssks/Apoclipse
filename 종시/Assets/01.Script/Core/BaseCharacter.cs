using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour를 상속받아서 게임에서 사용될 베이스 캐릭터 클래스
public class BaseCharacter : MonoBehaviour
{
	// 캐릭터 매니저에 대한 참조를 저장하는 변수 선언
	private CharacterManager _characterManager;

	// CharacterManager 속성 정의
	public CharacterManager CharacterManager => _characterManager;

	/*
   
    public CharacterManager CharacterManager
    {
        get { return _characterManager; }
    }
    */

	// 가상 메서드: 상속받는 클래스에서 재정의할 수 있는 초기화 메서드
	public virtual void Init(CharacterManager characterManager)
	{
		// 캐릭터 매니저에 전달된 값을 저장
		_characterManager = characterManager;
	}
}
