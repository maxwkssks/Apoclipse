using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// 기본적인 스킬 setting 
public class BaseSkill : MonoBehaviour
{
	// 캐릭터 매니저에 대한 참조를 저장하는 변수
	protected CharacterManager _characterManager;

	// 스킬의 쿨다운 시간과 현재 경과 시간을 나타내는 변수
	public float CooldownTime;
	public float CurrentTime;

	// 스킬이 현재 쿨다운 중인지 나타내는 변수
	public bool bIsCoolDown = false;

	// 캐릭터 매니저를 초기화하는 메서드
	public void Init(CharacterManager characterManager)
	{
		_characterManager = characterManager;
	}

	// 매 프레임마다 호출되는 업데이트 메서드
	public void Update()
	{
		// 만약 스킬이 현재 쿨다운 중이면 경과 시간을 감소시키고, 쿨다운이 끝났는지 확인
		if (bIsCoolDown)
		{
			CurrentTime -= Time.deltaTime;
			if (CurrentTime <= 0)
			{
				bIsCoolDown = false;
			}
		}
	}

	// 스킬이 현재 사용 가능한지 확인하는 메서드
	public bool IsAvailable()
	{
		// 스킬이 쿨다운 중이 아니면 사용 가능
		return !bIsCoolDown;
	}

	// 스킬을 활성화하는 가상 메서드
	public virtual void Activate()
	{
		// 스킬을 활성화하고 쿨다운을 시작
		bIsCoolDown = true;
		CurrentTime = CooldownTime;
	}

	// 쿨다운을 초기화하는 메서드
	public void InitCoolDown()
	{
		bIsCoolDown = false;
		CurrentTime = 0;
	}
}
