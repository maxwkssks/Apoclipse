using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern4 : MonoBehaviour
{
	// �̵� �ӵ�
	public float MoveSpeed;

	// ���� ���� �ð�
	public float AttackStopTime;

	// �̵� �ð�
	public float MoveTime;

	// �߻��� �Ѿ�
	public GameObject Projectile;

	// �Ѿ��� �̵� �ӵ�
	public float ProjectileMoveSpeed;

	// ���� �� ����
	private bool _isAttack = false;

	// �̵� ���� (1: ������, -1: ����)
	private int _moveDirection = 1;

	void Start()
	{
		// ���� �ڷ�ƾ ����
		StartCoroutine(Attack());
	}

	void Update()
	{
		// ���� ���� �ƴ� ���� �̵�
		if (!_isAttack)
			Move();
	}

	// ���� �ֱ⸶�� �����ϴ� �ڷ�ƾ
	IEnumerator Attack()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f); // 1�� ��ٸ�

			// �÷��̾��� ��ġ�� ã�� ���� ����
			GameObject manager = GameObject.Find("Managers");
			BaseCharacter character = manager.GetComponent<CharacterManager>().Player;
			if (character is null)
			{
				Debug.Log("Player is null");
				break;
			}

			Vector3 playerPos = character.GetComponent<Transform>().position;
			Vector3 direction = playerPos - transform.position;
			direction.Normalize();

			// �Ѿ� ���� �� ���� , �̵� �ӵ� ����
			var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
			projectile.GetComponent<Projectile>().SetDirection(direction);
			projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;

			// ���� �� ���·� ��ȯ
			_isAttack = true;

			yield return new WaitForSeconds(AttackStopTime); // ���� ���� �ð� ���� ���

			// ���� ���� �� �̵� �� ���·� ��ȯ
			_isAttack = false;

			yield return new WaitForSeconds(MoveTime); // �̵� �ð� ���� ���
		}
	}

	// ���� �̵� �޼���
	void Move()
	{
		// ���� x ��ǥ�� �������� ���ο� x ��ǥ ���
		float newX = transform.position.x + _moveDirection * MoveSpeed * Time.deltaTime;

		// ȭ�� ��踦 üũ�Ͽ� ����
		float halfWidth = transform.localScale.x / 2f; // ���� ũ�⸦ ����Ͽ� ���� ������ ���� ���
		float screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
		float maxX = screenWidth / 2f - halfWidth;
		float minX = -maxX;

		// ȭ���� ��� ������ �ִٸ� �̵� ������ ����
		if (newX >= maxX || newX <= minX)
		{
			_moveDirection *= -1;
		}

		// ���� ��ġ�� �̵�
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
