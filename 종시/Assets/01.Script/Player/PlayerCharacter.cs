using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 플레이어 캐릭터가 사용하는 움직임/스킬/무기 등을 정의한 클래스
public class PlayerCharacter : BaseCharacter
{
	// 움직임과 이동 속도를 정의하는 지역 변수
	#region Movement
	private Vector2 _moveInput; // 이동 입력값
	public float MoveSpeed; // 이동 속도
	#endregion

	// 스킬 관련 변수들을 정의하는 지역 변수
	#region Skills
	[HideInInspector] public Dictionary<EnumTypes.PlayerSkill, BaseSkill> Skills; // EnumTypes.PlayerSkill과 BaseSkill을 저장하는 딕셔너리
	[SerializeField] private GameObject[] _skillPrefabs; // 스킬 프리팹을 저장하는 배열
	#endregion

	#region Weapon
	public int CurrentWeaponLevel = 0; // 현재 무기 레벨
	public int MaxWeaponLevel = 3; // 최대 무기 레벨
	#endregion

	#region Invincibility
	private bool invincibility; // 무적 상태 여부
	private Coroutine invincibilityCoroutine; // 무적 지속시간을 관리하는 코루틴
	private const double InvincibilityDurationInSeconds = 3; // 무적 지속 시간 (초)
	public bool Invincibility
	{
		get { return invincibility; }
		set { invincibility = value; }
	}
	#endregion

	// 캐릭터 초기화를 재정의하고 스킬 초기화 함수를 호출하는 메서드
	public override void Init(CharacterManager characterManager)
	{
		base.Init(characterManager);
		InitializeSkills();
	}

	// 캐릭터가 죽으면 게임 오브젝트를 제거하고 메인 메뉴로 돌아가는 메서드
	public void DeadProcess()
	{
		Destroy(gameObject);
		SceneManager.LoadScene("MainMenu");
	}

	// 직접적인 움직임과 스킬의 입력을 체크함
	private void Update()
	{
		UpdateSkillInput();
		UpdateMovement();
	}

	// 실제로 움직임을 담당하는 코드
	private void UpdateMovement()
	{
		// Horizontal과 Vertical 값을 변화시켜서 캐릭터를 움직임
		_moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		transform.Translate(new Vector3(_moveInput.x, _moveInput.y, 0f) * (MoveSpeed * Time.deltaTime));

		// 카메라의 좌측 하단은 (0, 0, 0.0)이며, 우측 상단은 (1.0 , 1.0)이다.
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		if (pos.x < 0f) pos.x = 0f;
		if (pos.x > 1f) pos.x = 1f;
		if (pos.y < 0f) pos.y = 0f;
		if (pos.y > 1f) pos.y = 1f;
		// 뷰포트 좌표에서의 상대적인 위치를 기반으로 실제 3D 세계에서의 좌표로 변환
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	private void UpdateSkillInput()
	{
		if (Input.GetKey(KeyCode.Z)) ActivateSkill(EnumTypes.PlayerSkill.Primary); // Z키를 누르고 있을 때, 기본스킬이 호출됨.
		if (Input.GetKeyUp(KeyCode.X)) ActivateSkill(EnumTypes.PlayerSkill.Repair); // X키를 누르고 떼었을 때 Repair스킬이 호출됨.
		if (Input.GetKeyUp(KeyCode.C)) ActivateSkill(EnumTypes.PlayerSkill.Bomb); // C키를 누르고 떼었을 때 Bomb스킬이 호출됨.
	}

	private void InitializeSkills()
	{
		Skills = new Dictionary<EnumTypes.PlayerSkill, BaseSkill>();

		// 스킬 프리팹 배열을 순회하면서 스킬을 초기화하고 딕셔너리에 추가합니다.
		for (int i = 0; i < _skillPrefabs.Length; i++)
		{
			AddSkill((EnumTypes.PlayerSkill)i, _skillPrefabs[i]);
		}

		// 게임 인스턴스에서 현재 플레이어의 무기 레벨을 가져와서 설정합니다.
		CurrentWeaponLevel = GameInstance.instance.CurrentPlayerWeaponLevel;
	}

	private void AddSkill(EnumTypes.PlayerSkill skillType, GameObject prefab)
	{
		// 스킬 프리팹을 인스턴스화하고 플레이어 캐릭터에 자식으로 추가합니다.
		GameObject skillObject = Instantiate(prefab, transform.position, Quaternion.identity);
		skillObject.transform.parent = this.transform;

		if (skillObject != null)
		{
			// 인스턴스화된 스킬 오브젝트로부터 BaseSkill 컴포넌트를 가져와 초기화합니다.
			BaseSkill skillComponent = skillObject.GetComponent<BaseSkill>();
			skillComponent.Init(CharacterManager);

			// 딕셔너리에 스킬을 추가합니다. (키: 스킬 유형, 값: BaseSkill 컴포넌트)
			Skills.Add(skillType, skillComponent);
		}
	}

	private void ActivateSkill(EnumTypes.PlayerSkill skillType)
	{
		// 딕셔너리에 해당하는 스킬이 존재하면 활성화합니다.
		if (Skills.ContainsKey(skillType))
		{
			// 스킬이 사용 가능한 상태라면 활성화합니다.
			if (Skills[skillType].IsAvailable())
			{
				Skills[skillType].Activate();
			}
			// 사용 불가능한 상태라면 (쿨다운 중이라면) 주석 처리된 부분처럼 처리할 수 있습니다.
			// else
			// {
			//     if (skillType != EnumTypes.PlayerSkill.Primary)
			//         GetComponent<PlayerUI>().NoticeSkillCooldown(skillType);
			// }
		}
	}
}
