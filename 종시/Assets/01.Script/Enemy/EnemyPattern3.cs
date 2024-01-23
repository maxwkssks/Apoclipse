using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern3 : MonoBehaviour
{
	// 적 이동 속도
	public float MoveSpeed;

	// 플레이어의 Transform
	private Transform playerTransform;

	// 이동 방향
	private Vector2 _moveDirection;

	void Start()
	{
		// 플레이어를 태그를 기반으로 찾음
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

		// 적의 이동 메서드를 코루틴으로 시작
		StartCoroutine(MoveTowardsPlayer());
	}

	void Update()
	{
		// 적 이동 메서드 호출
		Move();
	}

	// 플레이어 방향으로 일정 주기마다 이동하는 코루틴
	IEnumerator MoveTowardsPlayer()
	{
		while (true)
		{
			// 플레이어의 위치를 기준으로 적의 이동 방향 설정
			_moveDirection = (playerTransform.position - transform.position).normalized;

			// 0.5초마다 플레이어의 위치를 갱신
			yield return new WaitForSeconds(0.5f);
		}
	}

	// 적 이동 메서드
	void Move()
	{
		// 현재 위치를 기준으로 이동 후의 새로운 위치 계산
		Vector2 newPosition = new Vector2(
			transform.position.x + _moveDirection.x * MoveSpeed * Time.deltaTime,
			transform.position.y + _moveDirection.y * MoveSpeed * Time.deltaTime
		);

		// 계산된 위치로 이동
		transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
	}
}
