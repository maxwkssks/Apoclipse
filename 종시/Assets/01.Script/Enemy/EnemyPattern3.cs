using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern3 : MonoBehaviour
{
	// �� �̵� �ӵ�
	public float MoveSpeed;

	// �÷��̾��� Transform
	private Transform playerTransform;

	// �̵� ����
	private Vector2 _moveDirection;

	void Start()
	{
		// �÷��̾ �±׸� ������� ã��
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

		// ���� �̵� �޼��带 �ڷ�ƾ���� ����
		StartCoroutine(MoveTowardsPlayer());
	}

	void Update()
	{
		// �� �̵� �޼��� ȣ��
		Move();
	}

	// �÷��̾� �������� ���� �ֱ⸶�� �̵��ϴ� �ڷ�ƾ
	IEnumerator MoveTowardsPlayer()
	{
		while (true)
		{
			// �÷��̾��� ��ġ�� �������� ���� �̵� ���� ����
			_moveDirection = (playerTransform.position - transform.position).normalized;

			// 0.5�ʸ��� �÷��̾��� ��ġ�� ����
			yield return new WaitForSeconds(0.5f);
		}
	}

	// �� �̵� �޼���
	void Move()
	{
		// ���� ��ġ�� �������� �̵� ���� ���ο� ��ġ ���
		Vector2 newPosition = new Vector2(
			transform.position.x + _moveDirection.x * MoveSpeed * Time.deltaTime,
			transform.position.y + _moveDirection.y * MoveSpeed * Time.deltaTime
		);

		// ���� ��ġ�� �̵�
		transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
	}
}
