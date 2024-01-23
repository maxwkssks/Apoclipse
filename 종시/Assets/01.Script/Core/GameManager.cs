using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// 전체적으로 게임을 관리하는 클래스
public class GameManager : MonoBehaviour
{
	static public GameManager Instance;  // Singleton 패턴을 사용하여 게임 매니저의 단일 인스턴스를 제어하는 정적 변수

	public CharacterManager CharacterManager;  // 캐릭터 관리자 변수, Inspector에서 설정 가능
	public MapManager MapManager;
	public EnemySpawnManager EnemySpawnManager;

	[HideInInspector] public bool bStageCleared = false;  // 현재 스테이지가 클리어되었는지 여부를 숨겨진 변수로 저장

	private void Awake()  // 객체 생성시 최초 실행되는 Awake 메서드
	{
		if (Instance == null)  // 단 하나만 존재하게끔 확인
		{
			Instance = this;  // 객체 생성시 instance에 자기 자신을 넣어줌
		}
		else
			Destroy(this.gameObject);  // 이미 존재하는 경우 현재 객체를 파괴하여 중복 생성 방지
	}

	public PlayerCharacter GetPlayerCharacter()  // 현재 플레이어 캐릭터를 반환하는 메서드
	{
		return CharacterManager.Player.GetComponent<PlayerCharacter>();
	}

	void Start()
	{
		if (CharacterManager == null) { return; }  // 캐릭터 매니저가 설정되지 않은 경우 초기화를 수행하지 않고 종료
		CharacterManager.Init(this);  // 캐릭터 매니저를 초기화

		MapManager.Init(this);
		EnemySpawnManager.Init(this);
	}

	public void GameStart()  // 게임을 시작하는 메서드
	{
		SceneManager.LoadScene("Stage1");  // "Stage1" 씬을 로드하여 게임을 시작
	}
}
