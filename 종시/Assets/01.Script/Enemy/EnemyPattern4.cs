using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern4 : MonoBehaviour
{
	// 이동 속도
	public float MoveSpeed;

	// 공격 중지 시간
	public float AttackStopTime;

	// 이동 시간
	public float MoveTime;

	// 발사할 총알
	public GameObject Projectile;

	// 총알의 이동 속도
	public float ProjectileMoveSpeed;

	// 공격 중 여부
	private bool _isAttack = false;

	// 이동 방향 (1: 오른쪽, -1: 왼쪽)
	private int _moveDirection = 1;

	void Start()
	{
		// 공격 코루틴 시작
		StartCoroutine(Attack());
	}

	void Update()
	{
		// 공격 중이 아닐 때만 이동
		if (!_isAttack)
			Move();
	}

	// 일정 주기마다 공격하는 코루틴
	IEnumerator Attack()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f); // 1초 기다림

			// 플레이어의 위치를 찾아 방향 설정
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

			// 총알 생성 및 방향 , 이동 속도 설정
			var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
			projectile.GetComponent<Projectile>().SetDirection(direction);
			projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;

			// 공격 중 상태로 전환
			_isAttack = true;

			yield return new WaitForSeconds(AttackStopTime); // 공격 중지 시간 동안 대기

			// 공격 중지 후 이동 중 상태로 전환
			_isAttack = false;

			yield return new WaitForSeconds(MoveTime); // 이동 시간 동안 대기
		}
	}

	// 적의 이동 메서드
	void Move()
	{
		// 현재 x 좌표를 기준으로 새로운 x 좌표 계산
		float newX = transform.position.x + _moveDirection * MoveSpeed * Time.deltaTime;

		// 화면 경계를 체크하여 반전
		float halfWidth = transform.localScale.x / 2f; // 적의 크기를 고려하여 가로 길이의 반을 계산
		float screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
		float maxX = screenWidth / 2f - halfWidth;
		float minX = -maxX;

		// 화면을 벗어날 위험이 있다면 이동 방향을 반전
		if (newX >= maxX || newX <= minX)
		{
			_moveDirection *= -1;
		}

		// 계산된 위치로 이동
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
