using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

// MonoBehaviour에서 상속받은 GameInstance 클래스의 정의
public class GameInstance : MonoBehaviour
{
	// GameInstance 클래스에 대한 정적 참조, 단 하나의 인스턴스만 존재하도록 함
	public static GameInstance instance;

	public bool BossBSummoned = false;


	// 게임 관련 정보를 저장하는 변수들
	public float GameStartTime = 0f;            // 게임 시작 시간
	public int Score = 0;                       // 플레이어 스코어
	public int CurrentStageLevel = 1;           // 현재 스테이지 레벨

	public int CurrentPlayerWeaponLevel = 0;   // 플레이어 무기 레벨
	public int CurrentPlayerHP = 3;             // 플레이어 현재 체력
	public float CurrentPlayerFuel = 100f;      // 플레이어 현재 연료

	// Awake 메서드는 스크립트 인스턴스가 로드될 때 호출됨
	private void Awake()
	{
		// GameInstance의 인스턴스가 이미 존재하는지 확인
		if (instance == null)
		{
			// 인스턴스가 없으면 현재 인스턴스를 설정하고 씬 전환시에도 유지되도록 함
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			// 이미 인스턴스가 존재하면 중복된 인스턴스를 파괴
			Destroy(gameObject);
		}

		// Unity의 Time 클래스를 사용하여 게임 시작 시간을 기록
		GameStartTime = Time.time;
	}
}
