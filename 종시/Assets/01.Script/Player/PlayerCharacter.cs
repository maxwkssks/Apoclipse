using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �÷��̾� ĳ���Ͱ� ����ϴ� ������/��ų/���� ���� ������ Ŭ����
public class PlayerCharacter : BaseCharacter
{
	// �����Ӱ� �̵� �ӵ��� �����ϴ� ���� ����
	#region Movement
	private Vector2 _moveInput; // �̵� �Է°�
	public float MoveSpeed; // �̵� �ӵ�
	#endregion

	// ��ų ���� �������� �����ϴ� ���� ����
	#region Skills
	[HideInInspector] public Dictionary<EnumTypes.PlayerSkill, BaseSkill> Skills; // EnumTypes.PlayerSkill�� BaseSkill�� �����ϴ� ��ųʸ�
	[SerializeField] private GameObject[] _skillPrefabs; // ��ų �������� �����ϴ� �迭
	#endregion

	#region Weapon
	public int CurrentWeaponLevel = 0; // ���� ���� ����
	public int MaxWeaponLevel = 3; // �ִ� ���� ����
	#endregion

	#region Invincibility
	private bool invincibility; // ���� ���� ����
	private Coroutine invincibilityCoroutine; // ���� ���ӽð��� �����ϴ� �ڷ�ƾ
	private const double InvincibilityDurationInSeconds = 3; // ���� ���� �ð� (��)
	public bool Invincibility
	{
		get { return invincibility; }
		set { invincibility = value; }
	}
	#endregion

	// ĳ���� �ʱ�ȭ�� �������ϰ� ��ų �ʱ�ȭ �Լ��� ȣ���ϴ� �޼���
	public override void Init(CharacterManager characterManager)
	{
		base.Init(characterManager);
		InitializeSkills();
	}

	// ĳ���Ͱ� ������ ���� ������Ʈ�� �����ϰ� ���� �޴��� ���ư��� �޼���
	public void DeadProcess()
	{
		Destroy(gameObject);
		SceneManager.LoadScene("MainMenu");
	}

	// �������� �����Ӱ� ��ų�� �Է��� üũ��
	private void Update()
	{
		UpdateSkillInput();
		UpdateMovement();
	}

	// ������ �������� ����ϴ� �ڵ�
	private void UpdateMovement()
	{
		// Horizontal�� Vertical ���� ��ȭ���Ѽ� ĳ���͸� ������
		_moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		transform.Translate(new Vector3(_moveInput.x, _moveInput.y, 0f) * (MoveSpeed * Time.deltaTime));

		// ī�޶��� ���� �ϴ��� (0, 0, 0.0)�̸�, ���� ����� (1.0 , 1.0)�̴�.
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		if (pos.x < 0f) pos.x = 0f;
		if (pos.x > 1f) pos.x = 1f;
		if (pos.y < 0f) pos.y = 0f;
		if (pos.y > 1f) pos.y = 1f;
		// ����Ʈ ��ǥ������ ������� ��ġ�� ������� ���� 3D ���迡���� ��ǥ�� ��ȯ
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	private void UpdateSkillInput()
	{
		if (Input.GetKey(KeyCode.Z)) ActivateSkill(EnumTypes.PlayerSkill.Primary); // ZŰ�� ������ ���� ��, �⺻��ų�� ȣ���.
		if (Input.GetKeyUp(KeyCode.X)) ActivateSkill(EnumTypes.PlayerSkill.Repair); // XŰ�� ������ ������ �� Repair��ų�� ȣ���.
		if (Input.GetKeyUp(KeyCode.C)) ActivateSkill(EnumTypes.PlayerSkill.Bomb); // CŰ�� ������ ������ �� Bomb��ų�� ȣ���.
	}

	private void InitializeSkills()
	{
		Skills = new Dictionary<EnumTypes.PlayerSkill, BaseSkill>();

		// ��ų ������ �迭�� ��ȸ�ϸ鼭 ��ų�� �ʱ�ȭ�ϰ� ��ųʸ��� �߰��մϴ�.
		for (int i = 0; i < _skillPrefabs.Length; i++)
		{
			AddSkill((EnumTypes.PlayerSkill)i, _skillPrefabs[i]);
		}

		// ���� �ν��Ͻ����� ���� �÷��̾��� ���� ������ �����ͼ� �����մϴ�.
		CurrentWeaponLevel = GameInstance.instance.CurrentPlayerWeaponLevel;
	}

	private void AddSkill(EnumTypes.PlayerSkill skillType, GameObject prefab)
	{
		// ��ų �������� �ν��Ͻ�ȭ�ϰ� �÷��̾� ĳ���Ϳ� �ڽ����� �߰��մϴ�.
		GameObject skillObject = Instantiate(prefab, transform.position, Quaternion.identity);
		skillObject.transform.parent = this.transform;

		if (skillObject != null)
		{
			// �ν��Ͻ�ȭ�� ��ų ������Ʈ�κ��� BaseSkill ������Ʈ�� ������ �ʱ�ȭ�մϴ�.
			BaseSkill skillComponent = skillObject.GetComponent<BaseSkill>();
			skillComponent.Init(CharacterManager);

			// ��ųʸ��� ��ų�� �߰��մϴ�. (Ű: ��ų ����, ��: BaseSkill ������Ʈ)
			Skills.Add(skillType, skillComponent);
		}
	}

	private void ActivateSkill(EnumTypes.PlayerSkill skillType)
	{
		// ��ųʸ��� �ش��ϴ� ��ų�� �����ϸ� Ȱ��ȭ�մϴ�.
		if (Skills.ContainsKey(skillType))
		{
			// ��ų�� ��� ������ ���¶�� Ȱ��ȭ�մϴ�.
			if (Skills[skillType].IsAvailable())
			{
				Skills[skillType].Activate();
			}
			// ��� �Ұ����� ���¶�� (��ٿ� ���̶��) �ּ� ó���� �κ�ó�� ó���� �� �ֽ��ϴ�.
			// else
			// {
			//     if (skillType != EnumTypes.PlayerSkill.Primary)
			//         GetComponent<PlayerUI>().NoticeSkillCooldown(skillType);
			// }
		}
	}
}
