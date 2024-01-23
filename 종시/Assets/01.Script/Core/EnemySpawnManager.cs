using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터를 스폰하는 것을 관리하는 클래스 
public class EnemySpawnManager : BaseManager
{
	public GameObject[] Enemys; // 게임에 등장하는 적들
	public Transform[] EnemySpawnTransform; // 적이 생성되는 위치
	public float CoolDownTime; // 스폰 쿨타임
	public int MaxSpawnEnemyCount; // 최대 스폰된 적의 수 

	private int _spawnCount = 0; // 스폰 수
	public int BossSpawnCount = 10; // 보스가 스폰되기 위한 적의 수 

	private bool _bSpawnBoss = false; // 보스가 스폰 되었는지 여부

	public GameObject BossA; // 소환되는 BossA

	// 가상함수를 썼던 GameManager를 재정의 함.
	public override void Init(GameManager gameManager)
	{
		base.Init(gameManager); // 상위 클래스에서 Init을 사용
		StartCoroutine(SpawnEnemy()); // ->	 부모 클래스의 가상함수를 적을 소환하는 코루틴을 추가하여 적을 소환함
	}

	IEnumerator SpawnEnemy()
	{
		// 보스가 아직 소환되지 않은 동안 계속해서 실행됨
		while (!_bSpawnBoss)
		{
			// CoolDownTime 만큼 대기
			yield return new WaitForSeconds(CoolDownTime);

			// 무작위로 생성할 
			int spawnCount = Random.Range(1, EnemySpawnTransform.Length); 

			// EnemySpawnTransform.Length 크기의 리스트를 생성하고 실제 공간을 만들었다
			List<int> availablePositions = new List<int>(EnemySpawnTransform.Length);

			for (int i = 0; i < EnemySpawnTransform.Length; i++)
			{
				availablePositions.Add(i);  
			}

			// 결정된 개수만큼의 적을 생성
			for (int i = 0; i < spawnCount; i++)
			{
				
				// 적과 그 위치를 가져옴
				int randomEnemy = Random.Range(0, Enemys.Length);
			
				int randomPositionIndex = Random.Range(0, availablePositions.Count);
				
				int randomPosition = availablePositions[randomPositionIndex];
				

				// 사용되었던 위치를 리스트에서 제거해서 중복사용을 방지하는 코드
				availablePositions.RemoveAt(randomPositionIndex);

				// 적을 해당 위치에 생성
				Instantiate(Enemys[randomEnemy], EnemySpawnTransform[randomPosition].position, Quaternion.identity);
			}

			// 생성된 전체 적의 수를 증가
			_spawnCount += spawnCount;

			// 지정된 보스 소환 횟수에 도달하면 보스를 소환함
			if (_spawnCount >= BossSpawnCount)
			{
				_bSpawnBoss = true;
				Instantiate(BossA, new Vector3(EnemySpawnTransform[1].position.x, EnemySpawnTransform[1].position.y + 1, 0f), Quaternion.identity); // 아무런 회전이 적용되지 않은 상태
			}
		}
	}
}